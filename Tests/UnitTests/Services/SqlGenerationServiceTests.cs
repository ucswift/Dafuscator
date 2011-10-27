using System;
using System.Collections.Generic;
using NUnit.Framework;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Generators;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Services;

namespace WaveTech.Dafuscator.UnitTests.Services
{
	namespace SqlGenerationServiceTests
	{
		public class with_the_sql_generation_service : FixtureBase
		{
			protected Dafuscator.Services.DataGenerationService dataGenerationService;
			protected Dafuscator.Services.SqlGenerationService sqlGenerationService;
			protected Dafuscator.Providers.DatabaseInteractionProvider.DatabaseProvider databaseProvider;
			protected Dafuscator.Services.DatabaseInteractionService databaseInteractionService;
			protected Dafuscator.Framework.EventAggregator eventAggregator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				databaseProvider = new Dafuscator.Providers.DatabaseInteractionProvider.DatabaseProvider();
				eventAggregator = new EventAggregator();
				sqlGenerationService = new Dafuscator.Services.SqlGenerationService(eventAggregator);
				databaseInteractionService = new DatabaseInteractionService(databaseProvider);
				dataGenerationService = new Dafuscator.Services.DataGenerationService(databaseInteractionService, eventAggregator);
			}
		}

		[TestFixture]
		public class when_generating_sql : with_the_sql_generation_service
		{
			[Test]
			public void should_not_be_null()
			{
				System.Collections.Generic.List<Table> tables = databaseProvider.GetSchemaInformation("Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=CoreMgr;Integrated Security=SSPI;");

				Column pk = tables[0].Columns[0];

				tables[0].Columns[2].GeneratorType = new AddressGeneratorInfo().Id;
				object[] data1 = new object[1];
				data1[0] = tables[0].RecordCount;

				tables[0].Columns[5].GeneratorType = new CityGeneratorInfo().Id;

				tables[0].Columns[6].GeneratorType = new StateGeneratorInfo().Id;

				tables[0].Columns[7].GeneratorType = new ZipCodeGeneratorInfo().Id;
				object[] data2 = new object[2];
				data2[0] = tables[0].RecordCount;
				data2[1] = true;

				tables[0].Columns[9].GeneratorType = new CountryGeneratorInfo().Id;

				HashSet<string> existingData = new HashSet<string>();

				tables[0].Columns[2] = dataGenerationService.GenerateDataForColumn(tables[0].Columns[2], data1, existingData);
				tables[0].Columns[5] = dataGenerationService.GenerateDataForColumn(tables[0].Columns[5], data1, existingData);
				tables[0].Columns[6] = dataGenerationService.GenerateDataForColumn(tables[0].Columns[6], data1, existingData);
				tables[0].Columns[7] = dataGenerationService.GenerateDataForColumn(tables[0].Columns[7], data2, existingData);
				tables[0].Columns[9] = dataGenerationService.GenerateDataForColumn(tables[0].Columns[9], data1, existingData);

				string sql = sqlGenerationService.GenerateUpdateSqlForTable(tables[0], true);

				Console.WriteLine(sql);

				Assert.IsNotNullOrEmpty(sql);
			}
		}
	}
}