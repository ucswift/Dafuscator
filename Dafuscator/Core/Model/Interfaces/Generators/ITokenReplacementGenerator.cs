using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface ITokenReplacementGenerator
	{
		string GenerateReplacedString(string token);
		List<string> GenerateReplacedStrings(double count, string token, HashSet<string> existingColumnData);
	}
}