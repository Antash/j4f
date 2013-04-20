using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SearcherCore;

namespace Searcher
{
	public partial class SearcherMainForm : Form
	{
		private readonly SearchManager _sm;

		private readonly IList<Tuple<int, string>> _foundFiles;
		private const int GridRowsMargine = 10;

		public SearcherMainForm(SearchManager manager)
		{
			InitializeComponent();

			_foundFiles = new List<Tuple<int, string>>();

			_sm = manager;
			_sm.OnFileFound += _sm_OnFileFound;

			//dgwResult.DataSource = _sm.FoundFiles;
			//dgwResult.Columns[0].Visible = false;
			
			dgwResult.Columns.Add("fname", "fname");
			dgwResult.CellValueNeeded += dgwResult_CellValueNeeded;

			dgwWorkers.DataSource = _sm.SearchWorkers;
			dgwWorkers.Columns[0].DisplayIndex = dgwWorkers.Columns.Count - 1;
			dgwWorkers.Columns[1].Visible = false;

			if (tscbSelPl.ComboBox != null)
				tscbSelPl.ComboBox.DataSource = _sm.PluginList;
		}

		void _sm_OnFileFound(object sender, FileFoundArgs e)
		{
			_foundFiles.Add(new Tuple<int, string>(e.SearcherId, e.FileName));

			var visibleRowsCount = dgwResult.DisplayedRowCount(true);
			var firstVisibleRowIndex = dgwResult.FirstDisplayedScrollingRowIndex;
			var lastVisibleRowIndex = (firstVisibleRowIndex + visibleRowsCount) - 1;
			if (dgwResult.RowCount < lastVisibleRowIndex + GridRowsMargine)
				if (dgwResult.InvokeRequired)
					dgwResult.Invoke((MethodInvoker)delegate { dgwResult.RowCount++; });
		}

		void dgwResult_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			e.Value = _foundFiles[e.RowIndex].Item2;
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
			{
				//_sm.ApplyFilter(worker);
				//dgwResult.RowCount = _sm.FoundFiles.Table.Rows.Count;
			}
		}

		private void dgwWorkers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			var worker = (SearchWorker)e.Row.DataBoundItem;
			_sm.TerminateSearch(worker);
			//_sm.ClearResult(worker);
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

		private void tsbHelp_Click(object sender, EventArgs e)
		{
			if (File.Exists("readme.txt"))
				Process.Start("readme.txt");
		}
	}
}
