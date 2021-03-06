﻿using System;
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

		private readonly object _syncRoot;
		private IList<Tuple<int, string>> _foundFiles;
		private IList<int> _filter;

		private const int ResultRowMargine = 3;

		public SearcherMainForm(SearchManager manager)
		{
			InitializeComponent();

			_syncRoot = new object();
			_foundFiles = new List<Tuple<int, string>>();
			_filter = new List<int>();

			_sm = manager;
			_sm.OnFileFound += _sm_OnFileFound;
		}

		private void SearcherMainForm_Load(object sender, EventArgs e)
		{
			dgwWorkers.DataSource = _sm.SearchWorkers;
			dgwWorkers.Columns[0].DisplayIndex = dgwWorkers.Columns.Count - 1;
			dgwWorkers.Columns[1].Visible = false;

			searchParamEditor.cbPlugin.DataSource = _sm.PluginList;
		}

		private void _sm_OnFileFound(object sender, FileFoundArgs e)
		{
			lock (_syncRoot)
			{
				_foundFiles.Add(new Tuple<int, string>(e.SearcherId, e.FileName));
			}
			if (NewRowNeeded() && dgwResult.InvokeRequired)
				dgwResult.Invoke((MethodInvoker)delegate
					{
						dgwResult.RowCount++;
					});
		}

		#region Result grid event handlers

		void dgwResult_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			lock (_syncRoot)
			{
				var filt = _foundFiles.Where(f => _filter.Contains(f.Item1)).ToList();
				if (filt.Count > e.RowIndex)
				{
					e.Value = filt[e.RowIndex].Item2;
				}
			}
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

		private void dgwResult_Resize(object sender, EventArgs e)
		{
			ActualizeRowCount();
		}

		private void dgwResult_Scroll(object sender, ScrollEventArgs e)
		{
			if (NewRowNeeded())
				dgwResult.RowCount++;
		}

		#endregion

		#region Workers grid event handlers

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

		private void dgwWorkers_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			ActualizeRowCount();
		}

		private void dgwWorkers_SelectionChanged(object sender, EventArgs e)
		{
			if (dgwWorkers.SelectedRows.Count > 0)
			{
				searchParamEditor.SearchParameters = ((SearchWorker)dgwWorkers.SelectedRows[0].DataBoundItem).Parameter;
				FilterResult(dgwWorkers.SelectedRows.Cast<DataGridViewRow>().Select(r => ((SearchWorker)r.DataBoundItem).Id));
			}
		}

		#endregion

		private bool NewRowNeeded()
		{
			return dgwResult.RowCount < PossibleLastDisplayedRowCount() && dgwResult.RowCount < FilteredResultCount();
		}

		private void ActualizeRowCount()
		{
			dgwResult.RowCount = Math.Min(FilteredResultCount(), PossibleLastDisplayedRowCount());
		}

		private int FilteredResultCount()
		{
			return dgwWorkers.SelectedRows.Cast<DataGridViewRow>().Sum(w =>
				((SearchWorker) w.DataBoundItem).FilesFound);
		}

		private int PossibleLastDisplayedRowCount()
		{
			return dgwResult.DisplayRectangle.Height/dgwResult.RowTemplate.Height + 
				dgwResult.FirstDisplayedScrollingRowIndex + ResultRowMargine;
		}

		private void FilterResult(IEnumerable<int> ids)
		{
			_filter = new List<int>(ids);
			dgwResult.Rows.Clear();
			ActualizeRowCount();
		}

		private void DeleteWorkerResult(int id)
		{
			lock (_syncRoot)
			{
				_foundFiles = _foundFiles.Where(f => f.Item1 != id).ToList();
			}
			dgwResult.Rows.Clear();
		}

		private void tsbHelp_Click(object sender, EventArgs e)
		{
			if (File.Exists("readme.txt"))
				Process.Start("readme.txt");
		}

		private void bSearch_Click(object sender, EventArgs e)
		{
			_sm.StartSearch(searchParamEditor.SearchParameters);
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
	}
}
