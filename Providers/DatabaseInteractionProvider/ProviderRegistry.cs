using StructureMap.Configuration.DSL;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Providers.DatabaseInteractionProvider
{
	internal class ProviderRegistry : Registry
	{
		public ProviderRegistry()
		{
			For<IDatabaseProvider>().Use<DatabaseProvider>();
		}
	}
}