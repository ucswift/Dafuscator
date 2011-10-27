using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Events;
using WaveTech.Dafuscator.Model.Interfaces.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Services
{
	public class SqlGenerationService : ISqlGenerationService
	{
		private readonly IEventAggregator _eventAggregator;

		public SqlGenerationService(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

		public string GenerateUpdateSql(List<Table> tables, bool isUserRun)
		{
			StringBuilder sb = new StringBuilder();

			foreach (Table t in tables)
			{
				sb.Append(GenerateUpdateSqlForTable(t, isUserRun));
			}

			return sb.ToString();
		}

		public string GenerateUpdateSqlForTable(Table table, bool isUserRun)
		{
			if (table.HandlerType != TableHandlerTypes.None)
			{
				_eventAggregator.SendMessage<StatusUpdateEvent>(new StatusUpdateEvent(string.Format("Generating SQL for table: {0}", table.FullTableName)));

				StringBuilder sql1 = new StringBuilder();

				if (table.HandlerType == TableHandlerTypes.Drop)
				{
					sql1.Append(string.Format("IF OBJECT_ID('{0}') IS NOT NULL \r\n", table.FullTableName));
					sql1.Append(string.Format("     DROP TABLE {0} \r\n\r\n", table.FullTableName));

					if (isUserRun)
						sql1.Append("GO\r\n\r\n");
				}
				else
				{
					sql1.Append(string.Format("IF OBJECT_ID('{0}') IS NOT NULL \r\n", table.FullTableName));
					sql1.Append(string.Format("     DELETE FROM {0} \r\n\r\n", table.FullTableName));

					if (isUserRun)
						sql1.Append("GO\r\n\r\n");
				}

				return sql1.ToString();
			}

			if (table.AreAnyGeneratorsActive == false)
				return String.Empty;

			if (table.RecordCount <= 0)
				return String.Empty;

			StringBuilder sql = new StringBuilder();
			Column primaryKey = table.GetPrimaryKeyColumn();
			List<Column> activeColumns = table.Columns.Where(x => x.GeneratorType != null && x.GeneratorType != SystemConstants.DefaultGuid).ToList();
			List<Column> activeColumnsNonClear = activeColumns.Where(x => x.GeneratorType != SystemConstants.ClearGeneratorGuid).ToList();
			List<Column> activeColumnsOnlyClear = activeColumns.Where(x => x.GeneratorType == SystemConstants.ClearGeneratorGuid).ToList();

			string tableNameOnly = table.Schema + table.Name;
			string tableName = tableNameOnly + "ObfuscateData";

			if (activeColumnsNonClear.Count > 0)
			{
				if (isUserRun)
					sql.Append(string.Format("\r\nPRINT '########## [' + CONVERT(VARCHAR, GETDATE()) + ']: Starting setting up temp Obfuscation data for {0}' \r\nGO\r\n\r\n", table.FullTableName));

				// Create generated data temp table structre
				sql.Append(string.Format("CREATE TABLE #{0} \r\n ( \r\n", tableName));
				sql.Append("   Id bigint \r\n");

				foreach (Column c in activeColumnsNonClear)
				{
					sql.Append(string.Format("   ,[{0}] VARCHAR(MAX) \r\n", c.Name));
				}

				sql.Append(") \r\n");
				sql.Append(" \r\n");

				// Populate the generated data temp table
				sql.Append(string.Format("INSERT INTO #{0} \r\n", tableName));
				sql.Append("   (Id, ");

				for (int i = 0; i < activeColumnsNonClear.Count(); i++)
				{
					if (i == 0)
						sql.Append(string.Format("[{0}]", activeColumnsNonClear[i].Name));
					else
						sql.Append(string.Format(", [{0}]", activeColumnsNonClear[i].Name));
				}
				sql.Append(") \r\n");

				for (int i = 0; i < table.RecordCount; i++)
				{
					sql.Append("   SELECT");
					for (int j = 0; j < activeColumnsNonClear.Count(); j++)
					{
						try
						{
							if (j == 0)
								sql.Append(string.Format(" {0}", i));

							sql.Append(string.Format(", '{0}'", activeColumnsNonClear[j].Data[i].Trim()));
						}
						catch (Exception ex)
						{
							Logging.LogError(string.Format("Error trying to add data for table {0}, column {1}, data index {2}, total count {3}",
								table.FullTableName, activeColumnsNonClear[j].Name, i, activeColumnsNonClear[j].Data.Count));

							Logging.LogException(ex);

							throw;
						}
					}

					if (i != table.RecordCount - 1)
						sql.Append("\r\n   UNION ALL \r\n");
				}

				if (isUserRun)
					sql.Append(string.Format("\r\nPRINT '########## [' + CONVERT(VARCHAR, GETDATE()) + ']: Finished setting up temp Obfuscation data for {0}' \r\nGO\r\n\r\n", table.FullTableName));

				sql.Append(" \r\n");
				sql.Append(" \r\n");
			}

			if (isUserRun)
				sql.Append(string.Format("\r\nPRINT '########## [' + CONVERT(VARCHAR, GETDATE()) + ']: Starting Obfuscating table: {0}' \r\nGO\r\n\r\n", table.FullTableName));

			sql.Append(string.Format("DECLARE @{0}RecId BIGINT \r\n", tableNameOnly));
			sql.Append(string.Format("DECLARE @{0}RowNum BIGINT \r\n", tableNameOnly));

			sql.Append(string.Format("SELECT TOP 1 @{0}RecId={1} FROM [{2}].[{3}] ORDER BY {1} ASC \r\n", tableNameOnly, primaryKey.Name, table.Schema, table.Name));
			sql.Append(string.Format("SET @{0}RowNum = 0 \r\n", tableNameOnly));
			sql.Append(string.Format("WHILE @{0}RowNum < {1} \r\n", tableNameOnly, table.RecordCount));
			sql.Append("BEGIN \r\n");
			sql.Append(string.Format("   UPDATE [{0}].[{1}] \r\n", table.Schema, table.Name));

			for (int i = 0; i < activeColumns.Count(); i++)
			{
				if (i == 0)
					if (activeColumns[i].GeneratorType == SystemConstants.ClearGeneratorGuid)
					{
						if (activeColumns[i].GeneratorData != null && activeColumns[i].GeneratorData.Count > 0 && (bool)activeColumns[i].GeneratorData[0])
							sql.Append(string.Format("      SET [{0}] = NULL \r\n", activeColumns[i].Name));
						else
							sql.Append(string.Format("      SET [{0}] = '' \r\n", activeColumns[i].Name));
					}
					else
						sql.Append(string.Format("      SET [{0}] = (SELECT {0} FROM #{1} WHERE Id = @{2}RowNum) \r\n", activeColumns[i].Name, tableName, tableNameOnly));
				else
					if (activeColumns[i].GeneratorType == SystemConstants.ClearGeneratorGuid)
					{
						if (activeColumns[i].GeneratorData != null && activeColumns[i].GeneratorData.Count > 0 && (bool)activeColumns[i].GeneratorData[0])
							sql.Append(string.Format("         ,[{0}] = NULL \r\n", activeColumns[i].Name));
						else
							sql.Append(string.Format("         ,[{0}] = '' \r\n", activeColumns[i].Name));
					}
					else
						sql.Append(string.Format("         ,[{0}] = (SELECT {0} FROM #{1} WHERE Id = @{2}RowNum) \r\n", activeColumns[i].Name, tableName, tableNameOnly));
			}

			sql.Append(string.Format("   WHERE [{0}] = @{1}RecId \r\n", primaryKey.Name, tableNameOnly));

			sql.Append(string.Format("   SET @{0}RowNum = @{0}RowNum + 1 \r\n", tableNameOnly));
			sql.Append(string.Format("   SELECT TOP 1 @{0}RecId={1} FROM [{2}].[{3}] WHERE {1} > @{0}RecId  ORDER BY {1} ASC \r\n", tableNameOnly, primaryKey.Name, table.Schema, table.Name));
			sql.Append("END \r\n");

			if (isUserRun)
				sql.Append("GO \r\n");

			sql.Append("\r\n");

			if (activeColumnsNonClear.Count > 0)
			{
				sql.Append(string.Format("DROP TABLE #{0}\r\n", tableName));

				if (isUserRun)
					sql.Append("GO \r\n");

				sql.Append("\r\n");
			}

			if (isUserRun)
				sql.Append(string.Format("\r\nPRINT '########## [' + CONVERT(VARCHAR, GETDATE()) + ']: Finished Obfuscating table: {0}' \r\nGO\r\n\r\n", table.FullTableName));

			return sql.ToString();
		}
	}
}