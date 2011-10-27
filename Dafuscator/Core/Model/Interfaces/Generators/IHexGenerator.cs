using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IHexGenerator
	{
		string GenerateHexNumber();
		List<string> GenerateHexNumbers(double count);
	}
}