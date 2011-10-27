using System;
using System.Linq;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Services;

namespace WaveTech.Dafuscator.Services
{
	public class RefreshService : IRefreshService
	{
		private readonly IDatabaseInteractionService _databaseInteractionService;

		public RefreshService(IDatabaseInteractionService databaseInteractionService)
		{
			_databaseInteractionService = databaseInteractionService;
		}

		public Database RefreshDatabaseProject(Database database)
		{
			Database db = new Database();

			try
			{
				db.ConnectionString = database.ConnectionString;
				db.Tables = new NotifyList<Table>(_databaseInteractionService.GetTablesFromDatabase(database.ConnectionString));

				foreach (Table t in db.Tables)
				{
					Table oldTable = database.Tables.Where(x => x.FullTableName == t.FullTableName).SingleOrDefault();

					if (oldTable != null)
					{
						t.HandlerType = oldTable.HandlerType;
						t.RecordCount = oldTable.RecordCount;

						foreach (Column c in t.Columns)
						{
							Column oldColumn = oldTable.Columns.Where(x => x.Name == c.Name).SingleOrDefault();

							if (oldColumn != null)
							{
								c.GeneratorType = oldColumn.GeneratorType;
								c.GeneratorData = oldColumn.GeneratorData;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);
				throw;
			}

			return db;
		}
	}
}