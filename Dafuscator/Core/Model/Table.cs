using System;
using System.Linq;

namespace WaveTech.Dafuscator.Model
{
	public class Table : BaseObject
	{
		#region Private Properties
		private string _name;
		private string _schema;
		public NotifyList<Column> Columns { get; set; }
		private double _recordCount;
		private TableHandlerTypes _handlerType;
		#endregion Private Properties

		#region Constructor
		public Table()
		{
			Columns = new NotifyList<Column>();
			Columns.ItemPropertyChanged += Columns_ItemPropertyChanged;
			HandlerType = TableHandlerTypes.None;
		}
		#endregion Constructor

		#region Private Event Handlers
		private void Columns_ItemPropertyChanged(object sender, NotifyEventArgs<Column> e)
		{
			OnPropertyChanged("AreAnyGeneratorsActive");
		}
		#endregion Private Event Handlers

		#region Public Properties
		public Column GetPrimaryKeyColumn()
		{
			return Columns.Where(x => x.IsPrimaryKey).FirstOrDefault();
		}

		public bool AreAnyGeneratorsActive
		{
			get
			{
				if (_handlerType != TableHandlerTypes.None)
					return true;

				var activeGenerators = Columns.Where(x => x.GeneratorType != new Guid("00000000-0000-0000-0000-000000000000"));

				if (activeGenerators != null)
				{
					if (activeGenerators.Count() > 0)
						return true;
					else
						return false;
				}

				return false;
			}
		}

		public string Name
		{
			get
			{
				return _name;
			}

			set
			{
				if (value != _name)
				{
					_name = value;
					OnPropertyChanged("Name");
				}
			}
		}

		public string Schema
		{
			get
			{
				return _schema;
			}

			set
			{
				if (value != _schema)
				{
					_schema = value;
					OnPropertyChanged("Schema");
				}
			}
		}

		public double RecordCount
		{
			get
			{
				return _recordCount;
			}

			set
			{
				if (value != _recordCount)
				{
					_recordCount = value;
					OnPropertyChanged("RecordCount");
				}
			}
		}

		public string FullTableName
		{
			get { return string.Format("{0}.{1}", _schema, _name); }
		}

		public TableHandlerTypes HandlerType
		{
			get
			{
				return _handlerType;
			}

			set
			{
				if (value != _handlerType)
				{
					_handlerType = value;
					OnPropertyChanged("HandlerType");
				}
			}
		}
		#endregion Public Properties
	}
}