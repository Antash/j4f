using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SearcherCore;

namespace Searcher
{
	public partial class SearcherMainForm : Form
	{
		private readonly SearchManager _sm;

		public SearcherMainForm(SearchManager manager)
		{
			InitializeComponent();

			_sm = manager;

			dgwResult.DataSource = _sm.FoundFiles;
			dgwResult.Columns[0].Visible = false;

			dgwWorkers.DataSource = _sm.SearchWorkers;
			dgwWorkers.Columns[0].DisplayIndex = dgwWorkers.Columns.Count - 1;
			dgwWorkers.Columns[1].Visible = false;

			if (tscbSelPl.ComboBox != null)
				tscbSelPl.ComboBox.DataSource = _sm.PluginList;
		}

		private void bSearch_Click(object sender, EventArgs e)
		{
			_sm.StartSearch(new FileSearchParam
			{
				RootDir = tbRootDir.Text,
				SearchPattern = tbSearchPattern.Text,
				PlugName = tscbSelPl.SelectedItem.ToString()
			});
		}

		private void tsbLoadPlugins_Click(object sender, EventArgs e)
		{
			fbdPlugin.SelectedPath = Application.StartupPath;
			if (fbdPlugin.ShowDialog() == DialogResult.OK)
			{
				var loadedPlug = _sm.LoadPlugins(fbdPlugin.SelectedPath);
				MessageBox.Show(string.Format("{0} plugins loaded", loadedPlug));
			}
		}

		private void bSelDir_Click(object sender, EventArgs e)
		{
			if (fbdSearch.ShowDialog() == DialogResult.OK)
			{
				tbRootDir.Text = fbdSearch.SelectedPath;
			}
		}

		private void dgwWorkers_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
				return;
			var worker = (SearchWorker)dgwWorkers.Rows[e.RowIndex].DataBoundItem;
			if (e.ColumnIndex == 0)
				_sm.TerminateSearch(worker);
			else
				_sm.ApplyFilter(worker);
		}

		private void tsbHelp_Click(object sender, EventArgs e)
		{
			if (File.Exists("readme.txt"))
				Process.Start("readme.txt");
		}

		private void dgwWorkers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			var worker = (SearchWorker)e.Row.DataBoundItem;
			_sm.TerminateSearch(worker);
			_sm.ClearResult(worker);
		}

		private void dgwResult_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			new Process
			{
				StartInfo =
					{
						UseShellExecute = true,
						FileName = @"explorer",
						Arguments = string.Format("/select, \"{0}\"", dgwResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
					}
			}.Start();
		}
	}
}
