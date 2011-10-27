using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IClearGenerator
	{
		string GenerateClear(bool emptyInstead);
		List<string> GenerateClears(double count, bool emptyInstead);
	}
}