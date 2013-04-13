using System.ComponentModel.Composition;
using System.Xml;
using SearcherCore;

namespace XMLSearcherPlug
{
	[Export(typeof(ISearcher))]
	public class XmlSearcher : ISearcher
    {
		public SearcherType GetSearcherType()
		{
			return SearcherType.XmlSearcher;
		}

		public void Search()
		{
			var doc = new XmlDocument();
			doc.Load("books.xml");
			if (doc.HasChildNodes)
				foreach (XmlNode node in doc.ChildNodes)
				{
					//node.
				}
		}
    }
}
