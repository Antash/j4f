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
		private List<string> loadedPlugins;

		public SearcherMainForm()
		{
			InitializeComponent();

			loadedPlugins = new List<string>();
			loadedPlugins.Add("No Plugins");
			tscbSelPl.ComboBox.DataSource = loadedPlugins;
		}

		private void bSearch_Click(object sender, EventArgs e)
		{
			//var sf = new FileSearcher();
			//sf.OnFileFound += sf_OnFileFound;
			//new Task(() => { sf.Search(@"C:\Users", tbSearchPattern.Text); }).Start();
		}

		//void sf_OnFileFound(object sender, FileFoundArgs e)
		//{
		//	//lvResults.Invoke(new MethodInvoker(delegate()
		//	//	{ lvResults.Items.Add(e.FileName); }));
		//}

		private void tsbLoadPlugins_Click(object sender, EventArgs e)
		{
			//var pluginSelector = new FolderBrowserDialog();
			//pluginSelector.SelectedPath = Application.StartupPath;
			//if (pluginSelector.ShowDialog() == DialogResult.OK)
			//{
			//	var loadedPlug = SearchManager.LoadPlugins(pluginSelector.SelectedPath);
			//	if (loadedPlug == 0)
			//	{
			//		MessageBox.Show("No plugins loaded!");
			//	}
			//	else
			//	{
			//		MessageBox.Show(String.Format("{0} plugins loaded!", loadedPlug));
			//		loadedPlugins.AddRange(SearchManager.GetPluginList());
			//		tscbSelPl.ComboBox.DataSource = null;
			//		tscbSelPl.ComboBox.DataSource = loadedPlugins;
			//	}
			//}
		}
	}
}
