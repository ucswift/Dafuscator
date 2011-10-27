using StructureMap.Configuration.DSL;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Services
{
	internal class ServicesRegistry : Registry
	{
		public ServicesRegistry()
		{
			For<IDatabaseProjectService>().Use<DatabaseProjectService>();
			For<IDataGenerationService>().Use<DataGenerationService>();
			For<ISqlGenerationService>().Use<SqlGenerationService>();
			For<IExportService>().Use<ExportService>();
			For<IDatabaseInteractionService>().Use<DatabaseInteractionService>();
			For<IPreviewService>().Use<PreviewService>();
			For<IReportService>().Use<ReportService>();
			For<IRefreshService>().Use<RefreshService>();
			For<IRunService>().Use<RunService>();
		}
	}
}