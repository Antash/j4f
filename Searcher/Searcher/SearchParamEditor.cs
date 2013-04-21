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
						PlugName = cbPlugin.SelectedItem.ToString(),
						SearchInHiden = cbFollowHidden.Checked,
						IsRecursive = cbRecursive.Checked
					};
			}
			set
			{
				tbRootDir.Text = value.RootDir;
				tbSearchPattern.Text = value.SearchPattern;
				cbPlugin.SelectedItem = value.PlugName;
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
