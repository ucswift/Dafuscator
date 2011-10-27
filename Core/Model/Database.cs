namespace WaveTech.Dafuscator.Model
{
	public class Database: BaseObject
	{
		#region Private Members
		private ConnectionString _connectionString;
		private NotifyList<Table> _tables;
		#endregion Private Members

		#region Constructor
		public Database()
		{
			_tables = new NotifyList<Table>();
		}
		#endregion Constructor

		#region Public Properties
		public ConnectionString ConnectionString
		{
			get
			{
				return _connectionString;
			}

			set
			{
				if (value != _connectionString)
				{
					_connectionString = value;
					OnPropertyChanged("ConnectionString");
				}
			}
		}

		public NotifyList<Table> Tables
		{
			get
			{
				return _tables;
			}

			set
			{
				if (value != _tables)
				{
					_tables = value;
					OnPropertyChanged("Tables");
				}
			}
		}
		#endregion Public Properties

		#region Public Methods
		public bool AreAnyGeneratorsActive()
		{
			foreach (Table t in Tables)
			{
				if (t.AreAnyGeneratorsActive)
					return true;
			}

			return false;
		}
		#endregion Public Methods
	}
}