using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SearcherExtensibility;

namespace SearcherCore
{
	public class SearchManager
	{
		private enum WorkerStatus
		{
			Pending,
			Running,
			Stopped
		}

		private readonly object _syncRoot;
		private readonly PluginManager _pluginMgr = new PluginManager();
		private IDictionary<int, CancellationTokenSource> WorkerTokenSources { get; set; }
		private int _currWorkerId;

		public BindingList<SearchWorker> SearchWorkers { get; private set; }
		public BindingList<string> PluginList { get; private set; }

		#region event declaration

		public event OnFileFoundDelegate OnFileFound;

		private void FileFound(int workerId, string fileName)
		{
			OnFileFoundDelegate handler = OnFileFound;
			if (handler != null)
			{
				handler(this, new FileFoundArgs
				{
					FileName = fileName,
					SearcherId = workerId
				});
			}
		}

		#endregion

		public SearchManager()
		{
			_syncRoot = new object();
			WorkerTokenSources = new Dictionary<int, CancellationTokenSource>();
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
			searcher.OnSearchStarted += searcher_OnSearchStarted;
			searcher.OnFileFound += searcher_OnFileFound;

			SearchWorkers.Add(new SearchWorker
				{
					Id = searcher.Id,
					FilesFound = 0,
					Parameter = param.ToString(),
					Status = WorkerStatus.Pending.ToString()
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
					worker.Status = WorkerStatus.Stopped.ToString();
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

		void searcher_OnSearchStarted(object sender, EventArgs e)
		{
			lock (_syncRoot)
			{
				var worker = SearchWorkers.SingleOrDefault(w => w.Id == ((FileSearcher)sender).Id);
				if (worker != null)
					worker.Status = WorkerStatus.Running.ToString();
			}
		}

		private void searcher_OnFileFound(object sender, FileFoundArgs e)
		{
			lock (_syncRoot)
			{
				var worker = SearchWorkers.SingleOrDefault(w => w.Id == e.SearcherId);
				if (worker != null)
					worker.FilesFound++;
				FileFound(e.SearcherId, e.FileName);
			}
		}

		private FileSearcher CreateSearcher(CancellationToken ct, string pluginName)
		{
			return new FileSearcher(_currWorkerId++, ct, _pluginMgr.GetProcessor(pluginName));
		}
	}
}
