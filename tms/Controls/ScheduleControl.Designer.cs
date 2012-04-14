namespace tms
{
	partial class ScheduleControl
	{
		/// <summary> 
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Обязательный метод для поддержки конструктора - не изменяйте 
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleControl));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.tsbPrev = new System.Windows.Forms.ToolStripButton();
			this.tsbNext = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbLess = new System.Windows.Forms.ToolStripButton();
			this.tsbMore = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbNew = new System.Windows.Forms.ToolStripButton();
			this.panel = new System.Windows.Forms.Panel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.toolStrip.SuspendLayout();
			this.panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPrev,
            this.tsbNext,
            this.toolStripSeparator1,
            this.tsbLess,
            this.tsbMore,
            this.toolStripSeparator2,
            this.tsbNew});
			this.toolStrip.Location = new System.Drawing.Point(0, 441);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(759, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// tsbPrev
			// 
			this.tsbPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbPrev.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrev.Image")));
			this.tsbPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbPrev.Name = "tsbPrev";
			this.tsbPrev.Size = new System.Drawing.Size(23, 22);
			this.tsbPrev.Text = "Назад";
			// 
			// tsbNext
			// 
			this.tsbNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbNext.Image")));
			this.tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbNext.Name = "tsbNext";
			this.tsbNext.Size = new System.Drawing.Size(23, 22);
			this.tsbNext.Text = "Вперед";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbLess
			// 
			this.tsbLess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbLess.Image = ((System.Drawing.Image)(resources.GetObject("tsbLess.Image")));
			this.tsbLess.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbLess.Name = "tsbLess";
			this.tsbLess.Size = new System.Drawing.Size(23, 22);
			this.tsbLess.Text = "Меньше";
			// 
			// tsbMore
			// 
			this.tsbMore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbMore.Image = ((System.Drawing.Image)(resources.GetObject("tsbMore.Image")));
			this.tsbMore.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbMore.Name = "tsbMore";
			this.tsbMore.Size = new System.Drawing.Size(23, 22);
			this.tsbMore.Text = "Больше";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbNew
			// 
			this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
			this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbNew.Name = "tsbNew";
			this.tsbNew.Size = new System.Drawing.Size(23, 22);
			this.tsbNew.Text = "Новое событие";
			// 
			// panel
			// 
			this.panel.AutoScroll = true;
			this.panel.Controls.Add(this.pictureBox);
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(0, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(759, 441);
			this.panel.TabIndex = 1;
			// 
			// pictureBox
			// 
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(759, 441);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// ScheduleControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel);
			this.Controls.Add(this.toolStrip);
			this.DoubleBuffered = true;
			this.Name = "ScheduleControl";
			this.Size = new System.Drawing.Size(759, 466);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton tsbPrev;
		private System.Windows.Forms.ToolStripButton tsbNext;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbLess;
		private System.Windows.Forms.ToolStripButton tsbMore;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbNew;
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.PictureBox pictureBox;
	}
}
