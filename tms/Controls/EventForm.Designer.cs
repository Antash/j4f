namespace tms
{
	partial class EventForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventForm));
			this.mcb = new System.Windows.Forms.MonthCalendar();
			this.mce = new System.Windows.Forms.MonthCalendar();
			this.cbPlay = new System.Windows.Forms.ComboBox();
			this.cbType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tcMain = new System.Windows.Forms.TabControl();
			this.tpAR = new System.Windows.Forms.TabPage();
			this.dgAR = new System.Windows.Forms.DataGridView();
			this.Actor = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.bSave = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.dudhb = new System.Windows.Forms.DomainUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.dudmb = new System.Windows.Forms.DomainUpDown();
			this.dudme = new System.Windows.Forms.DomainUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.dudhe = new System.Windows.Forms.DomainUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.bDel = new System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.roleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tcMain.SuspendLayout();
			this.tpAR.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgAR)).BeginInit();
			this.SuspendLayout();
			// 
			// mcb
			// 
			this.mcb.Location = new System.Drawing.Point(409, 39);
			this.mcb.MaxSelectionCount = 1;
			this.mcb.Name = "mcb";
			this.mcb.ShowToday = false;
			this.mcb.TabIndex = 0;
			this.mcb.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mc_DateChanged);
			// 
			// mce
			// 
			this.mce.Location = new System.Drawing.Point(619, 39);
			this.mce.MaxSelectionCount = 1;
			this.mce.Name = "mce";
			this.mce.ShowToday = false;
			this.mce.TabIndex = 1;
			this.mce.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mc_DateChanged);
			// 
			// cbPlay
			// 
			this.cbPlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPlay.FormattingEnabled = true;
			this.cbPlay.Location = new System.Drawing.Point(159, 39);
			this.cbPlay.Name = "cbPlay";
			this.cbPlay.Size = new System.Drawing.Size(238, 24);
			this.cbPlay.TabIndex = 2;
			// 
			// cbType
			// 
			this.cbType.FormattingEnabled = true;
			this.cbType.Items.AddRange(new object[] {
            "спектакль",
            "репетиция",
            "гастроли"});
			this.cbType.Location = new System.Drawing.Point(3, 39);
			this.cbType.Name = "cbType";
			this.cbType.Size = new System.Drawing.Size(150, 24);
			this.cbType.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(109, 25);
			this.label1.TabIndex = 4;
			this.label1.Text = "Событие:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(404, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(141, 25);
			this.label2.TabIndex = 5;
			this.label2.Text = "Дата начала";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(614, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(177, 25);
			this.label3.TabIndex = 6;
			this.label3.Text = "Дата окончания";
			// 
			// tcMain
			// 
			this.tcMain.Controls.Add(this.tpAR);
			this.tcMain.Location = new System.Drawing.Point(3, 69);
			this.tcMain.Name = "tcMain";
			this.tcMain.SelectedIndex = 0;
			this.tcMain.Size = new System.Drawing.Size(394, 308);
			this.tcMain.TabIndex = 7;
			// 
			// tpAR
			// 
			this.tpAR.Controls.Add(this.dgAR);
			this.tpAR.Location = new System.Drawing.Point(4, 25);
			this.tpAR.Name = "tpAR";
			this.tpAR.Padding = new System.Windows.Forms.Padding(3);
			this.tpAR.Size = new System.Drawing.Size(386, 279);
			this.tpAR.TabIndex = 0;
			this.tpAR.Text = "Актеры/Роли";
			this.tpAR.UseVisualStyleBackColor = true;
			// 
			// dgAR
			// 
			this.dgAR.AllowUserToAddRows = false;
			this.dgAR.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.dgAR.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dgAR.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgAR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgAR.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.roleID,
            this.Role,
            this.Actor});
			this.dgAR.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgAR.Location = new System.Drawing.Point(3, 3);
			this.dgAR.Name = "dgAR";
			this.dgAR.RowTemplate.Height = 24;
			this.dgAR.Size = new System.Drawing.Size(380, 273);
			this.dgAR.TabIndex = 0;
			// 
			// Actor
			// 
			this.Actor.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
			this.Actor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Actor.HeaderText = "Актер";
			this.Actor.Name = "Actor";
			// 
			// bSave
			// 
			this.bSave.Image = ((System.Drawing.Image)(resources.GetObject("bSave.Image")));
			this.bSave.Location = new System.Drawing.Point(409, 334);
			this.bSave.Name = "bSave";
			this.bSave.Size = new System.Drawing.Size(192, 39);
			this.bSave.TabIndex = 8;
			this.bSave.Text = "Сохранить изменения";
			this.bSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.bSave.UseVisualStyleBackColor = true;
			this.bSave.Click += new System.EventHandler(this.bSave_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(406, 255);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(106, 17);
			this.label4.TabIndex = 9;
			this.label4.Text = "Время начала:";
			// 
			// dudhb
			// 
			this.dudhb.Items.Add("0");
			this.dudhb.Items.Add("1");
			this.dudhb.Items.Add("2");
			this.dudhb.Items.Add("3");
			this.dudhb.Items.Add("4");
			this.dudhb.Items.Add("5");
			this.dudhb.Items.Add("6");
			this.dudhb.Items.Add("7");
			this.dudhb.Items.Add("8");
			this.dudhb.Items.Add("9");
			this.dudhb.Items.Add("10");
			this.dudhb.Items.Add("11");
			this.dudhb.Items.Add("12");
			this.dudhb.Items.Add("13");
			this.dudhb.Items.Add("14");
			this.dudhb.Items.Add("15");
			this.dudhb.Items.Add("16");
			this.dudhb.Items.Add("17");
			this.dudhb.Items.Add("18");
			this.dudhb.Items.Add("19");
			this.dudhb.Items.Add("20");
			this.dudhb.Items.Add("21");
			this.dudhb.Items.Add("22");
			this.dudhb.Items.Add("23");
			this.dudhb.Location = new System.Drawing.Point(409, 275);
			this.dudhb.Name = "dudhb";
			this.dudhb.Size = new System.Drawing.Size(58, 22);
			this.dudhb.TabIndex = 12;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(473, 277);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 17);
			this.label7.TabIndex = 14;
			this.label7.Text = "Часов";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(473, 305);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(49, 17);
			this.label8.TabIndex = 15;
			this.label8.Text = "Минут";
			// 
			// dudmb
			// 
			this.dudmb.Items.Add("0");
			this.dudmb.Items.Add("1");
			this.dudmb.Items.Add("2");
			this.dudmb.Items.Add("3");
			this.dudmb.Items.Add("4");
			this.dudmb.Items.Add("5");
			this.dudmb.Items.Add("6");
			this.dudmb.Items.Add("7");
			this.dudmb.Items.Add("8");
			this.dudmb.Items.Add("9");
			this.dudmb.Items.Add("10");
			this.dudmb.Items.Add("11");
			this.dudmb.Items.Add("12");
			this.dudmb.Items.Add("13");
			this.dudmb.Items.Add("14");
			this.dudmb.Items.Add("15");
			this.dudmb.Items.Add("16");
			this.dudmb.Items.Add("17");
			this.dudmb.Items.Add("18");
			this.dudmb.Items.Add("19");
			this.dudmb.Items.Add("20");
			this.dudmb.Items.Add("21");
			this.dudmb.Items.Add("22");
			this.dudmb.Items.Add("23");
			this.dudmb.Items.Add("24");
			this.dudmb.Items.Add("25");
			this.dudmb.Items.Add("26");
			this.dudmb.Items.Add("27");
			this.dudmb.Items.Add("28");
			this.dudmb.Items.Add("29");
			this.dudmb.Items.Add("30");
			this.dudmb.Items.Add("31");
			this.dudmb.Items.Add("32");
			this.dudmb.Items.Add("33");
			this.dudmb.Items.Add("34");
			this.dudmb.Items.Add("35");
			this.dudmb.Items.Add("36");
			this.dudmb.Items.Add("37");
			this.dudmb.Items.Add("38");
			this.dudmb.Items.Add("39");
			this.dudmb.Items.Add("40");
			this.dudmb.Items.Add("41");
			this.dudmb.Items.Add("42");
			this.dudmb.Items.Add("43");
			this.dudmb.Items.Add("44");
			this.dudmb.Items.Add("45");
			this.dudmb.Items.Add("46");
			this.dudmb.Items.Add("47");
			this.dudmb.Items.Add("48");
			this.dudmb.Items.Add("49");
			this.dudmb.Items.Add("50");
			this.dudmb.Items.Add("51");
			this.dudmb.Items.Add("52");
			this.dudmb.Items.Add("53");
			this.dudmb.Items.Add("54");
			this.dudmb.Items.Add("55");
			this.dudmb.Items.Add("56");
			this.dudmb.Items.Add("57");
			this.dudmb.Items.Add("58");
			this.dudmb.Items.Add("59");
			this.dudmb.Location = new System.Drawing.Point(409, 303);
			this.dudmb.Name = "dudmb";
			this.dudmb.Size = new System.Drawing.Size(58, 22);
			this.dudmb.TabIndex = 16;
			// 
			// dudme
			// 
			this.dudme.Items.Add("0");
			this.dudme.Items.Add("1");
			this.dudme.Items.Add("2");
			this.dudme.Items.Add("3");
			this.dudme.Items.Add("4");
			this.dudme.Items.Add("5");
			this.dudme.Items.Add("6");
			this.dudme.Items.Add("7");
			this.dudme.Items.Add("8");
			this.dudme.Items.Add("9");
			this.dudme.Items.Add("10");
			this.dudme.Items.Add("11");
			this.dudme.Items.Add("12");
			this.dudme.Items.Add("13");
			this.dudme.Items.Add("14");
			this.dudme.Items.Add("15");
			this.dudme.Items.Add("16");
			this.dudme.Items.Add("17");
			this.dudme.Items.Add("18");
			this.dudme.Items.Add("19");
			this.dudme.Items.Add("20");
			this.dudme.Items.Add("21");
			this.dudme.Items.Add("22");
			this.dudme.Items.Add("23");
			this.dudme.Items.Add("24");
			this.dudme.Items.Add("25");
			this.dudme.Items.Add("26");
			this.dudme.Items.Add("27");
			this.dudme.Items.Add("28");
			this.dudme.Items.Add("29");
			this.dudme.Items.Add("30");
			this.dudme.Items.Add("31");
			this.dudme.Items.Add("32");
			this.dudme.Items.Add("33");
			this.dudme.Items.Add("34");
			this.dudme.Items.Add("35");
			this.dudme.Items.Add("36");
			this.dudme.Items.Add("37");
			this.dudme.Items.Add("38");
			this.dudme.Items.Add("39");
			this.dudme.Items.Add("40");
			this.dudme.Items.Add("41");
			this.dudme.Items.Add("42");
			this.dudme.Items.Add("43");
			this.dudme.Items.Add("44");
			this.dudme.Items.Add("45");
			this.dudme.Items.Add("46");
			this.dudme.Items.Add("47");
			this.dudme.Items.Add("48");
			this.dudme.Items.Add("49");
			this.dudme.Items.Add("50");
			this.dudme.Items.Add("51");
			this.dudme.Items.Add("52");
			this.dudme.Items.Add("53");
			this.dudme.Items.Add("54");
			this.dudme.Items.Add("55");
			this.dudme.Items.Add("56");
			this.dudme.Items.Add("57");
			this.dudme.Items.Add("58");
			this.dudme.Items.Add("59");
			this.dudme.Location = new System.Drawing.Point(619, 303);
			this.dudme.Name = "dudme";
			this.dudme.Size = new System.Drawing.Size(58, 22);
			this.dudme.TabIndex = 21;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(683, 305);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(49, 17);
			this.label5.TabIndex = 20;
			this.label5.Text = "Минут";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(683, 277);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 17);
			this.label6.TabIndex = 19;
			this.label6.Text = "Часов";
			// 
			// dudhe
			// 
			this.dudhe.Items.Add("0");
			this.dudhe.Items.Add("1");
			this.dudhe.Items.Add("2");
			this.dudhe.Items.Add("3");
			this.dudhe.Items.Add("4");
			this.dudhe.Items.Add("5");
			this.dudhe.Items.Add("6");
			this.dudhe.Items.Add("7");
			this.dudhe.Items.Add("8");
			this.dudhe.Items.Add("9");
			this.dudhe.Items.Add("10");
			this.dudhe.Items.Add("11");
			this.dudhe.Items.Add("12");
			this.dudhe.Items.Add("13");
			this.dudhe.Items.Add("14");
			this.dudhe.Items.Add("15");
			this.dudhe.Items.Add("16");
			this.dudhe.Items.Add("17");
			this.dudhe.Items.Add("18");
			this.dudhe.Items.Add("19");
			this.dudhe.Items.Add("20");
			this.dudhe.Items.Add("21");
			this.dudhe.Items.Add("22");
			this.dudhe.Items.Add("23");
			this.dudhe.Location = new System.Drawing.Point(619, 275);
			this.dudhe.Name = "dudhe";
			this.dudhe.Size = new System.Drawing.Size(58, 22);
			this.dudhe.TabIndex = 18;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(616, 255);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(129, 17);
			this.label9.TabIndex = 17;
			this.label9.Text = "Время окончания:";
			// 
			// bDel
			// 
			this.bDel.Image = ((System.Drawing.Image)(resources.GetObject("bDel.Image")));
			this.bDel.Location = new System.Drawing.Point(619, 334);
			this.bDel.Name = "bDel";
			this.bDel.Size = new System.Drawing.Size(192, 39);
			this.bDel.TabIndex = 22;
			this.bDel.Text = "Удалить событие";
			this.bDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.bDel.UseVisualStyleBackColor = true;
			this.bDel.Click += new System.EventHandler(this.bDel_Click);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "Роль";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Visible = false;
			this.dataGridViewTextBoxColumn1.Width = 169;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.HeaderText = "Роль";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = 169;
			// 
			// roleID
			// 
			this.roleID.HeaderText = "ID";
			this.roleID.Name = "roleID";
			this.roleID.ReadOnly = true;
			this.roleID.Visible = false;
			// 
			// Role
			// 
			this.Role.HeaderText = "Роль";
			this.Role.Name = "Role";
			this.Role.ReadOnly = true;
			// 
			// EventForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(829, 382);
			this.Controls.Add(this.bDel);
			this.Controls.Add(this.dudme);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.dudhe);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.dudmb);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.dudhb);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.bSave);
			this.Controls.Add(this.tcMain);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbType);
			this.Controls.Add(this.cbPlay);
			this.Controls.Add(this.mce);
			this.Controls.Add(this.mcb);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EventForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.tcMain.ResumeLayout(false);
			this.tpAR.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgAR)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MonthCalendar mcb;
		private System.Windows.Forms.MonthCalendar mce;
		private System.Windows.Forms.ComboBox cbPlay;
		private System.Windows.Forms.ComboBox cbType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabControl tcMain;
		private System.Windows.Forms.TabPage tpAR;
		private System.Windows.Forms.DataGridView dgAR;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DomainUpDown dudhb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DomainUpDown dudmb;
        private System.Windows.Forms.DomainUpDown dudme;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DomainUpDown dudhe;
        private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button bDel;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn roleID;
		private System.Windows.Forms.DataGridViewTextBoxColumn Role;
		private System.Windows.Forms.DataGridViewComboBoxColumn Actor;
	}
}