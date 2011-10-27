using StructureMap.Configuration.DSL;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Providers.SymmetricEncryptionProvider
{
	internal class ProviderRegistry : Registry
	{
		public ProviderRegistry()
		{
			For<ISymmetricEncryptionProvider>().Use<SymmetricEncryptionProvider>();
		}
	}
}