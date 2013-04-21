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
	public partial class SearchParamEditor : UserControl
	{
		public SearchParamEditor()
		{
			InitializeComponent();
		}

		public FileSearchParam SearchParameters
		{
			get
			{
				return new FileSearchParam
					{
						RootDir = tbRootDir.Text,
						SearchPattern = tbSearchPattern.Text
					};
			}
			set
			{
				tbRootDir.Text = value.RootDir;
				tbSearchPattern.Text = value.SearchPattern;
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
