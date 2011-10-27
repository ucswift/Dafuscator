using System.Collections.Generic;
using System.Data;

namespace WaveTech.Dafuscator.Model.Interfaces.Providers
{
	public interface IDatabaseProvider
	{
		List<Table> GetSchemaInformation(string connectionInfo);
		List<string> GetPrimaryKeysForTable(string connectionInfo, Table table);
		double GetRecordCount(string connectionInfo, Table table);
		bool TestConnection(ConnectionString connectionString);
		DataTable GetPreviewData(ConnectionString connectionString, Table table);
		HashSet<string> GetDataForColumn(string connectionInfo, Table table, Column column);
		int ProcessSql(string connectionString, string sql);
	}
}