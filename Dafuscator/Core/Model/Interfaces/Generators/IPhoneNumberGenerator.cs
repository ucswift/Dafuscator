using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IPhoneNumberGenerator
	{
		string GeneratePhoneNumber(bool includeAreaCode);
		List<string> GeneratePhoneNumbers(double count, bool includeAreaCode);
	}
}