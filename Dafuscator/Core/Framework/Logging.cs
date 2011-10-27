using System;
using System.IO;
using System.Reflection;
using NLog;
using NLog.Targets;
using NLog.Config;

namespace WaveTech.Dafuscator.Framework
{
	public static class Logging
	{
		private static LoggingConfiguration _config;
		private static Logger _logger;
		private static bool _isInitialized;

		private static void Initialize()
		{
			if (_isInitialized == false)
			{
				// Step 1. Create configuration object 
				_config = new LoggingConfiguration();

				// Step 2. Create targets and add them to the configuration 
				//ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
				//config.AddTarget("console", consoleTarget);

				FileTarget fileTarget = new FileTarget();
				_config.AddTarget("file", fileTarget);

				// Step 3. Set target properties 
				//consoleTarget.Layout = "${date:format=HH\\:MM\\:ss}: ${message}";

				string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
				path = path.Replace("file:\\", "");

				// set the file 
				fileTarget.FileName = path + "\\log.txt";
				fileTarget.Layout = "${date:format=HH\\:MM\\:ss}: ${message}";

				// don’t clutter the hard drive
				fileTarget.DeleteOldFileOnStartup = true;

				// Step 4. Define rules 
				//LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
				//config.LoggingRules.Add(rule1);

        LoggingRule rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
				_config.LoggingRules.Add(rule2);

				// Step 5. Activate the configuration 
				LogManager.Configuration = _config;

				_logger = LogManager.GetLogger("Dafuscator");

				_isInitialized = true;
			}
		}

		public static void LogException(Exception exception)
		{
			Initialize();

			_logger.LogException(LogLevel.Fatal, exception.ToString(), exception);
		}

		public static void LogError(string message)
		{
			Initialize();

			_logger.Error(message);
		}
	}
}