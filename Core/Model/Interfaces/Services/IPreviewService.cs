using System.Collections.Generic;
using System.Data;

namespace WaveTech.Dafuscator.Model.Interfaces.Services
{
	public interface IPreviewService
	{
		List<DataTable> GetPreviewDataForTable(Table table, ConnectionString conntectionString);
	}
}