using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IGuidGenerator
	{
		string GenerateGuid();
		List<string> GenerateGuids(double count);
	}
}