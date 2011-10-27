using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Services
{
	public interface IRunService
	{
		ObfuscationResult ObfuscateDatabase(Database database);
		ObfuscationResult ObfuscateTable(ConnectionString connectionString, Table table);
	}
}