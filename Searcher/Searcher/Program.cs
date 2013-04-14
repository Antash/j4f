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
	        var s = new FileSearcher();
			s.Search(@"D:\photo", "2007-35-02_103534");
        }
    }
}
