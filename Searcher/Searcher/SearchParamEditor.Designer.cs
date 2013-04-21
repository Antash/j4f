namespace Searcher
{
	partial class SearchParamEditor
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbSearchPattern = new System.Windows.Forms.TextBox();
			this.lSearchPattern = new System.Windows.Forms.Label();
			this.tbRootDir = new System.Windows.Forms.TextBox();
			this.bSelDir = new System.Windows.Forms.Button();
			this.fbdSearch = new System.Windows.Forms.FolderBrowserDialog();
			this.cbIgnoreCase = new System.Windows.Forms.CheckBox();
			this.cbPlugin = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cbRecursive = new System.Windows.Forms.CheckBox();
			this.cbFollowHidden = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cbFilter = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// tbSearchPattern
			// 
			this.tbSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSearchPattern.Location = new System.Drawing.Point(87, 6);
			this.tbSearchPattern.Name = "tbSearchPattern";
			this.tbSearchPattern.Size = new System.Drawing.Size(227, 22);
			this.tbSearchPattern.TabIndex = 13;
			// 
			// lSearchPattern
			// 
			this.lSearchPattern.AutoSize = true;
			this.lSearchPattern.Location = new System.Drawing.Point(3, 9);
			this.lSearchPattern.Name = "lSearchPattern";
			this.lSearchPattern.Size = new System.Drawing.Size(78, 17);
			this.lSearchPattern.TabIndex = 14;
			this.lSearchPattern.Text = "Search for:";
			// 
			// tbRootDir
			// 
			this.tbRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbRootDir.Location = new System.Drawing.Point(87, 35);
			this.tbRootDir.Name = "tbRootDir";
			this.tbRootDir.Size = new System.Drawing.Size(227, 22);
			this.tbRootDir.TabIndex = 15;
			// 
			// bSelDir
			// 
			this.bSelDir.Location = new System.Drawing.Point(6, 35);
			this.bSelDir.Name = "bSelDir";
			this.bSelDir.Size = new System.Drawing.Size(75, 23);
			this.bSelDir.TabIndex = 16;
			this.bSelDir.Text = "Select dir";
			this.bSelDir.UseVisualStyleBackColor = true;
			this.bSelDir.Click += new System.EventHandler(this.bSelDir_Click);
			// 
			// fbdSearch
			// 
			this.fbdSearch.Description = "Select search folder";
			this.fbdSearch.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.fbdSearch.ShowNewFolderButton = false;
			// 
			// cbIgnoreCase
			// 
			this.cbIgnoreCase.AutoSize = true;
			this.cbIgnoreCase.Location = new System.Drawing.Point(182, 61);
			this.cbIgnoreCase.Name = "cbIgnoreCase";
			this.cbIgnoreCase.Size = new System.Drawing.Size(104, 21);
			this.cbIgnoreCase.TabIndex = 17;
			this.cbIgnoreCase.Text = "Ignore case";
			this.cbIgnoreCase.UseVisualStyleBackColor = true;
			// 
			// cbPlugin
			// 
			this.cbPlugin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPlugin.FormattingEnabled = true;
			this.cbPlugin.Location = new System.Drawing.Point(65, 62);
			this.cbPlugin.Name = "cbPlugin";
			this.cbPlugin.Size = new System.Drawing.Size(104, 24);
			this.cbPlugin.TabIndex = 26;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 65);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 17);
			this.label5.TabIndex = 27;
			this.label5.Text = "Plugin:";
			// 
			// cbRecursive
			// 
			this.cbRecursive.AutoSize = true;
			this.cbRecursive.Checked = true;
			this.cbRecursive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbRecursive.Location = new System.Drawing.Point(182, 115);
			this.cbRecursive.Name = "cbRecursive";
			this.cbRecursive.Size = new System.Drawing.Size(93, 21);
			this.cbRecursive.TabIndex = 28;
			this.cbRecursive.Text = "Recursive";
			this.cbRecursive.UseVisualStyleBackColor = true;
			// 
			// cbFollowHidden
			// 
			this.cbFollowHidden.AutoSize = true;
			this.cbFollowHidden.Location = new System.Drawing.Point(182, 88);
			this.cbFollowHidden.Name = "cbFollowHidden";
			this.cbFollowHidden.Size = new System.Drawing.Size(122, 21);
			this.cbFollowHidden.TabIndex = 29;
			this.cbFollowHidden.Text = "Hidden folders";
			this.cbFollowHidden.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 95);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 17);
			this.label1.TabIndex = 30;
			this.label1.Text = "Filter:";
			// 
			// cbFilter
			// 
			this.cbFilter.FormattingEnabled = true;
			this.cbFilter.Location = new System.Drawing.Point(65, 92);
			this.cbFilter.Name = "cbFilter";
			this.cbFilter.Size = new System.Drawing.Size(104, 24);
			this.cbFilter.TabIndex = 31;
			// 
			// SearchParamEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cbFilter);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbFollowHidden);
			this.Controls.Add(this.cbRecursive);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cbPlugin);
			this.Controls.Add(this.cbIgnoreCase);
			this.Controls.Add(this.bSelDir);
			this.Controls.Add(this.tbRootDir);
			this.Controls.Add(this.lSearchPattern);
			this.Controls.Add(this.tbSearchPattern);
			this.Name = "SearchParamEditor";
			this.Size = new System.Drawing.Size(317, 140);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbSearchPattern;
		private System.Windows.Forms.Label lSearchPattern;
		private System.Windows.Forms.TextBox tbRootDir;
		private System.Windows.Forms.Button bSelDir;
		private System.Windows.Forms.FolderBrowserDialog fbdSearch;
		private System.Windows.Forms.CheckBox cbIgnoreCase;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox cbRecursive;
		private System.Windows.Forms.CheckBox cbFollowHidden;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbFilter;
		internal System.Windows.Forms.ComboBox cbPlugin;

	}
}
