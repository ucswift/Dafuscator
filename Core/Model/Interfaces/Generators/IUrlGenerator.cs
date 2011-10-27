using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IUrlGenerator
	{
		string GenerateUrl(bool includePrefix);
		List<string> GenerateUrls(double count, bool includePrefix, HashSet<string> existingColumnData);
	}
}