using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface INumberGenerator
	{
		int GenerateRandomNumber(int min, int max);
		List<int> GenerateRandomNumbers(double count, int min, int max);
		int GenerateQuickRandomNumber(int min, int max);
	}
}