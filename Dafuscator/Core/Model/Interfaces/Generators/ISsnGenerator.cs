using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface ISsnGenerator
	{
		string GenerateSocialSecurityNumber();
		List<string> GenerateSocialSecurityNumbers(double count);
	}
}