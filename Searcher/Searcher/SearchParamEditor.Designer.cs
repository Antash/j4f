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
			this.SuspendLayout();
			// 
			// tbSearchPattern
			// 
			this.tbSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSearchPattern.Location = new System.Drawing.Point(87, 6);
			this.tbSearchPattern.Name = "tbSearchPattern";
			this.tbSearchPattern.Size = new System.Drawing.Size(160, 22);
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
			this.tbRootDir.Size = new System.Drawing.Size(160, 22);
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
			// SearchParamEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.bSelDir);
			this.Controls.Add(this.tbRootDir);
			this.Controls.Add(this.lSearchPattern);
			this.Controls.Add(this.tbSearchPattern);
			this.Name = "SearchParamEditor";
			this.Size = new System.Drawing.Size(250, 64);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbSearchPattern;
		private System.Windows.Forms.Label lSearchPattern;
		private System.Windows.Forms.TextBox tbRootDir;
		private System.Windows.Forms.Button bSelDir;
		private System.Windows.Forms.FolderBrowserDialog fbdSearch;

	}
}
