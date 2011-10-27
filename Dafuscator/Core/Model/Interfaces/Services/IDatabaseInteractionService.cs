using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Services
{
	public interface IDatabaseInteractionService
	{
		bool TestConnection(ConnectionString connectionString);
		double GetRecrodCountForTable(ConnectionString connectionString, Table table);
		List<Table> GetTablesFromDatabase(ConnectionString connectionString);
		HashSet<string> GetDataForColumn(ConnectionString connectionString, Table table, Column column);
		int ProcessSql(ConnectionString connectionString, string sql);
	}
}