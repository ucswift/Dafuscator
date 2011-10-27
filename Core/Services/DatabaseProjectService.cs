using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Repositories;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Services
{
	public class DatabaseProjectService : IDatabaseProjectService
	{
		private readonly IDatabaseProjectRepository _databaseProjectRepository;

		public DatabaseProjectService(IDatabaseProjectRepository databaseProjectRepository)
		{
			_databaseProjectRepository = databaseProjectRepository;
		}

		public Database GetDatabaseProject(string path)
		{
			return _databaseProjectRepository.GetDatabaseProject(path);
		}

		public Database SaveDatabaseProject(Database databaseProject, string path)
		{
			return _databaseProjectRepository.SaveDatabaseProject(databaseProject, path);
		}
	}
}