using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SearcherCore;

namespace Searcher
{
	[ComplexBindingProperties("DataSource")]
	public partial class SearchParamEditor : UserControl
	{
		public SearchParamEditor()
		{
			InitializeComponent();
		}

		private void bSelDir_Click(object sender, EventArgs e)
		{
			if (fbdSearch.ShowDialog() == DialogResult.OK)
			{
				tbRootDir.Text = fbdSearch.SelectedPath;
			}
		}

		private void bSearch_Click(object sender, EventArgs e)
		{
			//SearchStart(new FileSearchParam
			{
				//	RootDir = tbRootDir.Text,
				//	SearchPattern = tbSearchPattern.Text,
				//	PlugName = tscbSelPl.SelectedItem.ToString()
			}
		}

	}
}
