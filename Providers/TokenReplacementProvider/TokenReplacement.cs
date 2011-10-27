using System;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Providers.TokenReplacementProvider
{
	public class TokenReplacement : ITokenReplacementProvider
	{
		private readonly INumberGenerator numberGenerator;
		private readonly ICharacterGenerator characterGenerator;

		public TokenReplacement(INumberGenerator numberGenerator, ICharacterGenerator characterGenerator)
		{
			this.numberGenerator = numberGenerator;
			this.characterGenerator = characterGenerator;
		}

		public string ProcessToken(string token)
		{
			char[] accountNumberArray = token.ToCharArray();
			for (int i = 0; i < accountNumberArray.Length; i++)
			{
				if (accountNumberArray[i] == char.Parse("X"))
				{
					accountNumberArray[i] = characterGenerator.GenerateRandomCharacter(true);
				}
				else if (accountNumberArray[i] == char.Parse("A"))
				{
					accountNumberArray[i] = characterGenerator.GenerateRandomCharacter(false);
				}
				else if (accountNumberArray[i] == char.Parse("#"))
				{
					accountNumberArray[i] = char.Parse(numberGenerator.GenerateRandomNumber(0, 9).ToString());
				}
			}

			return new string(accountNumberArray);
		}
	}
}