using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing.Text;

namespace tms
{
	enum DragBound
	{
		None, Left, Right
	}

	class EventBtn : Button
	{
		int eventID;
		int dX, dY, widthOld, xOld;
		DragBound db;
		ToolTip tt = new ToolTip();
		public static int height = 25;
		DateTime begin, end;
		public double scale;
		public bool fix;
		bool poss;
		string name, type;
		StringFormat sf = new StringFormat();
		Brush br, 
			textBrush = new SolidBrush(Color.Black);
		Font font = new Font(FontFamily.GenericSansSerif, height / 1.5f, FontStyle.Italic, GraphicsUnit.Pixel);
		DataSet ds;

		public event EvSelectDelegate EventSelect;
		public delegate void EvSelectDelegate(int index);
		public event EvUpDelegate EventUp;
		public delegate void EvUpDelegate(EventBtn ev);
		public event EvUpdateDelegate EventUpdate;
		public delegate void EvUpdateDelegate(int index);
		
		public EventBtn(Point loc, int width, int evID, DataSet dataSet)
		{
			eventID = evID;
			ds = dataSet;
			//DataTable events = ds.Tables["Schedule"],
			//    plays = ds.Tables["Plays"];
			//var currplay = from e in events.AsEnumerable()
			//               join p in plays.AsEnumerable()
			//               on e.Field<int>("PlayID") equals p.Field<int>("ID")
			//               where e.Field<int>("ID") == eventID
			//               select p;
			//foreach (var p in currplay)
			//{ 
			//}
			SetStyle(ControlStyles.Selectable, false);
			SetStyle(ControlStyles.StandardClick, true);
			SetStyle(ControlStyles.StandardDoubleClick, true);
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			sf.FormatFlags = StringFormatFlags.NoWrap;
			Location = loc;
			Size = new Size(width + 3, height);
			MinimumSize = new Size(15, Height);

			DataTable sch = ds.Tables["Schedule"],
				pl = ds.Tables["Plays"];
			var dEvent = from ev in sch.AsEnumerable()
						 join play in pl.AsEnumerable()
						 on ev.Field<int>("PlayID") equals play.Field<int>("ID")
						 where ev.Field<int>("ID") == eventID
						 select new
						 {
							 id = ev.Field<int>("ID"),
							 name = play.Field<string>("Name"),
							 type = ev.Field<string>("Type"),
							 begin = ev.Field<DateTime>("DateStart"),
							 end = ev.Field<DateTime>("DateEnd"),
							 poss = ev.Field<Boolean>("Possible")
						 };
			foreach (var ev in dEvent)
			{
				name = ev.name;
				type = ev.type;
				begin = ev.begin;
				end = ev.end;
				poss = ev.poss;
			}
			if (poss == false)
				br = new LinearGradientBrush(new Rectangle(new Point(0, 0), Size),
					Color.FromArgb(100, Color.Red), Color.FromArgb(100, Color.Orange),
					LinearGradientMode.Vertical);
			else
				switch (type.ToLower())
				{
					case "спектакль":
						br = new LinearGradientBrush(new Rectangle(new Point(0, 0), Size),
							Color.FromArgb(100, Color.Green), Color.FromArgb(100, Color.GreenYellow),
							LinearGradientMode.Vertical);
						break;
					case "репетиция":
						br = new LinearGradientBrush(new Rectangle(new Point(0, 0), Size),
							Color.FromArgb(100, Color.Yellow), Color.FromArgb(100, Color.Wheat),
							LinearGradientMode.Vertical);
						break;
					case "гастроли":
						br = new LinearGradientBrush(new Rectangle(new Point(0, 0), Size),
							Color.FromArgb(100, Color.Blue), Color.FromArgb(100, Color.AliceBlue),
							LinearGradientMode.Vertical);
						break;
                    default:
                        br = new LinearGradientBrush(new Rectangle(new Point(0, 0), Size),
                            Color.FromArgb(100, Color.DarkGray), Color.FromArgb(100, Color.Gray),
                            LinearGradientMode.Vertical);
                        break;
				}
			MouseMove += new MouseEventHandler(EventBtn_MouseMove);
			MouseDown += new MouseEventHandler(EventBtn_MouseDown);
			MouseUp += new MouseEventHandler(EventBtn_MouseUp);
			MouseHover += new EventHandler(EventBtn_MouseHover);
			MouseLeave += new EventHandler(EventBtn_MouseLeave);
			DoubleClick += new EventHandler(EventBtn_DoubleClick);
			Paint += new PaintEventHandler(EventBtn_Paint);
		}

