using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Providers.DatabaseInteractionProvider
{
	public class DatabaseProvider : IDatabaseProvider
	{
		public List<Table> GetSchemaInformation(string connectionInfo)
		{
			List<Table> tables = new List<Table>();
			OleDbConnection con = new OleDbConnection(connectionInfo);
			con.Open();

			// Get all tables in the Database
			DataTable tableData = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
			foreach (DataRow row in tableData.Rows)
			{
				Table t = new Table();
				t.Schema = row[1].ToString();
				t.Name = row[2].ToString();

				// Get all columns in the Table
				DataTable columns = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, row[2], null });
				foreach (DataRow row1 in columns.Rows)
				{
					Column c = new Column();
					c.Name = row1[3].ToString();
					c.DataType = (OleDbType)row1["DATA_TYPE"];
					c.IsNullable = (bool)row1["IS_NULLABLE"];

					if (row1["CHARACTER_MAXIMUM_LENGTH"] != null && !String.IsNullOrEmpty(row1["CHARACTER_MAXIMUM_LENGTH"].ToString()))
						c.MaxLength = int.Parse(row1["CHARACTER_MAXIMUM_LENGTH"].ToString());

					t.Columns.Add(c);
				}

				// Get all the Primary Key Columns for the Table
				DataTable primaryKeys = con.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new object[] { null, row[1], row[2] });
				foreach (DataRow row2 in primaryKeys.Rows)
				{
					t.Columns.Where(x => x.Name == row2[3].ToString()).First().IsPrimaryKey = true;
					//t.Columns.Where(x => x.Name == row2[3].ToString()).First().PrimaryKeys = GetPrimaryKeysForTable(connectionInfo, t);
				}

				t.RecordCount = GetRecordCount(connectionInfo, t);

				tables.Add(t);
			}

			// Get all the Forign keys and table contraints
			foreach (Table t in tables)
			{
				DataTable forignKeys = con.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, new object[] { null, t.Schema, t.Name });
				foreach (DataRow row in forignKeys.Rows)
				{
					var table = tables.Where(x => x.Name == row[8].ToString()).First();  // TODO: I think this needs schema + table name
					table.Columns.Where(x => x.Name == row[9].ToString()).First().IsForignKey = true;
				}

				DataTable constraints = con.GetOleDbSchemaTable(OleDbSchemaGuid.Check_Constraints_By_Table, new object[] { null, t.Schema, t.Name });
				foreach (DataRow row in constraints.Rows)
				{
					var table = tables.Where(x => x.Name == row["TABLE_NAME"].ToString()).First(); // TODO: I think this needs schema + table name

					List<string> columnNames = GetTableNamesFromSql(row["CHECK_CLAUSE"].ToString());

					foreach (string column in columnNames)
					{
						Column c = table.Columns.Where(x => x.Name.Equals(column, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

						if (c != null)
							c.IsPartOfConstraint = true;
					}
				}
			}

			con.Close();

			return tables;
		}

		public List<string> GetPrimaryKeysForTable(string connectionInfo, Table table)
		{
			List<string> ids = new List<string>();
			Column primaryKey = table.GetPrimaryKeyColumn();

			OleDbConnection con = new OleDbConnection(connectionInfo);
			OleDbCommand command = con.CreateCommand();

			command.CommandText = string.Format("SELECT [{0}] FROM [{1}]", primaryKey.Name, table.FullTableName);
			command.CommandTimeout = 0;

			try
			{
				con.Open();

				OleDbDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					ids.Add(reader[0].ToString());
				}
			}
			finally
			{
				con.Close();
			}

			return ids;
		}

		public HashSet<string> GetDataForColumn(string connectionInfo, Table table, Column column)
		{
			HashSet<string> data = new HashSet<string>();

			OleDbConnection con = new OleDbConnection(connectionInfo);
			OleDbCommand command = con.CreateCommand();

			command.CommandText = string.Format("SELECT [{0}] FROM [{1}]", column.Name, table.FullTableName);
			command.CommandTimeout = 0;

			try
			{
				con.Open();

				OleDbDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					data.Add(reader[0].ToString().Trim().ToUpper());
				}
			}
			finally
			{
				con.Close();
			}

			return data;
		}

		public double GetRecordCount(string connectionInfo, Table table)
		{
			double recordCount = 0;

			Column primaryKey = table.GetPrimaryKeyColumn();

			OleDbConnection con = new OleDbConnection(connectionInfo);
			OleDbCommand command = con.CreateCommand();

			command.CommandText = string.Format("SELECT COUNT(*) FROM [{0}].[{1}]", table.Schema, table.Name);
			command.CommandTimeout = 0;

			try
			{
				con.Open();

				object returnValue = command.ExecuteScalar();

				if (returnValue != null && returnValue.ToString().Length > 0)
					recordCount = double.Parse(returnValue.ToString());

			}
			finally
			{
				con.Close();
			}

			return recordCount;
		}

		public bool TestConnection(ConnectionString connectionString)
		{
			bool connectionOk;
			OleDbConnection con = new OleDbConnection(connectionString.GetConnectionString());
			OleDbCommand command = con.CreateCommand();

			command.CommandText = string.Format("USE {0}", connectionString.DatabaseName);

			try
			{
				con.Open();
				command.ExecuteScalar();

				connectionOk = true;
			}
			catch (Exception ex)
			{
				connectionOk = false;
			}
			finally
			{
				con.Close();
			}

			return connectionOk;
		}

		public DataTable GetPreviewData(ConnectionString connectionString, Table table)
		{
			DataTable returnData = new DataTable();

			OleDbConnection con = new OleDbConnection(connectionString.GetConnectionString());
			OleDbCommand command = con.CreateCommand();

			command.CommandText = string.Format("SELECT TOP 1000 * FROM {0}", table.FullTableName);

			try
			{
				con.Open();
				IDataReader reader = command.ExecuteReader();

				returnData.Load(reader);
			}
			finally
			{
				con.Close();
			}

			return returnData;
		}

		public int ProcessSql(string connectionString, string sql)
		{
			int rowsProcessed = 0;

			OleDbConnection con = new OleDbConnection(connectionString);
			OleDbCommand command = con.CreateCommand();

			command.CommandText = sql;
			command.CommandTimeout = 0;

			try
			{
				//using (TransactionScope scope = new TransactionScope())
				//{
				con.Open();

				rowsProcessed = command.ExecuteNonQuery();

				// scope.Complete();
				//}
			}
			catch (Exception ex)
			{
				Logging.LogError(ex.Message + "\r\n\r\n\r\n" + sql);
				throw;
			}
			finally
			{
				con.Close();
			}

			return rowsProcessed;
		}

		public static List<string> GetTableNamesFromSql(string sql)
		{
			List<string> tableNames = new List<string>();

			char[] specialChars = new char[]
			                      	{
			                      		char.Parse("*"),
			                      		char.Parse("|"),
																char.Parse(","),
																char.Parse(":"),
																char.Parse(","),
																char.Parse("<"),
																char.Parse(">"),
																char.Parse("["),
																char.Parse("]"),
																char.Parse("{"),
																char.Parse("}"),
																char.Parse("`"),
																char.Parse("'"),
																char.Parse(";"),
																char.Parse("("),
																char.Parse(")"),
																char.Parse("@"),
																char.Parse("&"),
																char.Parse("$"),
																char.Parse("#"),
																char.Parse("%")
			                      	};

			char[] numberChars = new char[]
			                      	{
			                      		char.Parse("0"),
			                      		char.Parse("1"),
																char.Parse("2"),
																char.Parse("3"),
																char.Parse("4"),
																char.Parse("5"),
																char.Parse("6"),
																char.Parse("7"),
																char.Parse("8"),
																char.Parse("9")
			                      	};

			List<string> sqlKeywords = new List<string>
			{
        "SELECT",
        "FROM",
        "IS",
        "NULL",
        "NOT",
				"RTRIM",
				"LTRIM",
				"TRIM"
			};

			string[] sqlSplit = sql.Split(char.Parse(" "));

			foreach (string s in sqlSplit)
			{
				string temp = s;

				foreach (char c in specialChars)
					temp = temp.Replace(c, char.Parse(" "));

				foreach (char c in numberChars)
					temp = temp.Replace(c, char.Parse(" "));

				if (temp.Trim().Length <= 0)
					continue;
				else
				{
					bool okToAdd = true;

					foreach (string keyword in sqlKeywords)
						temp = temp.ToUpper().Replace(keyword, " ");

					if (temp.Trim().Length <= 0)
						okToAdd = false;

					if (okToAdd)
						tableNames.Add(temp.Trim());
				}
			}

			return tableNames;
		}
	}
}