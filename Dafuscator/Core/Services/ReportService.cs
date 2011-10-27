using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Services
{
	public class ReportService : IReportService
	{
		public void GenerateObfuscationReportForDatabase(Database database, string path)
		{
			List<IGeneratorInfo> generatorInfos = ObjectLocator.GetAllInstances<IGeneratorInfo>();

			StringBuilder report = new StringBuilder();
			report.AppendLine("");
			report.AppendLine("==========================================================");
			report.AppendLine("                      DAFUSCATOR                          ");
			report.AppendLine("==========================================================");
			report.AppendLine("");
			report.AppendLine("Report Type: Obfuscated Columns Report");
			report.AppendLine("Report Date: " + DateTime.Now);
			report.AppendLine("Database: " + database.ConnectionString.DatabaseName);
			report.AppendLine("");

			foreach (Table t in database.Tables)
			{
				List<Column> activeColumns =
					t.Columns.Where(x => x.GeneratorType != null && x.GeneratorType.Value != SystemConstants.DefaultGuid).ToList();

				if (activeColumns.Count > 0 || t.HandlerType != TableHandlerTypes.None)
				{
					report.AppendLine("Table: " + t.FullTableName);
					report.AppendLine("---------------------------------");

					if (t.HandlerType == TableHandlerTypes.None)
					{
						foreach (Column c in activeColumns)
						{
							report.AppendLine(string.Format("   Column: {0}      Generator: {1}", c.Name, generatorInfos.Where(x => x.Id == c.GeneratorType.Value).First().Name));
						}
					}
					else
					{
						if (t.HandlerType == TableHandlerTypes.Delete)
							report.AppendLine(string.Format("   All records in the table will be deleted."));
						else
							report.AppendLine(string.Format("   Table will be dropped from the Database."));
					}

					report.AppendLine("");
					report.AppendLine("");
				}
			}

			if (File.Exists(path))
				File.Delete(path);

			using (StreamWriter writer = new StreamWriter(path))
			{
				writer.Write(report.ToString());
			}
		}

		public void GenerateReportForObfucsationResult(ObfuscationResult obfuscationResult, string path)
		{
			StringBuilder report = new StringBuilder();
			report.AppendLine("");
			report.AppendLine("==========================================================");
			report.AppendLine("                      DAFUSCATOR                          ");
			report.AppendLine("==========================================================");
			report.AppendLine("");
			report.AppendLine("Report Type: Database Obfuscation Result Report");
			report.AppendLine("Report Date: " + DateTime.Now.Date);
			report.AppendLine("Database: " + obfuscationResult.DatabaseName);
			report.AppendLine("");
			report.AppendLine("Database Obfuscation Start: " + obfuscationResult.StartTimeStamp);
			report.AppendLine("Database Obfuscation Stop: " + obfuscationResult.FinsihedTimeStamp);
			report.AppendLine("Database Obfuscation Elapsed: " + obfuscationResult.TimeElapsed);
			report.AppendLine("");
			report.AppendLine("");

			var sorted = from r in obfuscationResult.TablesProcessed
									 orderby r.Key
									 select r;

			foreach (var d in sorted)
			{
				report.AppendLine(string.Format("{0}: {1}", d.Key, d.Value));
			}

			report.AppendLine("");
			report.AppendLine("                     Errors                               ");
			report.AppendLine("----------------------------------------------------------");
			report.AppendLine("");

			foreach (var d in obfuscationResult.ErroredTables)
			{
				report.AppendLine(string.Format("{0}: {1}", d.Key, d.Value));
				report.AppendLine("");
			}

			if (File.Exists(path))
				File.Delete(path);

			using (StreamWriter writer = new StreamWriter(path))
			{
				writer.Write(report.ToString());
			}
		}
	}
}