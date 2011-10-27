using System;
using System.Collections.Generic;
using NUnit.Framework;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Generators;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Services;

namespace WaveTech.Dafuscator.UnitTests.Services
{
	namespace DataGenerationServiceTests
	{
		public class with_the_data_generation_service : FixtureBase
		{
			protected Dafuscator.Services.DataGenerationService dataGenerationService;
			protected Dafuscator.Providers.DatabaseInteractionProvider.DatabaseProvider databaseProvider;
			protected Dafuscator.Services.DatabaseInteractionService databaseInteractionService;
			protected Dafuscator.Framework.EventAggregator eventAggregator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				eventAggregator = new EventAggregator();
				databaseProvider = new Dafuscator.Providers.DatabaseInteractionProvider.DatabaseProvider();
				databaseInteractionService = new DatabaseInteractionService(databaseProvider);
				dataGenerationService = new Dafuscator.Services.DataGenerationService(databaseInteractionService, eventAggregator);
			}
		}

		[TestFixture]
		public class when_generating_data : with_the_data_generation_service
		{
			[Test]
			public void should_not_be_null()
			{
				System.Collections.Generic.List<Table> tables = databaseProvider.GetSchemaInformation("Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=CoreMgr;Integrated Security=SSPI;");
				Column column = tables[0].Columns[0];
				column.GeneratorType = new NumberGeneratorInfo().Id;

				object[] data = new object[3];
				data[0] = tables[0].RecordCount;
				data[1] = 1;
				data[2] = 99999999;

				HashSet<string> existingData = new HashSet<string>();

				column = dataGenerationService.GenerateDataForColumn(column, data, existingData);

				foreach (var i in column.Data)
				{
					Console.WriteLine(i);
				}
			}
		}
	}
}
