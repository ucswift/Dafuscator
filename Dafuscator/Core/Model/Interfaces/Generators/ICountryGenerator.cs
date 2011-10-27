using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface ICountryGenerator
	{
		string GenerateCountryName();
		List<string> GenerateCountryNames(double count);
	}
}