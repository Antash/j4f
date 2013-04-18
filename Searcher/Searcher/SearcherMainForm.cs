using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SearcherCore;

using WorkTabInd = SearcherCore.SearchManager.WorkerTabInd;

namespace Searcher
{
	public partial class SearcherMainForm : Form
	{
		private readonly SearchManager _sm;

		public SearcherMainForm(SearchManager manager)
		{
			InitializeComponent();

			_sm = manager;
			_sm.OnFileFound += _sm_OnFileFound;

			dgwResult.DataSource = _sm.FoundFiles;
			dgwResult.Columns[0].Visible = false;

			dgwWorkers.DataSource = _sm.SearchWorkers;
			dgwWorkers.Columns[0].DisplayIndex = dgwWorkers.Columns.Count - 1;
			dgwWorkers.Columns[1].Visible = false;

			tscbSelPl.ComboBox.DataSource = _sm.PluginList;
		}

		void _sm_OnFileFound()
		{
			dgwResult.Invoke(new MethodInvoker(() =>
				{
					dgwWorkers.InvalidateColumn(3);
					dgwResult.InvalidateColumn(1);
				}));
		}

		private void bSearch_Click(object sender, EventArgs e)
		{
			_sm.StartSearch(new SearchManager.FileSearchParam()
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
			if (e.ColumnIndex == 0)
			{
				var worker = _sm.SearchWorkers.SingleOrDefault(w =>
					w.Id == (int) dgwWorkers.Rows[e.RowIndex].Cells[1].Value);
				if (worker != null)
					_sm.TerminateSearch(worker.Id);
			}
			else
			{
				if (e.RowIndex > 0)
					_sm.FoundFiles.DefaultView.RowFilter = string.Format("wid = {0}",
						(int)dgwWorkers.Rows[e.RowIndex].Cells[(int)WorkTabInd.Id].Value);
			}
		}

		private void tsbHelp_Click(object sender, EventArgs e)
		{
			//TODO show readme
		}

		private void dgwWorkers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			var wid = (int)e.Row.Cells[1].Value;
			foreach(var row in _sm.FoundFiles.Select(string.Format("wid = {0}", wid)))
			{
				_sm.FoundFiles.Rows.Remove(row);
			}
			_sm.TerminateSearch(wid);
		}

		private void dgwResult_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			var expl = new Process
				{
					StartInfo =
						{
							UseShellExecute = true,
							FileName = @"explorer",
							Arguments = Path.GetDirectoryName(
								dgwResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
						}
				};
			expl.Start();
		}
	}
}
