using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SearcherExtensibility;

namespace SearcherCore
{
	public class SearchManager
	{
		public class FileSearchParam
		{
			public FileSearchParam()
			{
			}

			public override string ToString()
			{
				return string.Format("Searching '{0}' in '{1}' using {2}", SearchPattern, RootDir, (PluginType)PlugType);
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

		public SearchManager()
		{
			workerTokenSources = new List<CancellationTokenSource>();
			StartedSearchProcesses = new List<string>();
			FoundFiles = new List<string>();
		}

		private readonly PluginManager _pluginMgr = new PluginManager();

		public int LoadPlugins(string path)
		{
			return _pluginMgr.LoadPlugins(path);
		}

		#region Manager events declaration

		public class SearchStartEventArgs : EventArgs
		{
			public string Details { get; set; }
		}

		public class SearchStopEventArgs : EventArgs
		{
			public int Index { get; set; }
		}

		public delegate void SearchDelegate(object sender, EventArgs e);
		public event SearchDelegate OnSearchStarted;
		public event SearchDelegate OnSearchFinished;

		private void SearchStart(int workerIndex)
		{
			if (OnSearchStarted != null)
			{
				OnSearchStarted(this, new SearchStartEventArgs() { Details = StartedSearchProcesses[workerIndex] });
			}
		}

		private void SearchFinish(int workerIndex)
		{
			if (OnSearchFinished != null)
			{
				OnSearchFinished(this, new SearchStopEventArgs() { Index = workerIndex });
			}
		}

		#endregion

		public IList<string> PluginList
		{
			get
			{
				return _pluginMgr.GetPluginList().Concat(new[] { PluginType.NoPlugin })
						.OrderBy(p => p)
						.Select(p => p.ToString())
						.ToList();
			}
		}

		public IList<string> StartedSearchProcesses { get; set; }

		private IList<CancellationTokenSource> workerTokenSources { get; set; }

		public IList<string> FoundFiles { get; set; }

		public async void StartSearch(FileSearchParam param)
		{
			var workerTokenSource = new CancellationTokenSource();
			var token = workerTokenSource.Token;

			var searcher = CreateSearcher(token, (PluginType)param.PlugType);
			searcher.OnFileFound += searcher_OnFileFound;

			workerTokenSources.Add(workerTokenSource);
			StartedSearchProcesses.Add(param.ToString());
			var workerIndex = workerTokenSources.IndexOf(workerTokenSource);

			SearchStart(workerIndex);
			await Task.Factory.StartNew(() => searcher.Search(param));
			SearchFinish(workerIndex);

			StartedSearchProcesses.Remove(param.ToString());
			workerTokenSources.Remove(workerTokenSource);
		}

		public void TerminateSearch(int workerIndex)
		{
			workerTokenSources[workerIndex].Cancel();
		}

		private void searcher_OnFileFound(object sender, FileFoundArgs e)
		{
			FoundFiles.Add(e.FileName);
		}

		private FileSearcher CreateSearcher(CancellationToken ct, PluginType type)
		{
			return new FileSearcher(ct, _pluginMgr.GetProcessor(type));
		}
	}
}
