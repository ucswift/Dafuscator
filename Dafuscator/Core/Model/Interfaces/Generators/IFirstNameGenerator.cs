using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IFirstNameGenerator
	{
		string GenerateFirstName();
		List<string> GenerateFirstNames(double count);
	}
}