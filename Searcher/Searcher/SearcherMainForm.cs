using System;
using System.Collections;
using System.Collections.Generic;
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

		private readonly IList<Tuple<int, string>> _foundFiles;
		private IList<int> _filter; 
 
		public SearcherMainForm(SearchManager manager)
		{
			InitializeComponent();

			_foundFiles = new List<Tuple<int, string>>();
			_filter = new List<int>();

			_sm = manager;
			_sm.OnFileFound += _sm_OnFileFound;
		}

		private void SearcherMainForm_Load(object sender, EventArgs e)
		{
			dgwResult.CellValueNeeded += dgwResult_CellValueNeeded;

			dgwWorkers.DataSource = _sm.SearchWorkers;
			dgwWorkers.Columns[0].DisplayIndex = dgwWorkers.Columns.Count - 1;
			dgwWorkers.Columns[1].Visible = false;

			searchParamEditor.cbPlugin.DataSource = _sm.PluginList;
		}

		void _sm_OnFileFound(object sender, FileFoundArgs e)
		{
			lock (((ICollection)_foundFiles).SyncRoot)
			{
				_foundFiles.Add(new Tuple<int, string>(e.SearcherId, e.FileName));
			}
			if (dgwResult.RowCount < PossibleLastDisplayedRowCount() && dgwResult.InvokeRequired)
				dgwResult.Invoke((MethodInvoker)delegate
					{
						dgwResult.RowCount++;
					});
		}

		void dgwResult_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			lock (((ICollection) _foundFiles).SyncRoot)
			{
				var filt = _foundFiles.Where(f => _filter.Contains(f.Item1)).ToList();
				if (filt.Count > e.RowIndex)
				{
					e.Value = filt[e.RowIndex].Item2;
				}
			}
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

		private void dgwWorkers_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
				return;
			var worker = (SearchWorker)dgwWorkers.Rows[e.RowIndex].DataBoundItem;
			if (e.ColumnIndex == 0)
				_sm.TerminateSearch(worker);
		}

		private void dgwWorkers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			var worker = (SearchWorker)e.Row.DataBoundItem;
			_sm.TerminateSearch(worker);
			DeleteWorkerResult(worker.Id);
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

		private void dgwResult_Scroll(object sender, ScrollEventArgs e)
		{
			if (dgwResult.RowCount < PossibleLastDisplayedRowCount() &&
				dgwResult.RowCount < ResultCount())
				dgwResult.RowCount++;
		}

		private int ResultCount()
		{
			return dgwWorkers.SelectedRows.Cast<DataGridViewRow>().Sum(w =>
				((SearchWorker) w.DataBoundItem).FilesFound);
		}

		private void dgwResult_Resize(object sender, EventArgs e)
		{
			if (dgwResult.RowCount < PossibleLastDisplayedRowCount() &&
				dgwResult.RowCount < ResultCount())
				dgwResult.RowCount++;
		}

		private int PossibleLastDisplayedRowCount()
		{
			return dgwResult.DisplayRectangle.Height/dgwResult.RowTemplate.Height + dgwResult.FirstDisplayedScrollingRowIndex + 1;
		}

		private void dgwWorkers_SelectionChanged(object sender, EventArgs e)
		{
			if (dgwWorkers.SelectedRows.Count > 0)
			{
				searchParamEditor.SearchParameters = ((SearchWorker) dgwWorkers.SelectedRows[0].DataBoundItem).Parameter;
				FilterResult(dgwWorkers.SelectedRows.Cast<DataGridViewRow>().Select(r => ((SearchWorker) r.DataBoundItem).Id));
			}
		}

		private void FilterResult(IEnumerable<int> ids)
		{
			_filter = new List<int>(ids);
			dgwResult.Rows.Clear();
			dgwResult.RowCount = Math.Min(ResultCount(), PossibleLastDisplayedRowCount());
		}

		private void DeleteWorkerResult(int id)
		{
			foreach (var fl in _foundFiles.Where(f => f.Item1 == id).ToList())
			{
				_foundFiles.Remove(fl);
			}
			dgwResult.Rows.Clear();
		}

		private void bSearch_Click(object sender, EventArgs e)
		{
			_sm.StartSearch(searchParamEditor.SearchParameters);
		}
	}
}
