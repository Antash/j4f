using System;
using System.Data;
using System.Data.SqlClient;

namespace tms
{
	public class DB
	{
		SqlConnection con;
		SqlCommand cmd;
		SqlDataAdapter adp;
		string flist = "";
		string vlist = "";
		string ulist = "";
        string clause = "";
		public string ErrStr = "";

		public bool Connect(string srv, string uid, string pwd, string dtb)
		{
			try
			{
				con = new SqlConnection("Server=" + srv + ";Database=" + dtb + ";User ID=" + uid + ";Password=" + pwd + ";Trusted_Connection=True;");
				adp = new SqlDataAdapter();
				adp.SelectCommand = cmd = con.CreateCommand();
			}
			catch (Exception ex)
			{
				ErrStr = ex.Message;
				return false;
			}
			return true;
		}

		public bool Open()
		{
			try { con.Open(); }
			catch (Exception e)
			{
				ErrStr = e.Message;
				return false;
			}
			return true;
		}

		public void Close()
		{
			if (con.State == ConnectionState.Open)
				con.Close();
		}

		public void FillDS(string q, DataSet ds, string table)
		{
			cmd.CommandText = q;
			try
			{
				adp.Fill(ds, table);
			}
			catch(Exception){}
		}

		public int LastID()
		{
            cmd.CommandText = "SELECT @@IDENTITY";
            return int.Parse(cmd.ExecuteScalar().ToString());
		}

		public void AddParam(string name, object value)
		{
			cmd.Parameters.AddWithValue("@" + name, value);
			flist += ((flist == "") ? "" : ", ") + name;
			vlist += ((vlist == "") ? "" : ", ") + '@' + name;
			ulist += ((ulist == "") ? "" : ", ") + name + " = @" + name;
		}

        public void AddClause(string name, string value)
        {
            clause += ((clause == "") ? "" : " AND ") + name + " = '" + value + "'";
        }

        public void ClearParams()
        {
            cmd.Parameters.Clear();
        }

		public void Insert(string table)
		{
			Exec("INSERT INTO " + table + " (" + flist + ") VALUES (" + vlist + ")");
		}

		public void Update(string table)
		{
			Exec("UPDATE " + table + " SET " + ulist + " WHERE " + clause);
		}

		public void Delete(string table)
		{
			Exec("DELETE FROM " + table + " WHERE " + clause);
		}

		public void Exec(string q)
		{
			cmd.CommandText = q;
			cmd.ExecuteNonQuery();
			cmd.Parameters.Clear();
            clause = "";
			flist = vlist = ulist = "";
		}
	}
}