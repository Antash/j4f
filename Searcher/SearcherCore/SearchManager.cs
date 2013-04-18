﻿using System;
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
		#region Nested types

		public class FileSearchParam
		{
			public FileSearchParam()
			{
			}

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

		#endregion

		private readonly PluginManager _pluginMgr = new PluginManager();
		private IDictionary<string, CancellationTokenSource> workerTokenSources { get; set; }

		public IList<string> FoundFiles { get; set; }

		public SearchManager()
		{
			workerTokenSources = new Dictionary<string, CancellationTokenSource>();
			FoundFiles = new List<string>();
		}

		public int LoadPlugins(string path)
		{
			return _pluginMgr.LoadPlugins(path);
		}

		#region Manager events declaration

		public class SearchStartEventArgs : EventArgs
		{
			public string Details { get; set; }
			public string WorkerId { get; set; }
		}

		public class SearchStopEventArgs : EventArgs
		{
			public string WorkerId { get; set; }
		}

		public delegate void SearchDelegate(object sender, EventArgs e);
		public event SearchDelegate OnSearchStarted;
		public event SearchDelegate OnSearchFinished;

		private void SearchStart(string workerId, string workerDetails)
		{
			if (OnSearchStarted != null)
			{
				OnSearchStarted(this, new SearchStartEventArgs() { 
					Details = workerDetails,
					WorkerId = workerId
				});
			}
		}

		private void SearchFinish(string workerId)
		{
			if (OnSearchFinished != null)
			{
				OnSearchFinished(this, new SearchStopEventArgs() { WorkerId = workerId });
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

		public async void StartSearch(FileSearchParam param)
		{
			var workerTokenSource = new CancellationTokenSource();
			var token = workerTokenSource.Token;

			var searcher = CreateSearcher(token, (PluginType)param.PlugType);
			searcher.OnFileFound += searcher_OnFileFound;

			var workerId = Guid.NewGuid().ToString();
			workerTokenSources.Add(workerId, workerTokenSource);

			SearchStart(workerId, param.ToString());
			await Task.Factory.StartNew(() => searcher.Search(param));
			SearchFinish(workerId);

			workerTokenSources.Remove(workerId);
		}

		public void TerminateSearch(string workerId)
		{
			if (workerTokenSources.ContainsKey(workerId))
				workerTokenSources[workerId].Cancel();
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
