﻿namespace Searcher
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearcherMainForm));
			this.dgwWorkers = new System.Windows.Forms.DataGridView();
			this.control = new System.Windows.Forms.DataGridViewButtonColumn();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbLoadPlugins = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tslSelPl = new System.Windows.Forms.ToolStripLabel();
			this.tscbSelPl = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbHelp = new System.Windows.Forms.ToolStripButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.bSearch = new System.Windows.Forms.Button();
			this.searchParamEditor1 = new Searcher.SearchParamEditor();
			this.dgwResult = new System.Windows.Forms.DataGridView();
			this.fname = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fbdPlugin = new System.Windows.Forms.FolderBrowserDialog();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.dgwWorkers)).BeginInit();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgwResult)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgwWorkers
			// 
			this.dgwWorkers.AllowUserToAddRows = false;
			this.dgwWorkers.AllowUserToResizeRows = false;
			this.dgwWorkers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgwWorkers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgwWorkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgwWorkers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.control});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgwWorkers.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgwWorkers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgwWorkers.Location = new System.Drawing.Point(0, 0);
			this.dgwWorkers.Name = "dgwWorkers";
			this.dgwWorkers.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgwWorkers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgwWorkers.RowTemplate.Height = 24;
			this.dgwWorkers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgwWorkers.Size = new System.Drawing.Size(610, 90);
			this.dgwWorkers.TabIndex = 8;
			this.dgwWorkers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwWorkers_CellContentClick);
			this.dgwWorkers.SelectionChanged += new System.EventHandler(this.dgwWorkers_SelectionChanged);
			this.dgwWorkers.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgwWorkers_UserDeletingRow);
			// 
			// control
			// 
			this.control.HeaderText = "Stop";
			this.control.Name = "control";
			this.control.ReadOnly = true;
			this.control.Text = "Stop";
			this.control.ToolTipText = "Stop search worker";
			this.control.UseColumnTextForButtonValue = true;
			this.control.Width = 43;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoadPlugins,
            this.toolStripSeparator1,
            this.tslSelPl,
            this.tscbSelPl,
            this.toolStripSeparator2,
            this.tsbHelp});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(610, 28);
			this.toolStrip1.TabIndex = 9;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbLoadPlugins
			// 
			this.tsbLoadPlugins.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbLoadPlugins.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadPlugins.Image")));
			this.tsbLoadPlugins.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbLoadPlugins.Name = "tsbLoadPlugins";
			this.tsbLoadPlugins.Size = new System.Drawing.Size(98, 25);
			this.tsbLoadPlugins.Text = "Load plugins";
			this.tsbLoadPlugins.Click += new System.EventHandler(this.tsbLoadPlugins_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
			// 
			// tslSelPl
			// 
			this.tslSelPl.Name = "tslSelPl";
			this.tslSelPl.Size = new System.Drawing.Size(115, 25);
			this.tslSelPl.Text = "Selected plugin:";
			// 
			// tscbSelPl
			// 
			this.tscbSelPl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tscbSelPl.Name = "tscbSelPl";
			this.tscbSelPl.Size = new System.Drawing.Size(121, 28);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
			// 
			// tsbHelp
			// 
			this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
			this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbHelp.Name = "tsbHelp";
			this.tsbHelp.Size = new System.Drawing.Size(45, 25);
			this.tsbHelp.Text = "Help";
			this.tsbHelp.Click += new System.EventHandler(this.tsbHelp_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 152);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.dgwWorkers);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.dgwResult);
			this.splitContainer1.Size = new System.Drawing.Size(610, 274);
			this.splitContainer1.SplitterDistance = 90;
			this.splitContainer1.TabIndex = 11;
			// 
			// bSearch
			// 
			this.bSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bSearch.Location = new System.Drawing.Point(532, 3);
			this.bSearch.Name = "bSearch";
			this.bSearch.Size = new System.Drawing.Size(75, 64);
			this.bSearch.TabIndex = 13;
			this.bSearch.Text = "Find!";
			this.bSearch.UseVisualStyleBackColor = true;
			this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
			// 
			// searchParamEditor1
			// 
			this.searchParamEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.searchParamEditor1.Location = new System.Drawing.Point(3, 3);
			this.searchParamEditor1.Name = "searchParamEditor1";
			this.searchParamEditor1.Size = new System.Drawing.Size(523, 118);
			this.searchParamEditor1.TabIndex = 9;
			// 
			// dgwResult
			// 
			this.dgwResult.AllowUserToAddRows = false;
			this.dgwResult.AllowUserToDeleteRows = false;
			this.dgwResult.AllowUserToResizeColumns = false;
			this.dgwResult.AllowUserToResizeRows = false;
			this.dgwResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgwResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgwResult.ColumnHeadersVisible = false;
			this.dgwResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fname});
			this.dgwResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgwResult.Location = new System.Drawing.Point(0, 0);
			this.dgwResult.Name = "dgwResult";
			this.dgwResult.ReadOnly = true;
			this.dgwResult.RowHeadersVisible = false;
			this.dgwResult.RowTemplate.Height = 24;
			this.dgwResult.Size = new System.Drawing.Size(610, 180);
			this.dgwResult.TabIndex = 0;
			this.dgwResult.VirtualMode = true;
			this.dgwResult.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgwResult_CellMouseDoubleClick);
			this.dgwResult.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgwResult_Scroll);
			this.dgwResult.Resize += new System.EventHandler(this.dgwResult_Resize);
			// 
			// fname
			// 
			this.fname.HeaderText = "fname";
			this.fname.Name = "fname";
			this.fname.ReadOnly = true;
			// 
			// fbdPlugin
			// 
			this.fbdPlugin.Description = "Select folder with search plugins";
			this.fbdPlugin.ShowNewFolderButton = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.bSearch);
			this.panel1.Controls.Add(this.searchParamEditor1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 28);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(610, 124);
			this.panel1.TabIndex = 14;
			// 
			// SearcherMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(610, 426);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "SearcherMainForm";
			this.Text = "SearcherMainForm";
			((System.ComponentModel.ISupportInitialize)(this.dgwWorkers)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgwResult)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgwWorkers;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbLoadPlugins;
		private System.Windows.Forms.ToolStripLabel tslSelPl;
		private System.Windows.Forms.ToolStripComboBox tscbSelPl;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.FolderBrowserDialog fbdPlugin;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbHelp;
		private System.Windows.Forms.DataGridView dgwResult;
		private System.Windows.Forms.DataGridViewButtonColumn control;
		private System.Windows.Forms.DataGridViewTextBoxColumn fname;
		private SearchParamEditor searchParamEditor1;
		private System.Windows.Forms.Button bSearch;
		private System.Windows.Forms.Panel panel1;
	}
}