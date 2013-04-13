using System.ComponentModel.Composition;
using SearcherCore;

namespace XMLSearcherPlug
{
	[Export(typeof(ISearcher))]
	public class XmlSearcher : ISearcher
    {
		public void Search()
		{
			throw new System.NotImplementedException();
		}
    }
}
