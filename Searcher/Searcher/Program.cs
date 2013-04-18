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
