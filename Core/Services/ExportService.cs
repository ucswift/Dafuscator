using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Services
{
	public class ExportService : IExportService
	{
		private readonly ISqlGenerationService _sqlGenerationService;
		private readonly IDataGenerationService _dataGenerationService;

		public ExportService(ISqlGenerationService sqlGenerationService, IDataGenerationService dataGenerationService)
		{
			_sqlGenerationService = sqlGenerationService;
			_dataGenerationService = dataGenerationService;
		}

		public void ExportTable(Table table, string path, ConnectionString connectionString)
		{
			try
			{
				if (File.Exists(path))
					File.Delete(path);

				StringBuilder sb = new StringBuilder();
				sb.Append("SET NOCOUNT ON\r\n\r\n");

				table = _dataGenerationService.GenerateDataForTable(connectionString, table, true);
				string sql = _sqlGenerationService.GenerateUpdateSqlForTable(table, true);

				sb.Append(sql);

				sb.Append("SET NOCOUNT OFF\r\n\r\n");

				using (StreamWriter writer = new StreamWriter(path))
				{
					writer.Write(sb.ToString());
				}
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);
				throw;
			}
		}

		public void ExportTables(IEnumerable<Table> tables, string path, ConnectionString connectionString)
		{
			try
			{
				if (File.Exists(path))
					File.Delete(path);

				StringBuilder sb = new StringBuilder();
				sb.Append("SET NOCOUNT ON\r\n\r\n");

				foreach (Table t in tables)
				{
					Table table = _dataGenerationService.GenerateDataForTable(connectionString, t, true);
					sb.Append(_sqlGenerationService.GenerateUpdateSqlForTable(table, true));
				}

				sb.Append("SET NOCOUNT OFF\r\n\r\n");

				using (StreamWriter writer = new StreamWriter(path))
				{
					writer.Write(sb);
				}
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);
				throw;
			}
		}

		public void ExportTestTable(Table table, string path, ConnectionString connectionString)
		{
			try
			{
				if (File.Exists(path))
					File.Delete(path);

				StringBuilder sb = new StringBuilder();

				sb.Append("BEGIN TRANSACTION\r\n\r\n");
				sb.Append("SET NOCOUNT ON\r\n\r\n");

				if (table.RecordCount > 1000)
					table.RecordCount = 1000;

				table = _dataGenerationService.GenerateDataForTable(connectionString, table, false);
				string sql = _sqlGenerationService.GenerateUpdateSqlForTable(table, true);

				sb.Append(sql);
				sb.Append("SET NOCOUNT OFF\r\n\r\n");
				sb.Append("\r\n\r\nROLLBACK TRANSACTION\r\n\r\n");

				using (StreamWriter writer = new StreamWriter(path))
				{
					writer.Write(sql);
				}
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);
				throw;
			}
		}

		public void ExportTestTables(IEnumerable<Table> tables, string path, ConnectionString connectionString)
		{
			try
			{
				if (File.Exists(path))
					File.Delete(path);

				StringBuilder sb = new StringBuilder();
				sb.Append("BEGIN TRANSACTION\r\n\r\n");
				sb.Append("SET NOCOUNT ON\r\n\r\n");

				foreach (Table t in tables)
				{
					if (t.RecordCount > 1000)
						t.RecordCount = 1000;

					Table table = _dataGenerationService.GenerateDataForTable(connectionString, t, false);
					sb.Append(_sqlGenerationService.GenerateUpdateSqlForTable(table, true));
				}

				sb.Append("SET NOCOUNT OFF\r\n\r\n");
				sb.Append("\r\n\r\nROLLBACK TRANSACTION\r\n\r\n");

				using (StreamWriter writer = new StreamWriter(path))
				{
					writer.Write(sb);
				}
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);
				throw;
			}
		}
	}
}