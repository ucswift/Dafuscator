using StructureMap;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Generators;
using WaveTech.Dafuscator.Services;

namespace DafuscatorBatch
{
	internal static class Bootstrapper
	{
		private static bool IsInitialized;

		public static void Configure()
		{
			if (!IsInitialized)
			{
				ObjectFactory.Configure(scanner =>
				{
					scanner.AddRegistry(new FrameworkRegistry());
					scanner.AddRegistry(new ServicesRegistry());

#if CE
				  scanner.AddRegistry(new DemoGeneratorRegistry());
#else
					scanner.AddRegistry(new GeneratorRegistry());
#endif

					scanner.AddRegistry(new WaveTech.Dafuscator.Providers.SymmetricEncryptionProvider.ProviderRegistry());
					scanner.AddRegistry(new WaveTech.Dafuscator.Providers.DatabaseInteractionProvider.ProviderRegistry());
					scanner.AddRegistry(new WaveTech.Dafuscator.Providers.FileDataProvider.ProviderRegistry());
					scanner.AddRegistry(new WaveTech.Dafuscator.Providers.ObjectSerializationProvider.ProviderRegistry());
					scanner.AddRegistry(new WaveTech.Dafuscator.Providers.TokenReplacementProvider.ProviderRegistry());
					scanner.AddRegistry(new WaveTech.Dafuscator.Repositories.DatabaseProjectRepository.RepositoryRegistry());
				}
					);

				ObjectLocator.IsInitialized = true;
				IsInitialized = true;
			}
		}
	}
}
