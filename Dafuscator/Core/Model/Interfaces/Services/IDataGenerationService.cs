using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Services
{
	public interface IDataGenerationService
	{
		Table GenerateDataForTable(ConnectionString connectionString, Table table, bool updateRecordCount);
		Column GenerateDataForColumn(Column column, object[] data, HashSet<string> existingColumnData);
	}
}