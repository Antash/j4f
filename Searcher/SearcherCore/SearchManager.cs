using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearcherExtensibility;

namespace SearcherCore
{
	public class SearchManager
	{
		public class FileSearchParam
		{
			public int PlugType { get; set; }
			public string RootDir { get; set; }
			public string SearchPattern { get; set; }
			public bool IgnoreCase { get; set; }
			public DateTime? CreationTimeFrom { get; set; }
			public DateTime? CreationTimeTo { get; set; }
			public long? SizeFrom { get; set; }
			public long? SizeTo { get; set; }
		}

		private readonly PluginManager _pluginMgr = new PluginManager();

		public int LoadPlugins(string path)
		{
			return _pluginMgr.LoadPlugins(path);
		}

		public string[] PluginList
		{
			get
			{
				return _pluginMgr.GetPluginList().Concat(new[] { PluginType.NoPlugin })
						.OrderBy(p => p)
						.Select(p => p.ToString()).ToArray();
			}
		}

		public string[] FoundFiles { get; set; }

		public async void StartSearch(FileSearchParam param)
		{
			var searcher = CreateSearcher((PluginType)param.PlugType);
			searcher.OnFileFound += searcher_OnFileFound;
			await searcher.Search(param);
			//new Task(() => ).Start();
		}

		private void searcher_OnFileFound(object sender, FileFoundArgs e)
		{
		}

		private FileSearcher CreateSearcher(PluginType type)
		{
			return new FileSearcher(_pluginMgr.GetProcessor(type));
		}
	}
}