		void EventBtn_DoubleClick(object sender, EventArgs e)
		{
			if (EventSelect != null)
				EventSelect(eventID);
		}

		void EventBtn_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			int X = 0, Y = 0, 
				height = Height - 1, 
				radius = 5,
				width = Width - 1;
			GraphicsPath gp = new GraphicsPath();
			gp.AddLine(X + radius, Y, X + width - (radius * 2), Y);
			gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);
			gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));
			gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
			gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);
			gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
			gp.AddLine(X, Y + height - (radius * 2), X, Y + radius);
			gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90);
			gp.CloseFigure();
			e.Graphics.FillPath(br, gp);
			e.Graphics.DrawPath(Pens.Navy, gp);
			gp.Dispose();
			Rectangle textRect = new Rectangle(0, 0, Width, Height);
			e.Graphics.DrawString(name, font, textBrush, textRect, sf);
		}

		void EventBtn_MouseLeave(object sender, EventArgs e)
		{
			tt.Hide(this);
		}

		void EventBtn_MouseHover(object sender, EventArgs e)
		{
			tt.UseAnimation = true;
			tt.ToolTipTitle = "Подробно:";
			tt.UseFading = true;
			string text = "\n" + type + ": " + name + "\n" + begin + " - " + end + "\n";
			text += poss == false ? "Проведение невозможно" : "Проведение возможно";
			tt.Show(text, this, new Point(0, Height));
		}

		void EventBtn_MouseUp(object sender, MouseEventArgs e)
		{            
            if (e.Button == MouseButtons.Right || fix == true)
                return;
			Cursor.Clip = new Rectangle();
			int dLoc = Location.X - xOld,
				dWidth = Width - widthOld;
			begin = begin.AddMinutes((int)(dLoc / scale));
			end = end.AddMinutes((int)((dWidth + dLoc) / scale));
			if (begin > end)
				begin = end.AddMinutes(-10);
			if (end < begin)
				end = begin.AddMinutes(10);
			var dEvent = from ev in ds.Tables["Schedule"].AsEnumerable()
						 where ev.Field<int>("ID") == eventID
						 select ev;
			foreach (var ev in dEvent)
			{
				ev.SetField<DateTime>("DateStart", begin);
				ev.SetField<DateTime>("DateEnd", end);
			}
			if (EventUpdate != null)
				EventUpdate(eventID);
		}

		void EventBtn_MouseDown(object sender, MouseEventArgs e)
		{
			if (EventUp != null)
				EventUp(this);
			switch (e.Button)
			{
				case MouseButtons.Left:
					if (fix == false)
					{
						if (e.X < 5)
							db = DragBound.Left;
						else if (e.X > Width - 5)
							db = DragBound.Right;
						else
							db = DragBound.None;
						widthOld = Width;
						xOld = Location.X;
						Cursor.Clip = new Rectangle(Parent.PointToScreen(Parent.Location), Parent.Size);
					}
					break;
			}
		}

		void EventBtn_MouseMove(object sender, MouseEventArgs e)
		{
			if (fix == true)
				return;
			if (e.Button == MouseButtons.Left)
			{
				if (dX == 0 || dY == 0)
				{
					dX = e.X;
					dY = e.Y;
				}
				if (db == DragBound.Left)
				{
					if (Width > MinimumSize.Width || e.X - dX < 0)
					{
						Left += e.X - dX;
						Width -= (e.X - dX); 
					}
				}
				else if (db == DragBound.Right)
				{
					Width = widthOld + e.X - dX; 
				}
				else if (Cursor != Cursors.SizeWE)
				{
					Left += e.X - dX;
					Top += e.Y - dY;
				}
				return;
			}
			dX = 0;
			dY = 0;
			if (e.X < 5 || e.X > Width - 5)
				Cursor = Cursors.SizeWE;
			else
				Cursor = Cursors.Default;
		}
	}
}