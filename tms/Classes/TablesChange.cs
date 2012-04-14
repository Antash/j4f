using System.Data;
using System.Collections.Generic;

namespace tms
{
	class TablesChange
	{
		List<DataRow>[] added, deleted, changed;
		DataTableCollection tables;
		public List<DataRow>[] Added { get { return added; } }
		public List<DataRow>[] Deleted { get { return deleted; } }
		public List<DataRow>[] Changed { get { return changed; } }
		public bool allowEvents = true;

		public TablesChange(DataTableCollection col)
		{
			tables = col;
			int count = tables.Count;
			added = new List<DataRow>[count];
			deleted = new List<DataRow>[count];
			changed = new List<DataRow>[count];
			for (int i = 0; i < count; i++)
			{
				added[i] = new List<DataRow>();
				deleted[i] = new List<DataRow>();
				changed[i] = new List<DataRow>();
			}
		}

		internal void RowChanged(object sender, DataRowChangeEventArgs e)
		{
			if (!allowEvents)
				return;
			int id;
			for (id = 0; sender != tables[id]; id++);
			DataRow row = e.Row;
			List<DataRow>
				newRows = added[id],
				modRows = changed[id],
				delRows = deleted[id];
			switch (e.Action)
			{
				case DataRowAction.Add:
					if (!newRows.Contains(row))
						newRows.Add(e.Row);
					break;
				case DataRowAction.Change:
					if (!newRows.Contains(row) && !modRows.Contains(row))
						modRows.Add(e.Row);
					break;
				case DataRowAction.Delete:
					if (newRows.Contains(row))
						newRows.Remove(row);
					else
					{
						if (modRows.Contains(row))
							modRows.Remove(row);
						if (!delRows.Contains(row))
						{
							DataRow copy = tables[id].NewRow();
							foreach (DataColumn col in tables[id].Columns)
								copy[col] = row[col];
							delRows.Add(copy);
						}
					}
					break;
			}
		}

		internal void Clear()
		{
			for (int i = 0; i < tables.Count; i++)
			{
				added[i].Clear();
				deleted[i].Clear();
				changed[i].Clear();
			}
		}
	}
}