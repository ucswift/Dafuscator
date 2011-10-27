using System;
using System.Collections.Generic;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.WpfApplication.Classes
{
	public class UIContext
	{
		#region Private ReadOnly Members
		private static readonly IDatabaseProvider _databaseProvider = ObjectLocator.GetInstance<IDatabaseProvider>();
		#endregion Private ReadOnly Members

		#region Private Members
		private static bool _newConnection;
		private static Database _database;
		#endregion Private Members

		#region Public Properties
		public static Database Database
		{
			get { return _database; }
			set
			{
				if (value != null || value != _database)
				{
					_database = value;
					_newConnection = true;
				}
			}
		}
		#endregion Public Properties

		#region Public Methods
		/// <summary>
		/// Gets an enumeration of all root <see cref="T:Table"/>s.
		/// </summary>
		public static Database GetDatabase()
		{
			if (_database != null && _newConnection)
			{
				if (_database.Tables == null || _database.Tables.Count == 0 || !_database.AreAnyGeneratorsActive())
				{
					try
					{
						_database.Tables =
							new NotifyList<Table>(_databaseProvider.GetSchemaInformation(_database.ConnectionString.GetConnectionString()));
					}
					catch (Exception ex)
					{
						Logging.LogException(ex);
						throw;
					}
				}
			}

			return _database;
		}

		public static List<IGeneratorInfo> GetGenerators()
		{
			List<IGeneratorInfo> generatorInfos = new List<IGeneratorInfo>();

			List<IGeneratorInfo> data = ObjectLocator.GetAllInstances<IGeneratorInfo>();

			foreach (var o in data)
			{
				generatorInfos.Add(o);
			}

			return generatorInfos;
		}
		#endregion Public Methods
	}
}