namespace WaveTech.Dafuscator.Model.Interfaces.Services
{
	public interface IReportService
	{
		void GenerateObfuscationReportForDatabase(Database database, string path);
		void GenerateReportForObfucsationResult(ObfuscationResult obfuscationResult, string path);
	}
}