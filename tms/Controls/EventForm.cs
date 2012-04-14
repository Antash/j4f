using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tms
{
	public partial class EventForm : Form
	{
		DataSet ds;
		int playID, eventID;
		ScheduleControl scp;

		public EventForm(int index, DataSet dataSet, ScheduleControl sc)
		{
			Load += new EventHandler(EventForm_Load);
			scp = sc;
			InitializeComponent();
			ds = dataSet;
			eventID = index;
			if (eventID == -1)
				initNew();
			else
				initModify();
		}

		void EventForm_Load(object sender, EventArgs e)
		{
			cbPlay.SelectedValueChanged += new EventHandler(cbPlay_SelectedValueChanged);
		}

		void cbPlay_SelectedValueChanged(object sender, EventArgs e)
		{
			if (cbPlay.SelectedValue == null)
				return;
			playID = int.Parse(cbPlay.SelectedValue.ToString());
			DataTable act = ds.Tables["Actors"],
				rol = ds.Tables["Roles"],
				actrol = ds.Tables["ActorsRoles"],
				play = ds.Tables["Plays"];
			var roles = from r in rol.AsEnumerable()
						where r.Field<int>("PlayID") == playID
						select new
						{
							id = r.Field<int>("ID"),
							role = r.Field<string>("Description")
						};
			int i = 0;
			dgAR.Rows.Clear();
			foreach (var r in roles)
			{
				dgAR.Rows.Add(r.id, r.role);
				var actors = from a in act.AsEnumerable()
							 join ar in actrol.AsEnumerable()
							 on a.Field<int>("ID") equals ar.Field<int>("ActorID")
							 where ar.Field<int>("RoleID") == r.id
							 select new
							 {
								 fio = a.Field<string>("FIO")
							 };
				DataGridViewComboBoxCell cbc = dgAR[2, i] as DataGridViewComboBoxCell;
				foreach (var a in actors)
					cbc.Items.Add(a.fio);
				if (cbc.Items.Count != 0)
					cbc.Value = cbc.Items[0];
				i++;
			}
			var currplay = from p in play.AsEnumerable()
						   where p.Field<int>("ID") == playID
						   select new
						   {
							   length = p.Field<TimeSpan>("TimeMount") +
							   p.Field<TimeSpan>("TimeDemount") +
							   p.Field<TimeSpan>("Duration")
						   };
			foreach (var p in currplay)
			{
				dudhe.SelectedIndex = dudhb.SelectedIndex + p.length.Hours;
				dudme.SelectedIndex = dudmb.SelectedIndex + p.length.Minutes;
			}
		}

		private void initModify()
		{
			Text = "Изменение события";
			cbPlay.DataSource = ds;
			cbPlay.DisplayMember = "Plays.Name";
			cbPlay.ValueMember = "Plays.ID";
			DataTable sch = ds.Tables["Schedule"],
				pl = ds.Tables["Plays"];
			var currplay = from ev in sch.AsEnumerable()
						   join play in pl.AsEnumerable()
						   on ev.Field<int>("PlayID") equals play.Field<int>("ID")
						   where ev.Field<int>("ID") == eventID
						   select new
						   {
							   id = ev.Field<int>("ID"),
							   plid = play.Field<int>("ID"),
							   name = play.Field<string>("Name"),
							   type = ev.Field<string>("Type"),
							   begin = ev.Field<DateTime>("DateStart"),
							   end = ev.Field<DateTime>("DateEnd"),
							   poss = ev.Field<Boolean>("Possible")
						   };
			foreach (var p in currplay)
			{
				cbType.Text = p.type;
				cbPlay.SelectedValue = p.plid;
				mcb.SetDate(p.begin);
				mce.SetDate(p.end);
				dudhb.SelectedIndex = p.begin.Hour;
				dudhe.SelectedIndex = p.end.Hour;
				dudmb.SelectedIndex = p.begin.Minute;
				dudme.SelectedIndex = p.end.Minute;
			}
			cbPlay_SelectedValueChanged(this, new EventArgs());
			foreach (DataGridViewRow dr in dgAR.Rows)
			{
				var arschedule = from ars in ds.Tables["ActorsRolesSchedule"].AsEnumerable()
								 where ars.Field<int>("ScheduleID") == eventID
								 select ars;
				DataGridViewComboBoxCell cbc = dr.Cells[2] as DataGridViewComboBoxCell;
				foreach (var ars in arschedule)
				{
					if (ars.Field<int>("RoleID").ToString() == dr.Cells[0].Value.ToString())
					{
						cbc.Value = ds.Tables["Actors"].Rows.Find(ars.Field<int>("ActorID")).Field<string>("FIO");
					}
				}
			}
		}

		private void initNew()
		{
			mcb.SetDate(scp.startD);
			mce.SetDate(scp.endD);
			Text = "Добавление нового события";
			cbPlay.DataSource = ds;
			cbPlay.DisplayMember = "Plays.Name";
			cbPlay.ValueMember = "Plays.ID";
			cbType.SelectedIndex = 0;
			cbPlay.SelectedIndex = 0;
			dudhb.SelectedIndex = 10;
			dudmb.SelectedIndex = 0;
			cbPlay_SelectedValueChanged(this, new EventArgs());
		}

		private void bSave_Click(object sender, EventArgs e)
		{
			DateTime dates = new DateTime(mcb.SelectionStart.Year,
							mcb.SelectionStart.Month,
							mcb.SelectionStart.Day,
							int.Parse(dudhb.SelectedItem.ToString()),
							int.Parse(dudmb.SelectedItem.ToString()), 0),
					 datee = new DateTime(mce.SelectionStart.Year,
							mce.SelectionStart.Month,
							mce.SelectionStart.Day,
							int.Parse(dudhe.SelectedItem.ToString()),
							int.Parse(dudme.SelectedItem.ToString()), 0), t;
			if (dates > datee)
			{
				t = datee;
				datee = dates;
				dates = t;
			}
			if (eventID == -1)
			{
				ds.Tables["Schedule"].Rows.Add(
					null,
					cbPlay.SelectedValue,
					cbType.Text,
					dates,
					datee,
					true
					);
				int lastrow = ds.Tables["Schedule"].Rows.Count - 1;
				eventID = ds.Tables["Schedule"].Rows[lastrow].Field<int>("ID");
				foreach (DataGridViewRow dr in dgAR.Rows)
				{
					var actor = from a in ds.Tables["Actors"].AsEnumerable()
								where a.Field<string>("FIO") == dr.Cells[2].Value.ToString()
								select a;
					int actorID = 0;
					foreach (var a in actor)
						actorID = a.Field<int>("ID");
					ds.Tables["ActorsRolesSchedule"].Rows.Add(
						actorID,
						dr.Cells[0].Value,
						eventID
						);
				}
			}
			else
			{
				DataRow drnew = ds.Tables["Schedule"].Rows.Find(eventID);
				drnew.SetField<int>("PlayID", playID);
				drnew.SetField<string>("Type", cbType.Text);
				drnew.SetField<DateTime>("DateStart", dates);
				drnew.SetField<DateTime>("DateEnd", datee);
				foreach (DataGridViewRow dr in dgAR.Rows)
				{
					var actor = from a in ds.Tables["Actors"].AsEnumerable()
								where a.Field<string>("FIO") == dr.Cells[2].Value.ToString()
								select a;
					int actorID = 0;
					foreach (var a in actor)
						actorID = a.Field<int>("ID");
					var arschedule = from ars in ds.Tables["ActorsRolesSchedule"].AsEnumerable()
									 where ars.Field<int>("ScheduleID") == eventID
									 select ars;
					foreach (var ars in arschedule)
					{
						if (ars.Field<int>("RoleID").ToString() == dr.Cells[0].Value.ToString() &&
							ars.Field<int>("ActorID") != actorID)
						{
							ars.Delete();
							ds.Tables["ActorsRolesSchedule"].Rows.Add(
								new object[] { actorID, dr.Cells[0].Value, eventID });
							ds.Tables["ActorsRolesSchedule"].AcceptChanges();
							break;
						}
					}
				}
			}
			scp.updateControl();
			Close();
		}

		private void bDel_Click(object sender, EventArgs e)
		{
			if (eventID != -1)
				ds.Tables["Schedule"].Rows.Remove(ds.Tables["Schedule"].Rows.Find(eventID));
			scp.updateControl();
			Close();
		}

		private void mc_DateChanged(object sender, DateRangeEventArgs e)
		{
			mce.MinDate = mcb.SelectionStart;
			mcb.MaxDate = mce.SelectionStart;
		}
	}
}
