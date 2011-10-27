using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface ILastNameGenerator
	{
		string GenerateLastName();
		List<string> GenerateLastNames(double count);
	}
}