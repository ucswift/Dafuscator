using System;
using System.IO;
using CommandLine;
using CommandLine.Text;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Events;
using WaveTech.Dafuscator.Model.Interfaces.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace DafuscatorBatch
{
	public class Program
	{
		private static readonly HeadingInfo _headingInfo = new HeadingInfo("Dafuscator Batch", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

		static void Main(string[] args)
		{
			Bootstrapper.Configure();

			_headingInfo.WriteMessage(" ");

			var options = new Options(_headingInfo);
			ICommandLineParser parser = new CommandLineParser(new CommandLineParserSettings(Console.Error));

			if (!parser.ParseArguments(args, options))
				Environment.Exit(1);

			PerformTask(options);

			Environment.Exit(0);
		}


		private static void PerformTask(Options options)
		{
			if (!String.IsNullOrEmpty(options.ProjectFile))
			{
				IDatabaseProjectService databaseProjectService = ObjectLocator.GetInstance<IDatabaseProjectService>();
				IEventAggregator eventAggregator = ObjectLocator.GetInstance<IEventAggregator>();

				Console.WriteLine(string.Format("Loading the Dafuscator project: {0}", options.ProjectFile));
				Database db = databaseProjectService.GetDatabaseProject(options.ProjectFile);
				ConnectionString connetionString;

				if (!String.IsNullOrEmpty(options.ConnectionString))
					connetionString = new ConnectionString(options.ConnectionString);
				else
					connetionString = db.ConnectionString;

				eventAggregator.AddListener<StatusUpdateEvent>(e => Console.WriteLine(string.Format("{0}: {1}", DateTime.Now, e.Message)));

				if (!String.IsNullOrEmpty(options.ExportFile))
				{
					Console.WriteLine(string.Format("Started exporting the Dafuscator project to {0}", options.ExportFile));
					IExportService exportService = ObjectLocator.GetInstance<IExportService>();
					exportService.ExportTables(db.Tables, options.ExportFile, connetionString);

					Console.WriteLine("Finished exporting the Dafuscator project.");
				}
				else
				{
					Console.WriteLine(string.Format("Started the obfuscation of the {0} database.", db.ConnectionString.DatabaseName));

					IRunService runService = ObjectLocator.GetInstance<IRunService>();
					IReportService reportService = ObjectLocator.GetInstance<IReportService>();

					ObfuscationResult result = runService.ObfuscateDatabase(db);

					string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					path = path + "\\Dafuscator";

					if (Directory.Exists(path) == false)
						Directory.CreateDirectory(path);

					path = path +
								 string.Format("\\DatabaseObfuscationReport_{0}_{1}_{2}-{3}_{4}.txt", DateTime.Now.Month, DateTime.Now.Day,
															 DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute);

					reportService.GenerateReportForObfucsationResult(result, path);

					Console.WriteLine("Finished the obfuscation process in {0}", result.TimeElapsed);
				}
			}
		}
	}
}