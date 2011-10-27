using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Services
{
	public interface IExportService
	{
		void ExportTable(Table table, string path, ConnectionString connectionString);
		void ExportTables(IEnumerable<Table> tables, string path, ConnectionString connectionString);

		void ExportTestTable(Table table, string path, ConnectionString connectionString);
		void ExportTestTables(IEnumerable<Table> tables, string path, ConnectionString connectionString);
	}
}