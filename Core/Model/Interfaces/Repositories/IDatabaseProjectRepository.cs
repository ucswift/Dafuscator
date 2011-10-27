namespace WaveTech.Dafuscator.Model.Interfaces.Repositories
{
	/// <summary>
	/// The repoistory for the database project data.
	/// </summary>
	public interface IDatabaseProjectRepository
	{
		Database GetDatabaseProject(string path);
		Database SaveDatabaseProject(Database databaseProject, string path);
	}
}