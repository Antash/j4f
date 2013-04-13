using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SearcherCore
{
	class PluginManager
	{
		[ImportMany]
		public IEnumerable<ISearcher> Searchers { get; set; }
	}
}
