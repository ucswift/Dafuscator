using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IZipCodeGenerator
	{
		string GenerateZipCode(bool append4DigitCode);
		List<string> GenerateZipCodes(double count, bool append4DigitCode);
	}
}