using StructureMap.Configuration.DSL;
using WaveTech.Dafuscator.Model.Interfaces.Repositories;

namespace WaveTech.Dafuscator.Repositories.DatabaseProjectRepository
{
	internal class RepositoryRegistry : Registry
	{
		public RepositoryRegistry()
		{
			For<IDatabaseProjectRepository>().Use<DatabaseProjectRepository>();
		}
	}
}