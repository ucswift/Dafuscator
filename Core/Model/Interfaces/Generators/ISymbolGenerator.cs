using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface ISymbolGenerator
	{
		string GenerateSymbol();
		List<string> GenerateSymbols(double count, HashSet<string> existingSecurityNames);
	}
}