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
		#region Invalidate UI event declaration

		public event Action OnInvalidate;
		private long _lastInvalidate;
		private const long InvalidateMinPeriod = 1000000;

		private void Invalidate(bool force = false)
		{
			if (OnInvalidate != null && (Stopwatch.GetTimestamp() - _lastInvalidate > InvalidateMinPeriod || force))
			{
				_lastInvalidate = Stopwatch.GetTimestamp();
				OnInvalidate();
			}
		}

		#endregion

		#region Nested types

		public class FileSearchParam
		{
			public override string ToString()
			{
				return string.Format("Searching '{0}' in '{1}' using {2}",
					SearchPattern,
					string.IsNullOrEmpty(RootDir) ? "everyware" : RootDir,
					(PluginType)PlugType);
			}

			public int PlugType { get; set; }
			public string RootDir { get; set; }
			public string SearchPattern { get; set; }
			public bool IgnoreCase { get; set; }
			public DateTime? CreationTimeFrom { get; set; }
			public DateTime? CreationTimeTo { get; set; }
			public long? SizeFrom { get; set; }
			public long? SizeTo { get; set; }
		}

		public class SearchWorker
		{
			public int Id { get; set; }
			public string Parameter { get; set; }
			public int FilesFound { get; set; }
			public string Status { get; set; }
		}

		#endregion

		private readonly object _syncRoot;
		private readonly PluginManager _pluginMgr = new PluginManager();
		private IDictionary<int, CancellationTokenSource> WorkerTokenSources { get; set; }
		private int _currWorkerId;

		public DataTable FoundFiles { get; private set; }
		public BindingList<SearchWorker> SearchWorkers { get; private set; }
		public BindingList<string> PluginList { get; private set; }

		public SearchManager()
		{
			_syncRoot = new object();
			WorkerTokenSources = new Dictionary<int, CancellationTokenSource>();

			FoundFiles = new DataTable();
			FoundFiles.Columns.Add("wid", typeof(int));
			FoundFiles.Columns.Add("fname", typeof(string));

			SearchWorkers = new BindingList<SearchWorker>();
			PluginList = new BindingList<string> { PluginType.NoPlugin.ToString() };
		}

		public int LoadPlugins(string path)
		{
			var npl = _pluginMgr.LoadPlugins(path);
			PluginList.Clear();
			PluginList.Add(PluginType.NoPlugin.ToString());
			foreach (var pl in _pluginMgr.GetPluginList().OrderBy(p => p))
			{
				PluginList.Add(pl.ToString());
			}
			return npl;
		}

		public async void StartSearch(FileSearchParam param)
		{
			var workerTokenSource = new CancellationTokenSource();
			var token = workerTokenSource.Token;

			var searcher = CreateSearcher(token, (PluginType)param.PlugType);
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
			catch (Exception)
			{
				// handle task cancelation
				Debug.WriteLine("Search canseled: {0}", searcher.Id);
			}
			finally
			{
				WorkerTokenSources.Remove(searcher.Id);
				SearchWorkers.Single(w => w.Id == searcher.Id).Status = "Stopped";
				Invalidate(true);
			}
		}

		public void TerminateSearch(int workerId)
		{
			if (WorkerTokenSources.ContainsKey(workerId))
			{
				WorkerTokenSources[workerId].Cancel();
			}
		}

		public void ClearResult(int workerId)
		{
			lock (_syncRoot)
			{
				foreach (var row in FoundFiles.Select(string.Format("wid = {0}", workerId)))
				{
					FoundFiles.Rows.Remove(row);
				}
			}
		}

		public void ApplyFilter(int workerId)
		{
			lock (_syncRoot)
			{
				FoundFiles.DefaultView.RowFilter = string.Format("wid = {0}", workerId);
			}
		}

		private void searcher_OnFileFound(object sender, FileSearcher.FileFoundArgs e)
		{
			lock (_syncRoot)
			{
				FoundFiles.Rows.Add(e.SearcherId, e.FileName);
				SearchWorkers.Single(w => w.Id == e.SearcherId).FilesFound++;
				Invalidate();
			}
		}

		private FileSearcher CreateSearcher(CancellationToken ct, PluginType type)
		{
			return new FileSearcher(_currWorkerId++, ct, _pluginMgr.GetProcessor(type));
		}
	}
}
