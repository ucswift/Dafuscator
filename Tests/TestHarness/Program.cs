using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Providers;
using WaveTech.Dafuscator.Model.Interfaces.Repositories;
using WaveTech.Dafuscator.Providers.DatabaseInteractionProvider;

namespace TestHarness
{
	class Program
	{
		static void Main(string[] args)
		{
			ReadTables();

			//TestSqlTableNameFinder();

			Console.WriteLine("Press enter to exit.");
			Console.ReadLine();

			IObjectSerializationProvider objectSerializationProvider = ObjectLocator.GetInstance<IObjectSerializationProvider>();
			ISymmetricEncryptionProvider symmetricEncryptionProvider = ObjectLocator.GetInstance<ISymmetricEncryptionProvider>();
			IDatabaseProjectRepository databaseProjectRepository = ObjectLocator.GetInstance<IDatabaseProjectRepository>();
		}

		private static void ReadTables()
		{
			//String connect = "Provider=Microsoft.JET.OLEDB.4.0;data source=.\\Employee.mdb";
			String connect = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=AdventureWorks;Integrated Security=SSPI;";
			OleDbConnection con = new OleDbConnection(connect);
			con.Open();

			Console.WriteLine("Information for each table contains:");
			DataTable tables = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

			Console.WriteLine("The tables are:");
			foreach (DataRow row in tables.Rows)
			{
				Console.WriteLine("  {0}", row[2]);
				Console.WriteLine("    The columns are:");
				 
				DataTable columns = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, row[2], null });

				foreach (DataRow row1 in columns.Rows)
					Console.WriteLine("      {0}", row1[3]);
			}

			Console.WriteLine();
			con.Close();
		}

		private static void TestSqlTableNameFinder()
		{
			List<string> tableNames = DatabaseProvider.GetTableNamesFromSql("(rtrim(ltrim([Name])) <> '')");

			foreach (string s in tableNames)
				Console.WriteLine(s);


		}
	}
}
