using System;
using System.Windows.Forms;

namespace Searcher
{
    class Program
    {
		[STAThread]
        static void Main(string[] args)
        {
			Console.WriteLine("Started!");

	        var form = new SearcherMainForm();
			Application.EnableVisualStyles();
			Application.Run(form);

			Console.WriteLine("Finished!");
	        //Console.ReadLine();
        }
    }
}
