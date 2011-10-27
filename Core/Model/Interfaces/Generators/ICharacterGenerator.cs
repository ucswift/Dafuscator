using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface ICharacterGenerator
	{
		char GenerateRandomCharacter(bool includeDigits);
		List<char> GenerateRandomCharacters(double count, bool includeDigits);
	}
}