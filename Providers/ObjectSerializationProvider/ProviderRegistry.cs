using StructureMap.Configuration.DSL;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Providers.ObjectSerializationProvider
{
	internal class ProviderRegistry : Registry
	{
		public ProviderRegistry()
		{
			For<IObjectSerializationProvider>().Use<ObjectSerializationProvider>();
		}
	}
}