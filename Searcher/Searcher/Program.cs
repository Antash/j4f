using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using SearcherCore;

namespace Searcher
{
    class Program
    {
        static void Main(string[] args)
        {
			//TODO UI
			//TODO search params
			//TODO multiprocessing/threading

	        var form = new SearcherMainForm();
			Application.EnableVisualStyles();
			//Application.Run(form);

	        var sf = new FileSearcher();
			//sf.Search(@"D:\photo", "2007-35-02_103534");
			sf.Search(@"C:\Users\aashmarin", "aa.aa");
			//sf.Search("*.dll");

	        //var s = new FileSearcher(SearchType.DotNetType);
			//s.Search(@"C:\Windows", "foo");
			//s.Search(@"C:\Users\Антон\Documents\Visual Studio 2012\Projects\j4f\Searcher\Searcher\bin\Debug", "Program");
			//s.Search(@"C:\Program Files\Paint.NET", "Program");

			//var s2 = new FileSearcher(SearchType.XmlTag);
			//s2.Search(@"C:\Users\Антон\Documents\Visual Studio 2012\Projects\j4f\Searcher\Searcher\bin\Debug", "book");

			Console.WriteLine("Finished!");
	        Console.ReadLine();
        }
    }
}
