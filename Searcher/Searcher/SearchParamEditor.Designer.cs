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
			this.cbPlugin = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cbRecursive = new System.Windows.Forms.CheckBox();
			this.cbFollowHidden = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// tbSearchPattern
			// 
			this.tbSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSearchPattern.Location = new System.Drawing.Point(87, 6);
			this.tbSearchPattern.Name = "tbSearchPattern";
			this.tbSearchPattern.Size = new System.Drawing.Size(336, 22);
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
			this.tbRootDir.Size = new System.Drawing.Size(336, 22);
			this.tbRootDir.TabIndex = 15;
			// 
			// bSelDir
			// 
			this.bSelDir.Location = new System.Drawing.Point(6, 35);
			this.bSelDir.Name = "bSelDir";
			this.bSelDir.Size = new System.Drawing.Size(75, 23);
			this.bSelDir.TabIndex = 16;
			this.bSelDir.Text = "Folder:";
			this.bSelDir.UseVisualStyleBackColor = true;
			this.bSelDir.Click += new System.EventHandler(this.bSelDir_Click);
			// 
			// fbdSearch
			// 
			this.fbdSearch.Description = "Select search folder";
			this.fbdSearch.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.fbdSearch.ShowNewFolderButton = false;
			// 
			// cbPlugin
			// 
			this.cbPlugin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPlugin.FormattingEnabled = true;
			this.cbPlugin.Location = new System.Drawing.Point(65, 62);
			this.cbPlugin.Name = "cbPlugin";
			this.cbPlugin.Size = new System.Drawing.Size(134, 24);
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
			this.cbRecursive.Location = new System.Drawing.Point(333, 64);
			this.cbRecursive.Name = "cbRecursive";
			this.cbRecursive.Size = new System.Drawing.Size(93, 21);
			this.cbRecursive.TabIndex = 28;
			this.cbRecursive.Text = "Recursive";
			this.cbRecursive.UseVisualStyleBackColor = true;
			// 
			// cbFollowHidden
			// 
			this.cbFollowHidden.AutoSize = true;
			this.cbFollowHidden.Location = new System.Drawing.Point(205, 65);
			this.cbFollowHidden.Name = "cbFollowHidden";
			this.cbFollowHidden.Size = new System.Drawing.Size(122, 21);
			this.cbFollowHidden.TabIndex = 29;
			this.cbFollowHidden.Text = "Hidden folders";
			this.cbFollowHidden.UseVisualStyleBackColor = true;
			// 
			// SearchParamEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cbFollowHidden);
			this.Controls.Add(this.cbRecursive);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cbPlugin);
			this.Controls.Add(this.bSelDir);
			this.Controls.Add(this.tbRootDir);
			this.Controls.Add(this.lSearchPattern);
			this.Controls.Add(this.tbSearchPattern);
			this.Name = "SearchParamEditor";
			this.Size = new System.Drawing.Size(426, 93);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbSearchPattern;
		private System.Windows.Forms.Label lSearchPattern;
		private System.Windows.Forms.TextBox tbRootDir;
		private System.Windows.Forms.Button bSelDir;
		private System.Windows.Forms.FolderBrowserDialog fbdSearch;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox cbRecursive;
		private System.Windows.Forms.CheckBox cbFollowHidden;
		internal System.Windows.Forms.ComboBox cbPlugin;

	}
}
