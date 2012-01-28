using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace calc
{
	public partial class MainForm : Form
	{
		private TextBox _tbCurr;

		private readonly Stack<string> _log;
		private bool _supressLog;
		private bool _isDirty;

		public MainForm()
		{
			InitializeComponent();

			_log = new Stack<string>();
		}

		private void ButtonQClick(object sender, EventArgs e)
		{
			double a, b;
			if (double.TryParse(textBoxSum.Text, out a) && double.TryParse(textBoxPc.Text, out b))
			{
				textBoxQ.Text = Math.Round(a * b / 100).ToString();
			}
			else
			{
				textBoxQ.Text = @"Ошибка!";
			}
		}

		private void ButtonSumClick(object sender, EventArgs e)
		{
			double a, b;
			if (double.TryParse(textBoxQ.Text, out a) && double.TryParse(textBoxPc.Text, out b))
			{
				textBoxSum.Text = Math.Round(a / b * 100).ToString();
			}
			else
			{
				textBoxSum.Text = @"Ошибка!";
			}
		}

		private void ButtonPcClick(object sender, EventArgs e)
		{
			double a, b;
			if (double.TryParse(textBoxSum.Text, out a) && double.TryParse(textBoxQ.Text, out b))
			{
				textBoxPc.Text = Math.Round(b * 100 / a).ToString();
			}
			else
			{
				textBoxPc.Text = @"Ошибка!";
			}
		}

		private void ButtonСClick(object sender, EventArgs e)
		{
			_tbCurr.Text = _tbCurr.Text.Length > 0 ? _tbCurr.Text.Substring(0, _tbCurr.Text.Length - 1) : String.Empty;
		}

		private void ButtonСсClick(object sender, EventArgs e)
		{
			_tbCurr.Text = String.Empty;
		}

		private void Form1Load(object sender, EventArgs e)
		{
			buttonD.Text = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

			foreach (var c in Controls.OfType<TextBox>())
			{
				c.Enter += TextBoxEnter;
				c.TextChanged += TextBoxTextChanged;
			}

			foreach (var c in Controls.OfType<Button>())
			{
				c.BackColor = SystemColors.Control;
				c.MouseEnter += c_MouseEnter;
				c.MouseLeave += c_MouseLeave;
			}

			foreach (var c in groupBox1.Controls.OfType<Button>())
			{
				c.BackColor = SystemColors.Control;
				c.MouseEnter += c_MouseEnter;
				c.MouseLeave += c_MouseLeave;
				c.Click += ButtonDigitClick;
			}
		}

		private void TextBoxTextChanged(object sender, EventArgs e)
		{
			if (_supressLog) return;
			_isDirty = true;
			var textBox = sender as TextBox;
			if (textBox != null) _log.Push(String.Format("{0}:{1}", textBox.Name, textBox.Text));
		}

		private void TextBoxEnter(object sender, EventArgs e)
		{
			if (_tbCurr != null) _tbCurr.BackColor = SystemColors.Window;
			_tbCurr = sender as TextBox;
			if (_tbCurr != null) _tbCurr.BackColor = Color.LightBlue;
		}

		void c_MouseLeave(object sender, EventArgs e)
		{
			var control = sender as Control;
			if (control != null) control.BackColor = SystemColors.Control;
		}

		void c_MouseEnter(object sender, EventArgs e)
		{
			var control = sender as Control;
			if (control != null) control.BackColor = Color.LightBlue;
		}

		private void ButtonDigitClick(object sender, EventArgs e)
		{
			if (_tbCurr == null) return;
			var button = sender as Button;
			if (button != null) _tbCurr.Text += button.Text;
		}

		private void ButtonCancelClick(object sender, EventArgs e)
		{
			if (_log.Count <= 1) return;

			if (_isDirty)
			{
				_log.Pop();
				_isDirty = false;
			}

			_supressLog = true;
			var ss = _log.Pop().Split(':');
			Controls[ss[0]].Text = ss[1];
			_supressLog = false;
		}
	}
}
