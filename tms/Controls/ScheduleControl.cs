using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace tms
{
	enum ScheduleType
	{
		Day, Week, Mounth, Year
	}

	public partial class ScheduleControl : UserControl
	{
		public event EvSelectDelegate EventSelect;
		public delegate void EvSelectDelegate(int index);

		Bitmap btmBack;
		Graphics grBack;
		ScheduleType scheduleType;
		public DateTime startD, endD;
		DataSet ds = new DataSet();
		StringFormat sf = new StringFormat();

		int[] timeStepD = { 0, 1, 1, 2, 2, 4, 6 };
		int[] timeStepW = { 0, 2, 4, 6 };
		public static int infoRectHeight = 40;
		int min = 15;
		Pen bLinePen = new Pen(SystemBrushes.ControlDark, 2);
		Brush textBrush = new SolidBrush(Color.FromArgb(150, SystemColors.ControlDark)),
			weekendBrush = new SolidBrush(Color.FromArgb(100, Color.Yellow)),
			selectedBrush = new SolidBrush(Color.FromArgb(100, Color.LightBlue)),
			headerBrush = SystemBrushes.ControlDarkDark;
		Font ctrlFont, headFont;
		float clickInterval = 0;
		bool zoomWhell = false;
		double mainScale;

		public ScheduleControl()
		{
			InitializeComponent();

			Load += new EventHandler(ScheduleControl_Load);

			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			sf.FormatFlags = StringFormatFlags.NoWrap;

			startD = endD = DateTime.Today;
			endD = endD.AddHours(23.9999);
			scheduleType = ScheduleType.Day;
		}

		public object DataSource
		{
			get { return ds; }
			set { ds = value as DataSet; }
		}

		void setGraphics()
		{
            btmBack = new Bitmap(pictureBox.Width, pictureBox.Height);
            grBack = Graphics.FromImage(btmBack);
            pictureBox.BackgroundImage = btmBack;
            grBack.SmoothingMode = SmoothingMode.HighQuality;
            grBack.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
		}

		Rectangle getInfoRect()
		{
			return new Rectangle(0, 0, pictureBox.Width, infoRectHeight);
		}

		Rectangle getSheduleRect()
		{
			return new Rectangle(0, infoRectHeight, pictureBox.Width, pictureBox.Height - infoRectHeight);
		}

		Rectangle getSelectedRect(int pos)
		{
			return new Rectangle((int)(pos * clickInterval), infoRectHeight, (int)clickInterval, pictureBox.Height - infoRectHeight);
		}

		void ScheduleControl_Load(object sender, EventArgs e)
		{
			setGraphics();
			drawBack();
			MouseWheel += new MouseEventHandler(ScheduleControl_MouseWheel);
			pictureBox.MouseDoubleClick += new MouseEventHandler(pictureBox_MouseDoubleClick);
			pictureBox.MouseClick += new MouseEventHandler(pictureBox_MouseClick);
			KeyUp += new KeyEventHandler(ScheduleControl_KeyUp);
			KeyDown += new KeyEventHandler(ScheduleControl_KeyDown);
			toolStrip.ItemClicked += new ToolStripItemClickedEventHandler(toolStrip_ItemClicked);
			pictureBox.Resize += new EventHandler(pictureBox_Resize);
		}

		void pictureBox_Resize(object sender, EventArgs e)
		{
            updateControl();
		}

        public void updateControl()
        {
            try
            {
                setGraphics();
                drawBack();
                drawFront();
            }
            catch (Exception) { }
        }

		int daysBetween(DateTime begin, DateTime end)
		{
			return begin.Year == end.Year ?
				end.DayOfYear - begin.DayOfYear :
				end.DayOfYear - begin.DayOfYear + (DateTime.IsLeapYear(begin.Year) == true ? 366 : 365);
		}

		int weeksBetween(DateTime begin, DateTime end)
		{
			return daysBetween(begin, end) / 7;
		}

		int mounthBetween(DateTime begin, DateTime end)
		{
			return begin.Month <= end.Month ?
				end.Month - begin.Month :
				12 - (begin.Month - end.Month);
		}

		int minutesBetween(DateTime begin, DateTime end)
		{
			return daysBetween(begin, end) * 24 * 60 +
				(end.Hour - begin.Hour) * 60 +
				end.Minute - begin.Minute;
		}

		void drawBack()
		{
			grBack.FillRectangle(SystemBrushes.Control, getInfoRect());
			grBack.FillRectangle(SystemBrushes.Control, getSheduleRect());
			grBack.DrawRectangle(bLinePen, getInfoRect());
			grBack.DrawRectangle(bLinePen, getSheduleRect());
			int bLineNum,
				tLineNum,
				n,
				t;
			float bStep,
				tStep;
			string text;
			DateTime currDate;
			RectangleF textRect;
			switch (scheduleType)
			{
				case ScheduleType.Day:
					n = daysBetween(startD, endD) + 1;
					bLineNum = n * 24 / timeStepD[n];
					tLineNum = n < 2 ? 60 / min : 0;
					bStep = pictureBox.Width / (float)bLineNum;
					tStep = bStep / tLineNum;
					ctrlFont = new Font(FontFamily.GenericSansSerif, bStep / 2, FontStyle.Bold, GraphicsUnit.Pixel);
					headFont = new Font(FontFamily.GenericSansSerif, infoRectHeight / 2, FontStyle.Italic, GraphicsUnit.Pixel);
					for (int i = 1, f = 0; i <= bLineNum; i++)
					{
						currDate = startD.AddDays(f);
						t = i * timeStepD[n] % 24 == 0 ? 23 : i * timeStepD[n] % 24 - 1;
						textRect = new RectangleF((i - 1) * bStep, infoRectHeight, bStep, pictureBox.Height - infoRectHeight);
						grBack.DrawString(t.ToString(), ctrlFont, textBrush, textRect, sf);
						if (t == 23)
						{
							clickInterval = 24 / timeStepD[n] * bStep;
							textRect = new RectangleF(24 / timeStepD[n] * bStep * f, 0, 24 / timeStepD[n] * bStep, infoRectHeight);
							text = currDate.ToString("dd.MM.yy") + ", " + currDate.ToString("dddd");
							if (currDate.DayOfWeek == DayOfWeek.Sunday || currDate.DayOfWeek == DayOfWeek.Saturday)
								grBack.FillRectangle(weekendBrush, textRect);
							grBack.DrawString(text, headFont, headerBrush, textRect, sf);
							grBack.DrawLine(bLinePen, i * bStep, 0, i * bStep, pictureBox.Height);
							f++;
						}
						else
							grBack.DrawLine(bLinePen, i * bStep, infoRectHeight, i * bStep, pictureBox.Height);
						for (int j = 1; j < tLineNum; j++)
						{
							float pos = (i - 1) * bStep + j * tStep;
							grBack.DrawLine(SystemPens.ControlDark, pos, infoRectHeight, pos, pictureBox.Height);
						}
					}
					break;
				case ScheduleType.Week:
					n = (daysBetween(startD, endD) + 1) / 7;
					bLineNum = n * 7;
					tLineNum = 24 / timeStepW[n];
					bStep = clickInterval = pictureBox.Width / (float)bLineNum;
					tStep = bStep / tLineNum;
					ctrlFont = new Font(FontFamily.GenericSansSerif, bStep / 2, FontStyle.Bold, GraphicsUnit.Pixel);
					headFont = new Font(FontFamily.GenericSansSerif, infoRectHeight / 2, FontStyle.Italic, GraphicsUnit.Pixel);
					for (int i = 1, f = 0; i <= bLineNum; i++)
					{
						currDate = startD.AddDays(i - 1);
						grBack.DrawLine(bLinePen, i * bStep, infoRectHeight, i * bStep, pictureBox.Height);
						if (i % 7 == 0)
						{
							text = startD.AddDays(i - 7).ToString("MMMM");
							if (startD.AddDays(i - 7).Month != currDate.Month)
								text += " - " + currDate.ToString("MMMM");
							text += ", " + currDate.ToString("yyyy");
							textRect = new RectangleF(f * 7 * bStep, 0, 7 * bStep, infoRectHeight);
							grBack.DrawString(text, headFont, headerBrush, textRect, sf);
							grBack.DrawLine(bLinePen, i * bStep, 0, i * bStep, pictureBox.Height);
							f++;
						}
						textRect = new RectangleF((i - 1) * bStep, infoRectHeight, bStep, pictureBox.Height - infoRectHeight);
						text = currDate.Day.ToString();
						grBack.DrawString(text, ctrlFont, textBrush, textRect, sf);
						if (currDate.DayOfWeek == DayOfWeek.Sunday || currDate.DayOfWeek == DayOfWeek.Saturday)
							grBack.FillRectangle(weekendBrush, textRect);
						for (int j = 1; j < tLineNum; j++)
						{
							float pos = (i - 1) * bStep + j * tStep;
							grBack.DrawLine(SystemPens.ControlDark, pos, infoRectHeight, pos, pictureBox.Height);
						}
					}
					break;
				case ScheduleType.Mounth:
					n = mounthBetween(startD, endD) + 1;
					bLineNum = n;
					bStep = pictureBox.Width / (float)bLineNum;
					if (n >= 3)
					{
						text = startD.Year.ToString();
						ctrlFont = new Font(FontFamily.GenericSansSerif, pictureBox.Width / 5, FontStyle.Bold, GraphicsUnit.Pixel);
						if (startD.Year != endD.Year)
						{
							text += " - " + endD.Year.ToString();
							ctrlFont = new Font(FontFamily.GenericSansSerif, pictureBox.Width / 7, FontStyle.Bold, GraphicsUnit.Pixel);
						}
						textRect = new RectangleF(0, infoRectHeight, pictureBox.Width, pictureBox.Height - infoRectHeight);
						grBack.DrawString(text, ctrlFont, new SolidBrush(SystemColors.ControlLight), textRect, sf);
					}
					headFont = new Font(FontFamily.GenericSansSerif, infoRectHeight / 2, FontStyle.Italic, GraphicsUnit.Pixel);
					for (int i = 1; i <= bLineNum; i++)
					{
						currDate = startD.AddMonths(i - 1);
						grBack.DrawLine(bLinePen, i * bStep, 0, i * bStep, pictureBox.Height);
						if (n < 3)
						{
							tLineNum = DateTime.DaysInMonth(currDate.Year, currDate.Month);
							tStep = bStep / (float)tLineNum;
							clickInterval = n < 2 ? tStep : bStep;
							ctrlFont = new Font(FontFamily.GenericSansSerif, tStep / 2, FontStyle.Bold, GraphicsUnit.Pixel);
							for (int j = 1; j <= tLineNum; j++)
							{
								float pos = (i - 1) * bStep + j * tStep;
								grBack.DrawLine(SystemPens.ControlDark, pos, infoRectHeight, pos, pictureBox.Height);
								textRect = new RectangleF(pos - tStep, infoRectHeight, tStep, pictureBox.Height - infoRectHeight);
								text = currDate.AddDays(j - 1).Day.ToString();
								grBack.DrawString(text, ctrlFont, textBrush, textRect, sf);
								if (currDate.AddDays(j - 1).DayOfWeek == DayOfWeek.Sunday || currDate.AddDays(j - 1).DayOfWeek == DayOfWeek.Saturday)
									grBack.FillRectangle(weekendBrush, textRect);
							}
							text = currDate.ToString("MMMM") + ", " + currDate.ToString("yy");
						}
						else
						{
							clickInterval = bStep;
							text = currDate.ToString("MMMM");
						}
						textRect = new RectangleF((i - 1) * bStep, 0, bStep, infoRectHeight);
						grBack.DrawString(text, headFont, headerBrush, textRect, sf);
					}
					break;
				case ScheduleType.Year:
					bLineNum = 12;
					bStep = clickInterval = pictureBox.Width / (float)bLineNum;
					ctrlFont = new Font(FontFamily.GenericSansSerif, pictureBox.Width / 5, FontStyle.Bold, GraphicsUnit.Pixel);
					headFont = new Font(FontFamily.GenericSansSerif, infoRectHeight / 2, FontStyle.Italic, GraphicsUnit.Pixel);
					text = startD.Year.ToString();
					textRect = new RectangleF(0, infoRectHeight, pictureBox.Width, pictureBox.Height - infoRectHeight);
					grBack.DrawString(text, ctrlFont, new SolidBrush(SystemColors.ControlLight), textRect, sf);
					for (int i = 1; i <= bLineNum; i++)
					{
						grBack.DrawLine(bLinePen, i * bStep, 0, i * bStep, pictureBox.Height);
						textRect = new RectangleF((i - 1) * bStep, 0, bStep, infoRectHeight);
						text = startD.AddMonths(i - 1).ToString("MMMM");
						grBack.DrawString(text, headFont, headerBrush, textRect, sf);
					}
					break;
			}
		}

		void drawFront()
		{
			mainScale = (double)pictureBox.Width / minutesBetween(startD, endD);
			pictureBox.Controls.Clear();
			DataTable sch = ds.Tables["Schedule"],
				pl = ds.Tables["Plays"];
			var displayedEvents = from ev in sch.AsEnumerable()
								  join play in pl.AsEnumerable()
								  on ev.Field<int>("PlayID") equals play.Field<int>("ID")
								  where ev.Field<DateTime>("DateStart") <= endD &&
								  ev.Field<DateTime>("DateEnd") >= startD
								  orderby ev.Field<DateTime>("DateStart") ascending
								  select new
								  {
									  id = ev.Field<int>("ID"),
									  name = play.Field<string>("Name"),
									  type = ev.Field<string>("Type"),
									  begin = ev.Field<DateTime>("DateStart"),
									  end = ev.Field<DateTime>("DateEnd"),
									  poss = ev.Field<Boolean>("Possible")
								  };
			List<int> maxEnd = new List<int>();
			int rowNum = 0, row;
			maxEnd.Add(0);
			foreach (var ev in displayedEvents)
			{
				row = -1;
				DateTime begin = ev.begin,
					end = ev.end;
				Point p = dateToPixel(begin, end);
				for (int i = 0; i <= rowNum; i++)
					if (p.X >= maxEnd[i])
					{
						maxEnd[i] = p.X + p.Y;
						row = i;
						break;
					}
				if (row == -1)
				{
					row = ++rowNum;
					maxEnd.Add(p.X + p.Y);
				}
				bool fix = false;
				Point loc = new Point(p.X, infoRectHeight + row * EventBtn.height);
				if (begin < startD || end > endD)
					fix = true;
				EventBtn evb = new EventBtn(loc, p.Y, ev.id, ds);
				evb.fix = fix;
				evb.scale = mainScale;
				evb.EventSelect += new EventBtn.EvSelectDelegate(evb_EventSelect);
				evb.EventUpdate += new EventBtn.EvUpdateDelegate(evb_EventUpdate);
				evb.EventUp += new EventBtn.EvUpDelegate(evb_EventUp);
				pictureBox.Controls.Add(evb);
			}
			Select();
		}

		void evb_EventUpdate(int index)
		{
		}

		void evb_EventUp(EventBtn ev)
		{
			pictureBox.Controls.SetChildIndex(ev, 0);
		}

		void evb_EventSelect(int index)
		{
			if (EventSelect != null)
				EventSelect(index);
		}

		Point dateToPixel(DateTime begin, DateTime end)
		{
			//p.X = location.X; p.Y = Width
			if (begin < startD)
				begin = startD;
			if (end > endD)
				end = endD;
			float scale;
			Point p = new Point();
			switch (scheduleType)
			{
				case ScheduleType.Day:
					scale = (float)pictureBox.Width / (daysBetween(startD, endD) + 1) / 24 / 60;
					p.X = (int)(minutesBetween(startD, begin) * scale);
					p.Y = (int)(minutesBetween(begin, end) * scale);
					break;
				case ScheduleType.Week:
					if (weeksBetween(startD, endD) == 0)
					{
						scale = (float)pictureBox.Width / (daysBetween(startD, endD) + 1) / 24 / 60;
						p.X = (int)(minutesBetween(startD, begin) * scale);
						p.Y = (int)(minutesBetween(begin, end) * scale);
						break;
					}
					scale = (float)pictureBox.Width / (daysBetween(startD, endD) + 1);
					p.X = (int)(daysBetween(startD, begin) * scale);
					p.Y = (int)((daysBetween(begin, end) + 1) * scale);
					break;
				case ScheduleType.Mounth:
					if (mounthBetween(startD, endD) == 0)
					{
						scale = (float)pictureBox.Width / DateTime.DaysInMonth(startD.Year, startD.Month);
						p.X = (int)(daysBetween(startD, begin) * scale);
						p.Y = (int)((daysBetween(begin, end) + 1) * scale);
						break;
					}
					if (mounthBetween(startD, endD) == 1)
					{
						scale = (float)pictureBox.Width / DateTime.DaysInMonth(begin.Year, begin.Month) / 2;
						float scale2 = (float)pictureBox.Width / DateTime.DaysInMonth(end.Year, end.Month) / 2;
						p.X = (int)(mounthBetween(startD, begin) * clickInterval);
						p.X += mounthBetween(startD, begin) == 0 ? (int)((begin.Day - 1) * scale) :
							(int)((begin.Day - 1) * scale2);
						p.Y = scale == scale2 ? (int)((daysBetween(begin, end) + 1) * scale) :
							(int)((daysBetween(begin, startD.AddMonths(1)) + 1) * scale +
							daysBetween(startD.AddMonths(1), end) * scale2);
						break;
					}
					scale = (float)pictureBox.Width / (mounthBetween(startD, endD) + 1);
					p.X = (int)(mounthBetween(startD, begin) * scale);
					p.Y = (int)((mounthBetween(begin, end) + 1) * scale);
					break;
				case ScheduleType.Year:
					scale = (float)pictureBox.Width / 12;
					p.X = (int)((begin.Month - 1) * scale);
					p.Y = (int)((mounthBetween(begin, end) + 1) * scale);
					break;
			}
			return p;
		}

		#region navigation functions

		void showMore()
		{
			switch (scheduleType)
			{
				case ScheduleType.Day:
					if (endD.DayOfYear - startD.DayOfYear < 5)
						endD = endD.AddDays(1);
					else
					{
						scheduleType = ScheduleType.Week;
						while (!(startD.DayOfWeek == DayOfWeek.Monday))
							startD = startD.AddDays(-1);
						endD = startD.AddDays(6);
						endD = endD.AddHours(23.9999);
					}

					break;
				case ScheduleType.Week:
					if ((endD.DayOfYear - startD.DayOfYear + 1) / 7 < 3)
						endD = endD.AddDays(7);
					else
					{
						scheduleType = ScheduleType.Mounth;
						startD = new DateTime(startD.Year, startD.Month, 1);
						endD = new DateTime(startD.Year, startD.Month, startD.AddMonths(1).AddDays(-1).Day);
						endD = endD.AddHours(23.9999);
					}
					break;
				case ScheduleType.Mounth:
					if (mounthBetween(startD, endD) < 11)
					{
						endD = endD.AddMonths(1);
						endD = endD.AddDays(DateTime.DaysInMonth(endD.Year, endD.Month) - endD.Day);
					}
					else
					{
						scheduleType = ScheduleType.Year;
						startD = new DateTime(startD.Year, 1, 1);
						endD = startD.AddYears(1).AddDays(-1);
						endD = endD.AddHours(23.9999);
					}
					break;
				case ScheduleType.Year: break;
			}
            updateControl();
		}

		void showLess()
		{
			switch (scheduleType)
			{
				case ScheduleType.Day:
					if (endD.DayOfYear != startD.DayOfYear)
						endD = endD.AddDays(-1);
					break;
				case ScheduleType.Week:
					if ((daysBetween(startD, endD) + 1) / 7 != 1)
						endD = endD.AddDays(-7);
					else
					{
						scheduleType = ScheduleType.Day;
						endD = endD.AddDays(-1);
					}
					break;
				case ScheduleType.Mounth:
					if (mounthBetween(startD, endD) > 0)
					{
						endD = endD.AddMonths(-1);
						endD = endD.AddDays(DateTime.DaysInMonth(endD.Year, endD.Month) - endD.Day);
					}
					else
					{
						scheduleType = ScheduleType.Week;
						while (!(startD.DayOfWeek == DayOfWeek.Monday))
							startD = startD.AddDays(1);
						endD = startD.AddDays(20);
						endD = endD.AddHours(23.9999);
					}
					break;
				case ScheduleType.Year:
					scheduleType = ScheduleType.Mounth;
					endD = endD.AddMonths(-1);
					break;
			}
            updateControl();
		}

		void nextDate()
		{
			switch (scheduleType)
			{
				case ScheduleType.Day:
					startD = startD.AddDays(1);
					endD = endD.AddDays(1);
					break;
				case ScheduleType.Week:
					startD = startD.AddDays(7);
					endD = endD.AddDays(7);
					break;
				case ScheduleType.Mounth:
					startD = startD.AddMonths(1);
					endD = endD.AddMonths(1);
					endD = endD.AddDays(DateTime.DaysInMonth(endD.Year, endD.Month) - endD.Day);
					break;
				case ScheduleType.Year:
					startD = startD.AddYears(1);
					endD = endD.AddYears(1);
					break;
			}
            updateControl();
		}

		void prevDate()
		{
			switch (scheduleType)
			{
				case ScheduleType.Day:
					startD = startD.AddDays(-1);
					endD = endD.AddDays(-1);
					break;
				case ScheduleType.Week:
					startD = startD.AddDays(-7);
					endD = endD.AddDays(-7);
					break;
				case ScheduleType.Mounth:
					startD = startD.AddMonths(-1);
					endD = endD.AddMonths(-1);
					endD = endD.AddDays(DateTime.DaysInMonth(endD.Year, endD.Month) - endD.Day);
					break;
				case ScheduleType.Year:
					startD = startD.AddYears(-1);
					endD = endD.AddYears(-1);
					break;
			}
            updateControl();
		}

		void clickScan(Point loc, bool expand)
		{
			int position = loc.X / (int)clickInterval;
			if (expand == true)
			{
				switch (scheduleType)
				{
					case ScheduleType.Day:
						startD = endD = startD.AddDays(position);
						endD = endD.AddHours(23.9999);
						break;
					case ScheduleType.Week:
						scheduleType = ScheduleType.Day;
						startD = endD = startD.AddDays(position);
						endD = endD.AddHours(23.9999);
						break;
					case ScheduleType.Mounth:
						if (startD.Month == endD.Month)
						{
							scheduleType = ScheduleType.Day;
							startD = endD = startD.AddDays(position);
							endD = endD.AddHours(23.9999);
						}
						else
						{
							startD = startD.AddMonths(position);
							endD = startD.AddMonths(1).AddDays(-1);
						}
						break;
					case ScheduleType.Year:
						scheduleType = ScheduleType.Mounth;
						startD = startD.AddMonths(position);
						endD = startD.AddMonths(1).AddDays(-1);
						break;
				}
                updateControl();
			}
			else
			{
                updateControl();
				if (startD.AddDays(1) < endD)
					grBack.FillRectangle(selectedBrush, getSelectedRect(position));
			}
		}

		private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			clickScan(e.Location, true);
		}

		private void pictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			Select();
			switch (e.Button)
			{
				case MouseButtons.Left: clickScan(e.Location, false); break;
			}
		}

		void ScheduleControl_MouseWheel(object sender, MouseEventArgs e)
		{
			Select();
			if (e.Delta < 0)
				if (zoomWhell == false)
					prevDate();
				else
					showLess();
			else
				if (zoomWhell == false)
					nextDate();
				else
					showMore();
		}

		private void ScheduleControl_KeyDown(object sender, KeyEventArgs e)
		{
			zoomWhell = e.Control;
		}

		private void ScheduleControl_KeyUp(object sender, KeyEventArgs e)
		{
			zoomWhell = false;
		}

		private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			switch (e.ClickedItem.Text)
			{
				case "Вперед": nextDate(); break;
				case "Назад": prevDate(); break;
				case "Больше": showMore(); break;
				case "Меньше": showLess(); break;
				case "Новое событие": 
					if (EventSelect != null)
						EventSelect(-1);
					break;
			}
		}

		#endregion

	}
}