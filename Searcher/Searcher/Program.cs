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
	        var s = new FileSearcher(SearchType.DotNetType);
			//s.Search(@"C:\Windows", "foo");
			s.Search(@"C:\Users\Антон\Documents\Visual Studio 2012\Projects\j4f\Searcher\Searcher\bin\Debug", "Program");

			var s2 = new FileSearcher(SearchType.XmlTag);
			s2.Search(@"C:\Users\Антон\Documents\Visual Studio 2012\Projects\j4f\Searcher\Searcher\bin\Debug", "book");
	        //s.Search(@"D:\photo", "2007-35-02_103534");
        }
    }
}
