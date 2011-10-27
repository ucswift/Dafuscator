using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface ICompanyNameGenerator
	{
		string GenerateCompanyName();
		string GenerateCompanyNameWithoutSuffix();
		List<string> GenerateCompanyNames(double count, HashSet<string> existingColumnData);
	}
}