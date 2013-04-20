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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearcherMainForm));
			this.bSearch = new System.Windows.Forms.Button();
			this.tbSearchPattern = new System.Windows.Forms.TextBox();
			this.lSearchPattern = new System.Windows.Forms.Label();
			this.dgwWorkers = new System.Windows.Forms.DataGridView();
			this.control = new System.Windows.Forms.DataGridViewButtonColumn();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbLoadPlugins = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tslSelPl = new System.Windows.Forms.ToolStripLabel();
			this.tscbSelPl = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbHelp = new System.Windows.Forms.ToolStripButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.bSelDir = new System.Windows.Forms.Button();
			this.tbRootDir = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dgwResult = new System.Windows.Forms.DataGridView();
			this.fbdPlugin = new System.Windows.Forms.FolderBrowserDialog();
			this.fbdSearch = new System.Windows.Forms.FolderBrowserDialog();
			this.fname = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgwWorkers)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgwResult)).BeginInit();
			this.SuspendLayout();
			// 
			// bSearch
			// 
			this.bSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bSearch.Location = new System.Drawing.Point(523, 8);
			this.bSearch.Name = "bSearch";
			this.bSearch.Size = new System.Drawing.Size(75, 51);
			this.bSearch.TabIndex = 0;
			this.bSearch.Text = "Find!";
			this.bSearch.UseVisualStyleBackColor = true;
			this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
			// 
			// tbSearchPattern
			// 
			this.tbSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSearchPattern.Location = new System.Drawing.Point(90, 8);
			this.tbSearchPattern.Name = "tbSearchPattern";
			this.tbSearchPattern.Size = new System.Drawing.Size(427, 22);
			this.tbSearchPattern.TabIndex = 2;
			// 
			// lSearchPattern
			// 
			this.lSearchPattern.AutoSize = true;
			this.lSearchPattern.Location = new System.Drawing.Point(6, 11);
			this.lSearchPattern.Name = "lSearchPattern";
			this.lSearchPattern.Size = new System.Drawing.Size(78, 17);
			this.lSearchPattern.TabIndex = 6;
			this.lSearchPattern.Text = "Search for:";
			// 
			// dgwWorkers
			// 
			this.dgwWorkers.AllowUserToAddRows = false;
			this.dgwWorkers.AllowUserToResizeRows = false;
			this.dgwWorkers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgwWorkers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgwWorkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgwWorkers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.control});
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgwWorkers.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgwWorkers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgwWorkers.Location = new System.Drawing.Point(3, 91);
			this.dgwWorkers.Name = "dgwWorkers";
			this.dgwWorkers.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgwWorkers.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgwWorkers.RowTemplate.Height = 24;
			this.dgwWorkers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgwWorkers.Size = new System.Drawing.Size(604, 108);
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
			// panel1
			// 
			this.panel1.Controls.Add(this.bSelDir);
			this.panel1.Controls.Add(this.tbRootDir);
			this.panel1.Controls.Add(this.lSearchPattern);
			this.panel1.Controls.Add(this.bSearch);
			this.panel1.Controls.Add(this.tbSearchPattern);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 18);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(604, 73);
			this.panel1.TabIndex = 10;
			// 
			// bSelDir
			// 
			this.bSelDir.Location = new System.Drawing.Point(9, 37);
			this.bSelDir.Name = "bSelDir";
			this.bSelDir.Size = new System.Drawing.Size(75, 23);
			this.bSelDir.TabIndex = 8;
			this.bSelDir.Text = "Select dir";
			this.bSelDir.UseVisualStyleBackColor = true;
			this.bSelDir.Click += new System.EventHandler(this.bSelDir_Click);
			// 
			// tbRootDir
			// 
			this.tbRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbRootDir.Location = new System.Drawing.Point(90, 37);
			this.tbRootDir.Name = "tbRootDir";
			this.tbRootDir.Size = new System.Drawing.Size(427, 22);
			this.tbRootDir.TabIndex = 7;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 28);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.dgwResult);
			this.splitContainer1.Size = new System.Drawing.Size(610, 398);
			this.splitContainer1.SplitterDistance = 202;
			this.splitContainer1.TabIndex = 11;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dgwWorkers);
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(610, 202);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Search parameters";
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
			this.dgwResult.Size = new System.Drawing.Size(610, 192);
			this.dgwResult.TabIndex = 0;
			this.dgwResult.VirtualMode = true;
			this.dgwResult.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgwResult_CellMouseDoubleClick);
			this.dgwResult.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgwResult_Scroll);
			this.dgwResult.Resize += new System.EventHandler(this.dgwResult_Resize);
			// 
			// fbdPlugin
			// 
			this.fbdPlugin.Description = "Select folder with search plugins";
			this.fbdPlugin.ShowNewFolderButton = false;
			// 
			// fbdSearch
			// 
			this.fbdSearch.Description = "Select search folder";
			this.fbdSearch.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.fbdSearch.ShowNewFolderButton = false;
			// 
			// fname
			// 
			this.fname.HeaderText = "fname";
			this.fname.Name = "fname";
			this.fname.ReadOnly = true;
			// 
			// SearcherMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(610, 426);
			this.Controls.Add(this.splitContainer1);
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
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgwResult)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bSearch;
		private System.Windows.Forms.TextBox tbSearchPattern;
		private System.Windows.Forms.Label lSearchPattern;
		private System.Windows.Forms.DataGridView dgwWorkers;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbLoadPlugins;
		private System.Windows.Forms.ToolStripLabel tslSelPl;
		private System.Windows.Forms.ToolStripComboBox tscbSelPl;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button bSelDir;
		private System.Windows.Forms.TextBox tbRootDir;
		private System.Windows.Forms.FolderBrowserDialog fbdPlugin;
		private System.Windows.Forms.FolderBrowserDialog fbdSearch;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbHelp;
		private System.Windows.Forms.DataGridView dgwResult;
		private System.Windows.Forms.DataGridViewButtonColumn control;
		private System.Windows.Forms.DataGridViewTextBoxColumn fname;
	}
}