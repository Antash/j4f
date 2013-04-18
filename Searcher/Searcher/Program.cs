using System;
using System.Windows.Forms;
using SearcherCore;

namespace Searcher
{
    class Program
    {
		[STAThread]
        static void Main(string[] args)
        {
			Console.WriteLine("Started!");

	        var form = new SearcherMainForm(new SearchManager());
			Application.EnableVisualStyles();
			Application.Run(form);

			Console.WriteLine("Finished!");
	        Console.ReadLine();
        }
    }
}
