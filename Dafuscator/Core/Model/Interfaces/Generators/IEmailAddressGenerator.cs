using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IEmailAddressGenerator
	{
		string GenerateEmailAddress();
		List<string> GenerateEmailAddresses(double count, HashSet<string> existingColumnData);
	}
}