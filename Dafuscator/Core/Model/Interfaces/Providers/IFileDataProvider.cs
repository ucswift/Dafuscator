using System.Collections.Generic;
using System.Reflection;

namespace WaveTech.Dafuscator.Model.Interfaces.Providers
{
	public interface IFileDataProvider
	{
		List<string> GetDataFromEmbededFile(Assembly assembly, string name);
	}
}