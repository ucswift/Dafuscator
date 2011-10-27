using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IStateGenerator
	{
		string GenerateStateName();
		List<string> GenerateStateNames(double count);
	}
}