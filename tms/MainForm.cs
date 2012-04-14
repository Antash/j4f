using System;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Drawing;

namespace tms
{
	public partial class MainForm : Form
	{
		BindingSource bsPlays, bsActors, bsTechStuff, bsStocks, bsTransport, bsDecorations;
		TablesChange changes;
		bool connected = false;
		DB dbc = new DB();
		Form gridForm;
		EventForm eventForm;
		DataGridView dgTwmp;

		public MainForm()
		{
			InitializeComponent();

			dgPlays.AutoGenerateColumns =
			dgActors.AutoGenerateColumns =
			dgTechStuff.AutoGenerateColumns =
			dgStocks.AutoGenerateColumns =
			dgTransport.AutoGenerateColumns =
			dgDecorations.AutoGenerateColumns = false;

			Load += new EventHandler(MainForm_Load);
		}

		void MainForm_Load(object sender, EventArgs e)
		{
			scheduleControl.EventSelect += new ScheduleControl.EvSelectDelegate(scheduleControl_EventSelect);
		}

		void tc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!connected && tc.SelectedIndex > 0)
			{
				MessageBox.Show("No connection");
				tc.SelectedIndex = 0;
			}
		}

		void bConnect_Click(object sender, EventArgs e)
		{
			ds.Clear();
			if (!dbc.Connect(tServer.Text, tUid.Text, tPwd.Text, tDB.Text))
			{
				MessageBox.Show(dbc.ErrStr);
				return;
			}
			if (!dbc.Open())
			{
				MessageBox.Show(dbc.ErrStr);
				return;
			}
			scheduleControl.Enabled = true;
			connected = true;
			bConnect.Text = "Обновить";
			bSync.Enabled =
			bCancel.Enabled =
			dgPlays.Enabled =
			dgActors.Enabled =
			dgTechStuff.Enabled =
			dgStocks.Enabled =
			dgTransport.Enabled =
			dgDecorations.Enabled = true;
			dbc.FillDS("SELECT * FROM Plays", ds, "Plays");
			dbc.FillDS("SELECT * FROM Actors", ds, "Actors");
			dbc.FillDS("SELECT * FROM Transport", ds, "Transport");
			dbc.FillDS("SELECT * FROM Stocks", ds, "Stocks");
			dbc.FillDS("SELECT * FROM Decorations", ds, "Decorations");
			dbc.FillDS("SELECT * FROM TechStuff", ds, "TechStuff");
			dbc.FillDS("SELECT * FROM Roles", ds, "Roles");
			dbc.FillDS("SELECT * FROM ActorsRoles", ds, "ActorsRoles");
			dbc.FillDS("SELECT * FROM PlaysDecorations", ds, "PlaysDecorations");
			dbc.FillDS("SELECT * FROM PlaysTechStuff", ds, "PlaysTechStuff");
			dbc.FillDS("SELECT * FROM DecorationsStocks", ds, "DecorationsStocks");
			dbc.FillDS("SELECT * FROM Schedule", ds, "Schedule");
			dbc.FillDS("SELECT * FROM TransportSchedule", ds, "TransportSchedule");
			dbc.FillDS("SELECT * FROM ActorsRolesSchedule", ds, "ActorsRolesSchedule");
			dgPlays.DataSource = bsPlays = new BindingSource(ds, "Plays");
			dgActors.DataSource = bsActors = new BindingSource(ds, "Actors");
			dgStocks.DataSource = bsStocks = new BindingSource(ds, "Stocks");
			dgTechStuff.DataSource = bsTechStuff = new BindingSource(ds, "TechStuff");
			dgTransport.DataSource = bsTransport = new BindingSource(ds, "Transport");
			dgDecorations.DataSource = bsDecorations = new BindingSource(ds, "Decorations");
			scheduleControl.DataSource = ds;
			scheduleControl.updateControl();
			changes = new TablesChange(ds.Tables);
			foreach (DataTable dt in ds.Tables)
			{
				dt.RowDeleting += new DataRowChangeEventHandler(changes.RowChanged);
				dt.RowChanged += new DataRowChangeEventHandler(changes.RowChanged);
			}
			dbc.Close();
			ssLabel.Text = "Соеденино";
		}

		void scheduleControl_EventSelect(int index)
		{
			if (eventForm != null)
				eventForm.Dispose();
			eventForm = new EventForm(index, ds, scheduleControl);
			eventForm.Show();
		}

		void bSync_Click(object sender, EventArgs e)
		{
			if (!dbc.Open())
			{
				MessageBox.Show(dbc.ErrStr);
				return;
			}
			changes.allowEvents = false;
			doSync("Plays", "ID", new string[] { "Plays_Schedule", "Plays_PlaysTechStuff", "Plays_Roles", "Plays_PlaysDecorations" }, new string[] { "ID" });
			doSync("Actors", "ID", new string[] { "Actors_ActorsRoles", "Actors_ActorsRolesSchedule" }, new string[] { "ID" });
			doSync("Transport", "ID", new string[] { "Transport_TransportSchedule" }, new string[] { "ID" });
			doSync("Stocks", "ID", new string[] { "Stocks_DecorationsStocks" }, new string[] { "ID" });
			doSync("Decorations", "ID", new string[] { "Decorations_DecorationsStocks", "PlaysDecorations" }, new string[] { "ID" });
			doSync("TechStuff", "ID", new string[] { "TechStuff_PlaysTechStuff" }, new string[] { "ID" });
			doSync("Roles", "ID", new string[] { "Roles_ActorsRoles", "Roles_ActorsRolesSchedule" }, new string[] { "ID" });
			doSync("Schedule", "ID", new string[] { "Schedule_ActorsRolesSchedule", "Schedule_TransportSchedule" }, new string[] { "ID" });
			doSync("ActorsRoles", null, new string[] { }, new string[] { "ActorID", "RoleID" });
			doSync("PlaysDecorations", null, new string[] { }, new string[] { "PlayID", "DecorationID" });
			doSync("PlaysTechStuff", null, new string[] { }, new string[] { "PlayID", "StuffID" });
			doSync("DecorationsStocks", null, new string[] { }, new string[] { "DecorationID", "StockID" });
			doSync("TransportSchedule", null, new string[] { }, new string[] { "ScheduleID", "TransportID" });
			doSync("ActorsRolesSchedule", null, new string[] { }, new string[] { "ActorID", "ScheduleID", "RoleID" });
			changes.Clear();
			changes.allowEvents = true;
			dbc.Close();
			ssLabel.Text = "Синхронизировано";
		}

		void doSync(string tab, string genKey, string[] rels, string[] pkeys)
		{	
			int id = 0;
			DataTable dt = ds.Tables[tab];
			string tabName = ds.Tables[tab].TableName;
			string colName = dt.Columns[0].ColumnName;
			int index = ds.Tables.IndexOf(tab);
			// Do insert
			foreach (DataRow row in changes.Added[index])
			{
				handleEmptyFields(row, dt);
				dbc.ClearParams();
				for (int i = 0; i < dt.Columns.Count; i++)
					if (dt.Columns[i].ColumnName != genKey)
						dbc.AddParam(dt.Columns[i].ColumnName, row[dt.Columns[i].ColumnName]);
				dbc.Insert(dt.TableName);
				if (genKey != null)
				{
					dt.Columns[genKey].ReadOnly = false;
					id = dbc.LastID();
					row[0] = id;
					dt.Columns[genKey].ReadOnly = true;
					if (rels != null)
						foreach (string rel in rels)
							foreach (DataRow dr in row.GetChildRows(ds.Relations[rel]))
								dr[ds.Relations[rel].ChildColumns[0].ColumnName] = id;
				}
				row.AcceptChanges();
			}
			// Do update
			foreach (DataRow row in changes.Changed[index])
			{
				handleEmptyFields(row, ds.Tables[tab]);
				dbc.ClearParams();
				for (int i = 0; i < dt.Columns.Count; i++)
					if (dt.Columns[i].ColumnName != genKey)
						dbc.AddParam(dt.Columns[i].ColumnName, row[dt.Columns[i].ColumnName]);
				foreach (string pk in pkeys)
					dbc.AddClause(pk, row[pk].ToString());
				dbc.Update(dt.TableName);
			}
			// Do delete
			foreach (DataRow row in changes.Deleted[index])
			{
				foreach (string pk in pkeys)
					dbc.AddClause(pk, row[pk].ToString());
				dbc.Delete(dt.TableName);
			}
		}

		void handleEmptyFields(DataRow row, DataTable dt)
		{
			for (int i = 1; i < dt.Columns.Count; i++)
				if (row[i] is DBNull)
					switch (dt.Columns[i].DataType.Name)
					{
						case "Boolean": row[i] = bool.FalseString; break;
						case "DateTime": row[i] = DateTime.MinValue; break;
						case "TimeSpan": row[i] = TimeSpan.Zero; break;
						case "Byte": row[i] = byte.MinValue; break;
						case "Byte[]": row[i] = new byte[] { }; break;
						case "Int32": row[i] = 0; break;
						case "Int16": row[i] = 0; break;
						default: row[i] = ""; break;
					}
		}

		void bCancel_Click(object sender, EventArgs e)
		{
			bsPlays.EndEdit();
			bsActors.EndEdit();
			bsTechStuff.EndEdit();
			bsStocks.EndEdit();
			bsTransport.EndEdit();
			bsDecorations.EndEdit();
			ds.RejectChanges();
			changes.Clear();
			scheduleControl.updateControl();
		}

		private void dgPlays_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
				return;
			DataGridViewComboBoxColumn cbc = new DataGridViewComboBoxColumn();
			string playName = dgPlays[1, e.RowIndex].Value.ToString();
			switch (e.ColumnIndex)
			{
				case 7:
					setGridForm("Роли/Актеры \"" + playName + "\"");
					SplitContainer splc = new SplitContainer();
					splc.Dock = DockStyle.Fill;
					splc.SplitterDistance = 70;
					dgTwmp = new DataGridView();
					BindingSource bsPlaysRoles;
					dgTwmp.DataSource = bsPlaysRoles = new BindingSource(bsPlays, "Plays_Roles");
					dgTwmp.Columns.Add("id", "ID");
					dgTwmp.Columns.Add("playid", "PlayID");
					dgTwmp.Columns.Add("role", "Роль");
					dgTwmp.Columns[0].DataPropertyName = "ID";
					dgTwmp.Columns[1].DataPropertyName = "PlayID";
					dgTwmp.Columns[2].DataPropertyName = "Description";
					dgTwmp.Columns[0].Visible = false;
					dgTwmp.Columns[1].Visible = false;
					dgTwmp.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
					dgTwmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
					dgTwmp.Dock = DockStyle.Fill;
					splc.Panel1.Controls.Add(dgTwmp);

					dgTwmp = new DataGridView();
					dgTwmp.DataSource = new BindingSource(bsPlaysRoles, "Roles_ActorsRoles");
					dgTwmp.Columns.Add("roleid", "RoleID");
					cbc.HeaderText = "Актер";
					cbc.DataSource = ds;
					cbc.ValueMember = "Actors.ID";
					cbc.DisplayMember = "Actors.FIO";
					cbc.FlatStyle = FlatStyle.Popup;
					dgTwmp.Columns.Add(cbc);
					dgTwmp.Columns[0].DataPropertyName = "RoleID";
					dgTwmp.Columns[1].DataPropertyName = "ActorID";
					dgTwmp.Columns[0].Visible = false;
					dgTwmp.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
					dgTwmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
					dgTwmp.Dock = DockStyle.Fill;
					splc.Panel2.Controls.Add(dgTwmp);

					gridForm.Controls.Add(splc);
					gridForm.Show();
					break;
				case 8:
					setGridForm("Персонал \"" + playName + "\"");
					dgTwmp = new DataGridView();
					dgTwmp.DataSource = new BindingSource(bsPlays, "Plays_PlaysDecorations");
					dgTwmp.Columns.Add("playid", "ID");
					cbc.HeaderText = "Декорация";
					cbc.DataSource = ds;
					cbc.ValueMember = "Decorations.ID";
					cbc.DisplayMember = "Decorations.Description";
					cbc.FlatStyle = FlatStyle.Popup;
					dgTwmp.Columns.Add(cbc);
					dgTwmp.Columns.Add("quant", "Колличество");
					dgTwmp.Columns[0].DataPropertyName = "PlayID";
					dgTwmp.Columns[1].DataPropertyName = "DecorationID";
					dgTwmp.Columns[2].DataPropertyName = "Quantity";
					dgTwmp.Columns[0].Visible = false;
					dgTwmp.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
					dgTwmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
					dgTwmp.Dock = DockStyle.Fill;
					gridForm.Controls.Add(dgTwmp);
					gridForm.Show();
					break;
				case 9:
					setGridForm("Декорации \"" + playName + "\"");
					dgTwmp = new DataGridView();
					dgTwmp.DataSource = new BindingSource(bsPlays, "Plays_PlaysTechStuff");
					dgTwmp.Columns.Add("playid", "ID");
					cbc.HeaderText = "Персонал";
					cbc.DataSource = ds;
					cbc.ValueMember = "TechStuff.ID";
					cbc.DisplayMember = "TechStuff.Description";
					cbc.FlatStyle = FlatStyle.Popup;
					dgTwmp.Columns.Add(cbc);
					dgTwmp.Columns.Add("quant", "Колличество");
					dgTwmp.Columns[0].DataPropertyName = "PlayID";
					dgTwmp.Columns[1].DataPropertyName = "TechStuffID";
					dgTwmp.Columns[2].DataPropertyName = "Quantity";
					dgTwmp.Columns[0].Visible = false;
					dgTwmp.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
					dgTwmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
					dgTwmp.Dock = DockStyle.Fill;
					gridForm.Controls.Add(dgTwmp);
					gridForm.Show();
					break;
			}
		}

		private void dgStocks_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
				return;
			string stockName = dgStocks[1, e.RowIndex].Value.ToString();
			DataGridViewComboBoxColumn cbc = new DataGridViewComboBoxColumn();
			switch (e.ColumnIndex)
			{
				case 7:
					setGridForm("Декорации \"" + stockName + "\"");
					dgTwmp = new DataGridView();
					dgTwmp.AlternatingRowsDefaultCellStyle.BackColor = Color.Wheat;
					dgTwmp.DataSource = new BindingSource(bsStocks, "Stocks_DecorationsStocks");
					dgTwmp.Columns.Add("stockid", "ID");
					cbc.HeaderText = "Декорация";
					cbc.DataSource = ds;
					cbc.ValueMember = "Decorations.ID";
					cbc.DisplayMember = "Decorations.Description";
					cbc.FlatStyle = FlatStyle.Popup;
					dgTwmp.Columns.Add(cbc);
					dgTwmp.Columns.Add("quant", "Колличество");
					dgTwmp.Columns[0].DataPropertyName = "StockID";
					dgTwmp.Columns[1].DataPropertyName = "DecorationID";
					dgTwmp.Columns[2].DataPropertyName = "Quantity";
					dgTwmp.Columns[0].Visible = false;
					dgTwmp.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
					dgTwmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
					dgTwmp.Dock = DockStyle.Fill;
					gridForm.Controls.Add(dgTwmp);
					gridForm.Show();
					break;
			}
		}

		void setGridForm(string header)
		{
			if (gridForm != null)
				gridForm.Dispose();
			gridForm = new Form();
			gridForm.StartPosition = FormStartPosition.CenterScreen;
			gridForm.Size = new Size(400, 500);
			gridForm.FormBorderStyle = FormBorderStyle.Sizable;
			gridForm.Text = header;
			gridForm.ShowIcon = false;
			gridForm.MaximizeBox = false;
			gridForm.MinimizeBox = false;
		}
	}
}
