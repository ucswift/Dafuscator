using StructureMap.Configuration.DSL;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Providers.TokenReplacementProvider
{
	internal class ProviderRegistry : Registry
	{
		public ProviderRegistry()
		{
			For<ITokenReplacementProvider>().Use<TokenReplacement>();
		}
	}
}