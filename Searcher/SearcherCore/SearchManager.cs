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

		private static readonly PluginManager PluginMgr = new PluginManager();

		public static int LoadPlugins(string path)
		{
			return PluginMgr.LoadPlugins(path);
		}

		public static string[] GetPluginList()
		{
			return PluginMgr.GetPluginList().Concat(new [] { PluginType.NoPlugin })
				.OrderBy(p => p)
				.Select(p => p.ToString()).ToArray();
		}

		public void StartSearch(FileSearchParam param)
		{
			var searcher = CreateSearcher((PluginType)param.PlugType);
			searcher.OnFileFound += searcher_OnFileFound;
			new Task(() => searcher.Search(param)).Start();
		}

		private void searcher_OnFileFound(object sender, FileFoundArgs e)
		{
		}

		private static FileSearcher CreateSearcher(PluginType type)
		{
			return new FileSearcher(PluginMgr.GetProcessor(type));
		}
	}
}
