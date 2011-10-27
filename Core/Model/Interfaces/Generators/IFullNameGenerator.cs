using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IFullNameGenerator
	{
		string GenerateFullName(bool includeMiddle, bool middleFullName);
		List<string> GenerateFullNames(double count, bool includeMiddle, bool middleFullName);
	}
}