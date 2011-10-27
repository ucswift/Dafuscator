using StructureMap.Configuration.DSL;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Providers.FileDataProvider
{
	internal class ProviderRegistry : Registry
	{
		public ProviderRegistry()
		{
			For<IFileDataProvider>().Use<FileData>();
		}
	}
}