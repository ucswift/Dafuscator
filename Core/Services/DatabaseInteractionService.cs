using System.Collections.Generic;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Providers;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Services
{
	public class DatabaseInteractionService : IDatabaseInteractionService
	{
		private readonly IDatabaseProvider _databaseProvider;

		public DatabaseInteractionService(IDatabaseProvider databaseProvider)
		{
			_databaseProvider = databaseProvider;
		}

		public bool TestConnection(ConnectionString connectionString)
		{
			return _databaseProvider.TestConnection(connectionString);
		}

		public double GetRecrodCountForTable(ConnectionString connectionString, Table table)
		{
			return _databaseProvider.GetRecordCount(connectionString.GetConnectionString(), table);
		}

		public List<Table> GetTablesFromDatabase(ConnectionString connectionString)
		{
			return _databaseProvider.GetSchemaInformation(connectionString.GetConnectionString());
		}

		public HashSet<string> GetDataForColumn(ConnectionString connectionString, Table table, Column column)
		{
			return _databaseProvider.GetDataForColumn(connectionString.GetConnectionString(), table, column);
		}

		public int ProcessSql(ConnectionString connectionString, string sql)
		{
			return _databaseProvider.ProcessSql(connectionString.GetConnectionString(), sql);
		}
	}
}