using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SearcherCore;

namespace Searcher
{
    class Program
    {
        static void Main(string[] args)
        {
			var doc = new XmlDocument();
			doc.Load("books.xml");
			if (doc.HasChildNodes)
				foreach (XmlNode node in doc.ChildNodes)
				{
					//node.
				}

	        var s = new FileSearcher();
	        s.Search();
        }
    }
}
