using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SearcherExtensibility;

namespace SearcherCore
{
	public class SearchManager
	{
		private readonly object _syncRoot;
		private readonly PluginManager _pluginMgr = new PluginManager();
		private IDictionary<int, CancellationTokenSource> WorkerTokenSources { get; set; }
		private int _currWorkerId;

		private readonly DataTable _foundFiles;

		public DataView FoundFiles { get; private set; }
		public BindingList<SearchWorker> SearchWorkers { get; private set; }
		public BindingList<string> PluginList { get; private set; }

		public SearchManager()
		{
			_syncRoot = new object();
			WorkerTokenSources = new Dictionary<int, CancellationTokenSource>();

			_foundFiles = new DataTable();
			_foundFiles.Columns.Add("wid", typeof(int));
			_foundFiles.Columns.Add("fname", typeof(string));

			FoundFiles = new DataView(_foundFiles);
			SearchWorkers = new BindingList<SearchWorker>();
			PluginList = new BindingList<string> { String.Empty };
		}

		public int LoadPlugins(string path)
		{
			var npl = _pluginMgr.LoadPlugins(path);
			PluginList.Clear();
			PluginList.Add(String.Empty);
			foreach (var pl in _pluginMgr.GetPluginList())
			{
				PluginList.Add(pl);
			}
			return npl;
		}

		public async void StartSearch(FileSearchParam param)
		{
			var workerTokenSource = new CancellationTokenSource();
			var token = workerTokenSource.Token;

			var searcher = CreateSearcher(token, param.PlugName);
			searcher.OnFileFound += searcher_OnFileFound;

			SearchWorkers.Add(new SearchWorker
				{
					Id = searcher.Id,
					FilesFound = 0,
					Parameter = param.ToString(),
					Status = "Running"
				});
			WorkerTokenSources.Add(searcher.Id, workerTokenSource);
			try
			{
				await Task.Factory.StartNew(() => searcher.Search(param));
			}
			catch (OperationCanceledException)
			{
				// handle task cancelation
				Debug.WriteLine("Search canseled: {0}", searcher.Id);
			}
			catch (Exception ex)
			{
				// other exception occured
				Debug.WriteLine(ex);
			}
			finally
			{
				var worker = SearchWorkers.SingleOrDefault(w => w.Id == searcher.Id);
				if (worker != null)
					worker.Status = "Stopped";
				WorkerTokenSources.Remove(searcher.Id);
			}
		}

		public void TerminateSearch(SearchWorker worker)
		{
			if (WorkerTokenSources.ContainsKey(worker.Id))
			{
				WorkerTokenSources[worker.Id].Cancel();
			}
		}

		public void ClearResult(SearchWorker worker)
		{
			lock (_syncRoot)
			{
				foreach (var row in _foundFiles.Select(string.Format("wid = {0}", worker.Id)))
				{
					_foundFiles.Rows.Remove(row);
				}
			}
		}

		public void ApplyFilter(SearchWorker worker)
		{
			lock (_syncRoot)
			{
				FoundFiles.RowFilter = string.Format("wid = {0}", worker.Id);
			}
		}

		private void searcher_OnFileFound(object sender, FileSearcher.FileFoundArgs e)
		{
			lock (_syncRoot)
			{
				_foundFiles.Rows.Add(e.SearcherId, e.FileName);
				SearchWorkers.Single(w => w.Id == e.SearcherId).FilesFound++;
			}
		}

		private FileSearcher CreateSearcher(CancellationToken ct, string pluginName)
		{
			return new FileSearcher(_currWorkerId++, ct, _pluginMgr.GetProcessor(pluginName));
		}
	}
}
