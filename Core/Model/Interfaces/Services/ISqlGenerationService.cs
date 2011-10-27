using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Services
{
	public interface ISqlGenerationService
	{
		string GenerateUpdateSql(List<Table> tables, bool isUserRun);
		string GenerateUpdateSqlForTable(Table table, bool isUserRun);
	}
}