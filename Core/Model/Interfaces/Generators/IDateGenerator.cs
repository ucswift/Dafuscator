using System;
using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IDateGenerator
	{
		DateTime GenerateDate(DateTime minDate, DateTime maxDate);
		List<DateTime> GenerateDate(double count, DateTime minDate, DateTime maxDate);
	}
}