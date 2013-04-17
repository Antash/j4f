namespace Searcher
{
	partial class SearcherMainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearcherMainForm));
			this.bSearch = new System.Windows.Forms.Button();
			this.lvResults = new System.Windows.Forms.ListView();
			this.tbSearchPattern = new System.Windows.Forms.TextBox();
			this.lDir = new System.Windows.Forms.Label();
			this.lSearchPattern = new System.Windows.Forms.Label();
			this.dgwWorkers = new System.Windows.Forms.DataGridView();
			this.swinfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.control = new System.Windows.Forms.DataGridViewButtonColumn();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbLoadPlugins = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tslSelPl = new System.Windows.Forms.ToolStripLabel();
			this.tscbSelPl = new System.Windows.Forms.ToolStripComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.dgwWorkers)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bSearch
			// 
			this.bSearch.Location = new System.Drawing.Point(339, 8);
			this.bSearch.Name = "bSearch";
			this.bSearch.Size = new System.Drawing.Size(75, 23);
			this.bSearch.TabIndex = 0;
			this.bSearch.Text = "Find!";
			this.bSearch.UseVisualStyleBackColor = true;
			this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
			// 
			// lvResults
			// 
			this.lvResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvResults.Location = new System.Drawing.Point(0, 0);
			this.lvResults.Name = "lvResults";
			this.lvResults.Size = new System.Drawing.Size(424, 280);
			this.lvResults.TabIndex = 1;
			this.lvResults.UseCompatibleStateImageBehavior = false;
			this.lvResults.View = System.Windows.Forms.View.List;
			// 
			// tbSearchPattern
			// 
			this.tbSearchPattern.Location = new System.Drawing.Point(96, 8);
			this.tbSearchPattern.Name = "tbSearchPattern";
			this.tbSearchPattern.Size = new System.Drawing.Size(237, 22);
			this.tbSearchPattern.TabIndex = 2;
			// 
			// lDir
			// 
			this.lDir.AutoSize = true;
			this.lDir.Location = new System.Drawing.Point(12, 40);
			this.lDir.Name = "lDir";
			this.lDir.Size = new System.Drawing.Size(24, 17);
			this.lDir.TabIndex = 5;
			this.lDir.Text = "dir";
			// 
			// lSearchPattern
			// 
			this.lSearchPattern.AutoSize = true;
			this.lSearchPattern.Location = new System.Drawing.Point(12, 11);
			this.lSearchPattern.Name = "lSearchPattern";
			this.lSearchPattern.Size = new System.Drawing.Size(78, 17);
			this.lSearchPattern.TabIndex = 6;
			this.lSearchPattern.Text = "Search for:";
			// 
			// dgwWorkers
			// 
			this.dgwWorkers.AllowUserToAddRows = false;
			this.dgwWorkers.AllowUserToDeleteRows = false;
			this.dgwWorkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgwWorkers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.swinfo,
            this.control});
			this.dgwWorkers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgwWorkers.Location = new System.Drawing.Point(0, 0);
			this.dgwWorkers.Name = "dgwWorkers";
			this.dgwWorkers.ReadOnly = true;
			this.dgwWorkers.RowTemplate.Height = 24;
			this.dgwWorkers.Size = new System.Drawing.Size(424, 74);
			this.dgwWorkers.TabIndex = 8;
			// 
			// swinfo
			// 
			this.swinfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.swinfo.Frozen = true;
			this.swinfo.HeaderText = "Search worker";
			this.swinfo.Name = "swinfo";
			this.swinfo.ReadOnly = true;
			this.swinfo.Width = 124;
			// 
			// control
			// 
			this.control.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.control.HeaderText = "Control";
			this.control.Name = "control";
			this.control.ReadOnly = true;
			this.control.Width = 59;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadPlugins,
            this.toolStripSeparator1,
            this.tslSelPl,
            this.tscbSelPl});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(424, 26);
			this.toolStrip1.TabIndex = 9;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbLoadPlugins
			// 
			this.tsbLoadPlugins.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbLoadPlugins.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadPlugins.Image")));
			this.tsbLoadPlugins.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbLoadPlugins.Name = "tsbLoadPlugins";
			this.tsbLoadPlugins.Size = new System.Drawing.Size(91, 23);
			this.tsbLoadPlugins.Text = "Load plugins";
			this.tsbLoadPlugins.Click += new System.EventHandler(this.tsbLoadPlugins_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
			// 
			// tslSelPl
			// 
			this.tslSelPl.Name = "tslSelPl";
			this.tslSelPl.Size = new System.Drawing.Size(108, 23);
			this.tslSelPl.Text = "Selected plugin:";
			// 
			// tscbSelPl
			// 
			this.tscbSelPl.Name = "tscbSelPl";
			this.tscbSelPl.Size = new System.Drawing.Size(121, 26);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lSearchPattern);
			this.panel1.Controls.Add(this.bSearch);
			this.panel1.Controls.Add(this.tbSearchPattern);
			this.panel1.Controls.Add(this.lDir);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 26);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(424, 69);
			this.panel1.TabIndex = 10;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 95);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.dgwWorkers);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lvResults);
			this.splitContainer1.Size = new System.Drawing.Size(424, 358);
			this.splitContainer1.SplitterDistance = 74;
			this.splitContainer1.TabIndex = 11;
			// 
			// SearcherMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 453);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "SearcherMainForm";
			this.Text = "SearcherMainForm";
			((System.ComponentModel.ISupportInitialize)(this.dgwWorkers)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bSearch;
		private System.Windows.Forms.ListView lvResults;
		private System.Windows.Forms.TextBox tbSearchPattern;
		private System.Windows.Forms.Label lDir;
		private System.Windows.Forms.Label lSearchPattern;
		private System.Windows.Forms.DataGridView dgwWorkers;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbLoadPlugins;
		private System.Windows.Forms.ToolStripLabel tslSelPl;
		private System.Windows.Forms.ToolStripComboBox tscbSelPl;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataGridViewTextBoxColumn swinfo;
		private System.Windows.Forms.DataGridViewButtonColumn control;
	}
}