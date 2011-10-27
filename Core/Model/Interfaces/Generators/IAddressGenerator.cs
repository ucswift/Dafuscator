using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IAddressGenerator
	{
		string GenerateAddressLine1();
		List<string> GenerateAddressLine1s(double count);
		string GenerateAddressLine2();
		List<string> GenerateAddressLine2s(double count);
	}
}