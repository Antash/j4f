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
		private SearchManager _sm;

		private enum GridIndexes
		{
 			Id = 0,
			Stop = 4,
			Status = 3,
		}

		public SearcherMainForm(SearchManager manager)
		{
			InitializeComponent();

			_sm = manager;
			_sm.OnSearchStarted += _sm_OnSearchStarted;
			_sm.OnSearchFinished += _sm_OnSearchFinished;

			tscbSelPl.ComboBox.DataSource = _sm.PluginList;
		}

		void _sm_OnSearchFinished(object sender, EventArgs e)
		{
			var args = (e as SearchManager.SearchStopEventArgs);
			if (args == null)
				return;
			var row = dgwWorkers.Rows.Cast<DataGridViewRow>()
				.Where(r => (int) r.Cells[(int)GridIndexes.Id].Value == args.WorkerId).FirstOrDefault();
			if (row != null)
				row.Cells[(int) GridIndexes.Status].Value = "Stopped";
		}

		void _sm_OnSearchStarted(object sender, EventArgs e)
		{
			var args = (e as SearchManager.SearchStartEventArgs);
			if (args == null)
				return;
			dgwWorkers.Rows.Add(args.WorkerId, args.Details, 0, "Running", "Stop");
		}

		private void bSearch_Click(object sender, EventArgs e)
		{
			_sm.StartSearch(new SearchManager.FileSearchParam() {
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
				if (loadedPlug == 0)
					MessageBox.Show("No plugins loaded!");
				else
				{
					MessageBox.Show(String.Format("{0} plugins loaded!", loadedPlug));
					tscbSelPl.ComboBox.DataSource = null;
					tscbSelPl.ComboBox.DataSource = _sm.PluginList;
				}
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
			if (e.ColumnIndex == 4)
			{
				_sm.TerminateSearch((int) dgwWorkers.Rows[e.RowIndex].Cells[(int) GridIndexes.Id].Value);
			}
			else
			{
 				//TODO filter results in the list
			}
		}

		private void tsbHelp_Click(object sender, EventArgs e)
		{
			//TODO show readme
		}

		private void dgwWorkers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			_sm.TerminateSearch((int) e.Row.Cells[(int)GridIndexes.Id].Value);
		}
	}
}
