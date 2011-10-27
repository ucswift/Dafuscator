using System;
using System.Collections.Generic;
using System.Data;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Providers;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Services
{
	public class PreviewService : IPreviewService
	{
		private readonly IDatabaseProvider _databaseProvider;

		public PreviewService(IDatabaseProvider databaseProvider)
		{
			_databaseProvider = databaseProvider;
		}

		public List<DataTable> GetPreviewDataForTable(Table table, ConnectionString conntectionString)
		{
			List<DataTable> previewData = new List<DataTable>();
			DataTable originalData = _databaseProvider.GetPreviewData(conntectionString, table);

			previewData.Add(originalData);

			return previewData;
		}
	}
}