using System.ComponentModel.Composition;
using SearcherCore;

namespace NETSearcherPlug
{
	[Export(typeof(ISearcher))]
	public class NetSearcher : ISearcher
    {
		public void Search()
		{
			throw new System.NotImplementedException();
		}
    }
}
