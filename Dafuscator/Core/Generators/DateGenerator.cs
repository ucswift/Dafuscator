using System;
using System.Collections.Generic;
using System.Data.OleDb;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class DateGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("577E56A7-83BD-4087-B042-4FFA54E5F193"); }
		}

		public string Name
		{
			get { return "Random Date Generator"; }
		}

		public Type Type
		{
			get { return typeof(IDateGenerator); }
		}

		public List<OleDbType> CompatibleDataTypes
		{
			get
			{
				return new List<OleDbType>
				       	{
									OleDbType.LongVarChar,
									OleDbType.LongVarWChar,
									OleDbType.VarChar,
									OleDbType.VarWChar,
									OleDbType.Date,
									OleDbType.DBDate
				       	};
			}
		}
	}

	public class DateGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("577E56A7-83BD-4087-B042-4FFA54E5F193"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
			{
				var dates = ((IDateGenerator)generator).GenerateDate((double)data[0], (DateTime)data[1], (DateTime)data[2]);

				generatedData = new List<string>();
				foreach (var d in dates)
				{
					generatedData.Add(d.ToString());
				}
			}

			return generatedData;
		}
	}

	public class DateGenerator : IDateGenerator
	{
		public DateTime GenerateDate(DateTime minDate, DateTime maxDate)
		{
			// http://jberke.blogspot.com/2008/01/calculating-random-date-in-c.html

			Random rand = new Random(DateTime.Now.Millisecond);
			TimeSpan timeSpan = maxDate - minDate;
			TimeSpan randomSpan = new TimeSpan((long)(timeSpan.Ticks * rand.NextDouble()));

			return minDate + randomSpan;
		}

		public List<DateTime> GenerateDate(double count, DateTime minDate, DateTime maxDate)
		{
			List<DateTime> dates = new List<DateTime>();

			for (double i = 0; i < count; i++)
			{
				dates.Add(GenerateDate(minDate, maxDate));
			}

			return dates;
		}
	}
}
