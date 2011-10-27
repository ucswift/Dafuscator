using System;
using CommandLine;
using CommandLine.Text;

namespace DafuscatorBatch
{
	public class Options
	{
		private HeadingInfo _headingInfo;

		public Options(HeadingInfo headingInfo)
		{
			_headingInfo = headingInfo;
		}

		[Option("p", "project",
						Required = true,
						HelpText = "The path to a Dafuscator project file")]
		public string ProjectFile = String.Empty;

		[Option("e", "export",
				HelpText = "Output file with generated SQL obfuscation UPDATE statements.")]
		public string ExportFile = String.Empty;

		[Option("c", "connction",
				HelpText = "An OLEDB connection string to override the project stored string")]
		public string ConnectionString = String.Empty;

		[HelpOption(
						HelpText = "Dispaly this help screen.")]
		public string GetUsage()
		{
			var help = new HelpText(_headingInfo);
			help.AdditionalNewLineAfterOption = true;
			help.Copyright = new CopyrightInfo("WaveTech Digital Technologies, Inc.", 2009, 2010);
			help.AddPreOptionsLine(" ");
			help.AddPreOptionsLine("This utility is the batch (command line) utility for the Dafuscator");
			help.AddPreOptionsLine("database data obfuscation system. This utility allows you to process");
			help.AddPreOptionsLine("a Dafuscator project (.daf) file againt a database, or to generate SQL.");
			help.AddPreOptionsLine(" ");
			help.AddPreOptionsLine(" ");
			help.AddPreOptionsLine("Usage: DafuscatorBatch -p\"C:\\My Solution\\Proj1\\ObfuscatedProdDB.daf\"");
			help.AddPreOptionsLine("       DafuscatorBatch -p\"C:\\My Solution\\Proj1\\ObfuscatedProdDB.daf\" -e\"C:\\My Solution\\Proj1\\ObfuscatedProdDB.SQL\" ");
			help.AddPreOptionsLine("       DafuscatorBatch -p\"C:\\My Solution\\Proj1\\ObfuscatedProdDB.daf\" -c\"Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=Northwind;Integrated Security=SSPI;\"");
			help.AddPreOptionsLine(" ");
			help.AddOptions(this);

			return help;
		}
	}
}