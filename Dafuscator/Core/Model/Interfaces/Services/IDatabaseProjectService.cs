namespace WaveTech.Dafuscator.Model.Interfaces.Services
{
	public interface IDatabaseProjectService
	{
		Database GetDatabaseProject(string path);
		Database SaveDatabaseProject(Database databaseProject, string path);
	}
}