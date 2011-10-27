using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface ILoginNameGenerator
	{
		string GenerateLoginName();
		List<string> GenerateLoginNames(double count);
	}
}