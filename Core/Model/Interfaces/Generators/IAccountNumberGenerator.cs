using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IAccountNumberGenerator
	{
		string GenerateAccountNumber();
		List<string> GenerateAccountNumbers(double count);
	}
}