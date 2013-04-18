﻿using System;
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

		public SearcherMainForm()
		{
			InitializeComponent();

			_sm = new SearchManager();
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
	}
}
