using System;
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
						SearchPattern = tbSearchPattern.Text,
						PlugName = cbPlugin.SelectedText,
						Filter = cbFilter.SelectedText,
						IgnoreCase = cbIgnoreCase.Checked,
						SearchInHiden = cbFollowHidden.Checked,
						IsRecursive = cbRecursive.Checked
					};
			}
			set
			{
				tbRootDir.Text = value.RootDir;
				tbSearchPattern.Text = value.SearchPattern;
				cbPlugin.SelectedText = value.PlugName;
				cbFilter.SelectedText = value.Filter;
				cbIgnoreCase.Checked = value.IgnoreCase;
				cbFollowHidden.Checked = value.SearchInHiden;
				cbRecursive.Checked = value.IsRecursive;
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
