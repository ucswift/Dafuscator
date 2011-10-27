using System;
using WaveTech.Dafuscator.Model.Events;
using WaveTech.Dafuscator.Model.Interfaces.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Model
{
	public class ObfuscationWorker
	{
		private readonly ISqlGenerationService _sqlGenerationService;
		private readonly IDataGenerationService _dataGenerationService;
		private readonly IDatabaseInteractionService _databaseInteractionService;
		private readonly IEventAggregator _eventAggregator;

		private int _rowsProcessed;
		private ConnectionString _connectionString;
		private Table _table;
		private string _errorString;

		public ObfuscationWorker(ISqlGenerationService sqlGenerationService, IDataGenerationService dataGenerationService,
			IDatabaseInteractionService databaseInteractionService, IEventAggregator eventAggregator, ConnectionString connectionString, Table table)
		{
			_sqlGenerationService = sqlGenerationService;
			_dataGenerationService = dataGenerationService;
			_databaseInteractionService = databaseInteractionService;
			_eventAggregator = eventAggregator;

			_table = table;
			_connectionString = connectionString;
		}

		public int RowsProcessed
		{
			get { return _rowsProcessed; }
		}

		public Table Table
		{
			get { return _table; }
		}

		public bool ErrorOccured
		{
			get
			{
				if (String.IsNullOrEmpty(_errorString))
					return false;

				return true;
			}
		}

		public string ErrorString
		{
			get { return _errorString; }
		}

		public void Process()
		{
			if (_table.AreAnyGeneratorsActive)
			{
				Table table = _dataGenerationService.GenerateDataForTable(_connectionString, _table, true);
				string sql = _sqlGenerationService.GenerateUpdateSqlForTable(table, false);

				if (String.IsNullOrEmpty(sql) == false)
				{
					_eventAggregator.SendMessage<StatusUpdateEvent>(new StatusUpdateEvent(string.Format("Processing SQL query for table: {0}", table.FullTableName)));

					try
					{
						_rowsProcessed = _databaseInteractionService.ProcessSql(_connectionString, sql);
					}
					catch (Exception ex)
					{
						_errorString = ex.ToString();
					}

				}
			}
		}
	}
}