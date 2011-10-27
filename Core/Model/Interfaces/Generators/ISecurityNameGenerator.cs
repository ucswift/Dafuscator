using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface ISecurityNameGenerator
	{
		string GenerateSecurityName();
		List<string> GenerateSecurityNames(double count, HashSet<string> existingSecurityNames);
	}
}