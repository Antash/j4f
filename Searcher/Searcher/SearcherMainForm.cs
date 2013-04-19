using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
			_sm.OnInvalidate += _sm_OnInvalidate;

			dgwResult.DataSource = _sm.FoundFiles;
			dgwResult.Columns[0].Visible = false;

			dgwWorkers.DataSource = _sm.SearchWorkers;
			dgwWorkers.Columns[0].DisplayIndex = dgwWorkers.Columns.Count - 1;
			dgwWorkers.Columns[1].Visible = false;

			if (tscbSelPl.ComboBox != null) 
				tscbSelPl.ComboBox.DataSource = _sm.PluginList;
		}

		void _sm_OnInvalidate()
		{
			if (InvokeRequired)
				dgwResult.Invoke(new MethodInvoker(() =>
					{
						dgwWorkers.InvalidateColumn(3);
						dgwWorkers.InvalidateColumn(4);
						dgwResult.InvalidateColumn(1);
					}));
		}

		private void bSearch_Click(object sender, EventArgs e)
		{
			_sm.StartSearch(new SearchManager.FileSearchParam
			{
				RootDir = tbRootDir.Text,
				SearchPattern = tbSearchPattern.Text,
				PlugType = tscbSelPl.SelectedIndex
			});
		}

		private void tsbLoadPlugins_Click(object sender, EventArgs e)
		{
			fbdPlugin.SelectedPath = Application.StartupPath;
			if (fbdPlugin.ShowDialog() == DialogResult.OK)
			{
				var loadedPlug = _sm.LoadPlugins(fbdPlugin.SelectedPath);
				MessageBox.Show(loadedPlug == 0 ?
					"No plugins loaded!" :
					String.Format("{0} plugins loaded!", loadedPlug));
			}
		}

		private void bSelDir_Click(object sender, EventArgs e)
		{
			if (fbdSearch.ShowDialog() == DialogResult.OK)
			{
				tbRootDir.Text = fbdSearch.SelectedPath;
			}
		}

		private void dgwWorkers_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0 && e.RowIndex != -1)
			{
				var worker = _sm.SearchWorkers.SingleOrDefault(w =>
					w.Id == (int)dgwWorkers.Rows[e.RowIndex].Cells[1].Value);
				if (worker != null)
					_sm.TerminateSearch(worker.Id);
			}
			else
			{
				if (e.RowIndex != -1)
					_sm.ApplyFilter((int)dgwWorkers.Rows[e.RowIndex].Cells[1].Value);
			}
		}

		private void tsbHelp_Click(object sender, EventArgs e)
		{
			if (File.Exists("readme.txt"))
				Process.Start("readme.txt");
		}

		private void dgwWorkers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			var wid = (int)e.Row.Cells[1].Value;
			_sm.TerminateSearch(wid);
			_sm.ClearResult(wid);
		}

		private void dgwResult_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			var selectedDir = Path.GetDirectoryName(dgwResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
			if (selectedDir != null && Directory.Exists(selectedDir))
				new Process
				{
					StartInfo =
						{
							UseShellExecute = true,
							FileName = @"explorer",
							Arguments = selectedDir
						}
				}.Start();
		}
	}
}
