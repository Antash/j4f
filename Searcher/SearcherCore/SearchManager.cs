using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearcherExtensibility;

namespace SearcherCore
{
	class SearchManager
	{
		private static readonly PluginManager PluginMgr = new PluginManager();

		public static int LoadPlugins(string path)
		{
			return PluginMgr.LoadPlugins(path);
		}

		public static string[] GetPluginList()
		{
			return PluginMgr.GetPluginList().Select(p => p.ToString()).ToArray();
		}

		public static FileSearcher CreateSearcher(int type)
		{
			return new FileSearcher(PluginMgr.GetProcessor((PluginType) type));
		}
	}
}
