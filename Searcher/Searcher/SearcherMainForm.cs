using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SearcherCore;

namespace Searcher
{
	public partial class SearcherMainForm : Form
	{
		public SearcherMainForm()
		{
			InitializeComponent();
		}

		private void bSearch_Click(object sender, EventArgs e)
		{
			var sf = new FileSearcher();
			sf.OnFileFound += sf_OnFileFound;
			new Task(() => { sf.Search(@"C:\Users", textBox1.Text); }).Start();
		}

		void sf_OnFileFound(object sender, FileFoundArgs e)
		{
			listView1.Invoke(new MethodInvoker(delegate()
				{ listView1.Items.Add(e.FileName); }));
		}
	}
}
