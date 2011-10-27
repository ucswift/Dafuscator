using System;
using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model
{
	public class ObfuscationResult
	{
		public DateTime StartTimeStamp { get; set; }
		public DateTime FinsihedTimeStamp { get; set; }
		public Dictionary<string, int> TablesProcessed { get; set; }
		public Dictionary<string, string> ErroredTables { get; set; }
		public string DatabaseName { get; set; }

		public ObfuscationResult()
		{
			TablesProcessed = new Dictionary<string, int>();
			ErroredTables = new Dictionary<string, string>();
		}

		public TimeSpan TimeElapsed
		{
			get { return FinsihedTimeStamp - StartTimeStamp; }
		}
	}
}