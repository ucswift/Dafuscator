using System;
using System.Linq;
using NUnit.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Providers.DatabaseInteractionProvider;

namespace WaveTech.Dafuscator.UnitTests.Providers
{

	namespace DatabaseProviderTests
	{
		public class with_the_database_provider : FixtureBase
		{
			protected Dafuscator.Providers.DatabaseInteractionProvider.DatabaseProvider databaseProvider;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				databaseProvider = new Dafuscator.Providers.DatabaseInteractionProvider.DatabaseProvider();
			}
		}

		[TestFixture]
		public class when_reading_database_schema : with_the_database_provider
		{
			[Test]
			public void should_not_be_null()
			{
				System.Collections.Generic.List<Table> tables = databaseProvider.GetSchemaInformation("Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=CoreMgr;Integrated Security=SSPI;");

				foreach (Table t in tables)
				{
					Console.WriteLine(t.Name);

					foreach (Column c in t.Columns)
					{
						Console.WriteLine(string.Format("     {0}     {1}     {2}", c.Name, c.Type, c.IsPrimaryKey));
					}
				}
				Assert.IsNotNull(tables);
			}
		}

		[TestFixture]
		public class when_generting_sql : with_the_database_provider
		{
			[Test]
			public void primary_keys_should_not_be_null()
			{
				System.Collections.Generic.List<Table> tables = databaseProvider.GetSchemaInformation("Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=CoreMgr;Integrated Security=SSPI;");

				foreach (Table t in tables)
				{
					Console.WriteLine(t.Name);

					System.Collections.Generic.List<string> primaryKeys =
					databaseProvider.GetPrimaryKeysForTable("Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=CoreMgr;Integrated Security=SSPI;", t);

					Console.WriteLine(string.Format("    Rows: {0}", primaryKeys.Count));

					foreach (string s in primaryKeys)
					{
						Console.WriteLine(string.Format("     {0}", s));
					}
				}
				Assert.IsNotNull(tables);
			}
		}

		[TestFixture]
		public class when_finding_table_names_in_sql : with_the_database_provider
		{
			[Test]
			public void should_handle_rtrim_ltrim()
			{
				System.Collections.Generic.List<string> tableNames = DatabaseProvider.GetTableNamesFromSql("(rtrim(ltrim([Name])) <> '')");

				Assert.IsNotNull(tableNames);
				Assert.AreEqual(1, tableNames.Count);
				Assert.AreEqual("Name", tableNames[0]);
			}
		}
	}
}