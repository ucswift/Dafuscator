using System;
using System.Collections.Generic;
using System.Linq;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Events;
using WaveTech.Dafuscator.Model.Interfaces.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Services
{
	public class RunService : IRunService
	{
		private readonly ISqlGenerationService _sqlGenerationService;
		private readonly IDataGenerationService _dataGenerationService;
		private readonly IDatabaseInteractionService _databaseInteractionService;
		private readonly IEventAggregator _eventAggregator;

		public RunService(ISqlGenerationService sqlGenerationService, IDataGenerationService dataGenerationService,
											IDatabaseInteractionService databaseInteractionService, IEventAggregator eventAggregator)
		{
			_sqlGenerationService = sqlGenerationService;
			_dataGenerationService = dataGenerationService;
			_databaseInteractionService = databaseInteractionService;
			_eventAggregator = eventAggregator;
		}

		public ObfuscationResult ObfuscateDatabase(Database database)
		{
			ObfuscationResult result = new ObfuscationResult();
			result.DatabaseName = database.ConnectionString.DatabaseName;
			result.StartTimeStamp = DateTime.Now;

			try
			{
				Func<Table, ObfuscationWorker> getAndProcessObfuscationWorker = table =>
				{
					var obfuscationWorker = new ObfuscationWorker(
						_sqlGenerationService,
						_dataGenerationService,
						_databaseInteractionService,
						_eventAggregator,
						database.ConnectionString,
						table);
					obfuscationWorker.Process();
					return obfuscationWorker;
				};

				IEnumerable<ObfuscationWorker> obfuscationWorkers;
				obfuscationWorkers = database.Tables
						.AsParallel()
						.Select(getAndProcessObfuscationWorker);

				foreach (ObfuscationWorker worker in obfuscationWorkers)
				{
					if (worker.ErrorOccured)
						result.ErroredTables.Add(worker.Table.FullTableName, worker.ErrorString);
					else
						result.TablesProcessed.Add(worker.Table.FullTableName, worker.RowsProcessed);
				}
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);
				throw;
			}

			result.FinsihedTimeStamp = DateTime.Now;
			return result;
		}

		public ObfuscationResult ObfuscateTable(ConnectionString connectionString, Table table)
		{
			ObfuscationResult result = new ObfuscationResult();
			result.DatabaseName = connectionString.DatabaseName;
			result.StartTimeStamp = DateTime.Now;

			if (table.AreAnyGeneratorsActive)
			{
				Table newTable = _dataGenerationService.GenerateDataForTable(connectionString, table, true);
				string sql = _sqlGenerationService.GenerateUpdateSqlForTable(newTable, false);

				if (String.IsNullOrEmpty(sql) == false)
				{
					_eventAggregator.SendMessage<StatusUpdateEvent>(new StatusUpdateEvent(string.Format("Processing SQL query for table: {0}", table.FullTableName)));
					int rowsProcessed = _databaseInteractionService.ProcessSql(connectionString, sql);

					result.TablesProcessed.Add(table.FullTableName, rowsProcessed);
				}
			}

			result.FinsihedTimeStamp = DateTime.Now;
			return result;
		}
	}
}