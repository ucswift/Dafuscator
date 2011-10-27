using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IStringGenerator
	{
		string GenerateRandomString(int minLength, int maxLength, bool includeSpecialCharacters, bool includeNumbers);
		List<string> GenerateRandomStrings(double count, int minLength, int maxLength, bool includeSpecialCharacters, bool includeNumbers, HashSet<string> existingColumnData);
	}
}