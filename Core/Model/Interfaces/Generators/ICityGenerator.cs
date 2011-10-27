using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface ICityGenerator
	{
		string GenerateCityName();
		List<string> GenerateCityNames(double count);
	}
}