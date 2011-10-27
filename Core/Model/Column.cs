using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace WaveTech.Dafuscator.Model
{
	public class Column : BaseObject
	{
		#region Private Members
		private string _name;
		private bool _isPrimaryKey;
		private bool _isForignKey;
		private bool _isNullable;
		private bool _isPartOfConstraint;
		private Guid? _generatorType;
		private List<object> _generatorData;
		private int? _maxLength;
		#endregion Private Members

		#region Constructor
		public Column()
		{
			_generatorData = new List<object>(10);
			_generatorType = new Guid("00000000-0000-0000-0000-000000000000");
		}
		#endregion Constructor

		#region Public Auto Properties
		public OleDbType DataType { get; set; }
		public List<string> Data { get; set; }
		#endregion Public Auto Properties

		#region Public Properties
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

		public bool IsPrimaryKey
		{
			get
			{
				return _isPrimaryKey;
			}

			set
			{
				if (value != _isPrimaryKey)
				{
					_isPrimaryKey = value;
					OnPropertyChanged("IsPrimaryKey");
				}
			}
		}

		public bool IsForignKey
		{
			get
			{
				return _isForignKey;
			}

			set
			{
				if (value != _isForignKey)
				{
					_isForignKey = value;
					OnPropertyChanged("IsForignKey");
				}
			}
		}

		public bool IsNullable
		{
			get
			{
				return _isNullable;
			}

			set
			{
				if (value != _isNullable)
				{
					_isNullable = value;
					OnPropertyChanged("IsNullable");
				}
			}
		}

		public int? MaxLength
		{
			get
			{
				return _maxLength;
			}

			set
			{
				if (value != _maxLength)
				{
					_maxLength = value;
					OnPropertyChanged("MaxLength");
				}
			}
		}

		public bool IsPartOfConstraint
		{
			get
			{
				return _isPartOfConstraint;
			}

			set
			{
				if (value != _isPartOfConstraint)
				{
					_isPartOfConstraint = value;
					OnPropertyChanged("IsPartOfConstraint");
				}
			}
		}

		public Guid? GeneratorType
		{
			get
			{
				return _generatorType;
			}

			set
			{
				if (value != _generatorType)
				{
					_generatorType = value;
					OnPropertyChanged("GeneratorType");
				}
			}
		}

		public List<object> GeneratorData
		{
			get
			{
				//if (_generatorData == null || _generatorData.Count <= 0)
				//	_generatorData = new List<object>(10);

				return _generatorData;
			}

			set
			{
				if (value != _generatorData)
				{
					_generatorData = value;

					// Clean up null data
					//for (int i = 0; i < _generatorData.Count; i++)
					//{
					//  if (_generatorData[i] == null)
					//    _generatorData.RemoveAt(i);
					//}

					OnPropertyChanged("GeneratorData");
				}
			}
		}

		public string Type
		{
			get
			{
				return DataType.ToString();
			}
		}
		#endregion Public Properties

		public bool IsValid()
		{
			bool isValid = true;

			if (GeneratorType == new Guid("A7AC88F5-8C61-4F3E-8066-23A4CCF19ED5"))	// Character Generator
			{
				if (_generatorData == null || _generatorData.Count < 1)
					return false;

				bool test;

				if (_generatorData[0] == null || bool.TryParse(_generatorData[0].ToString(), out test) == false)
					return false;
			}

			else if (GeneratorType == new Guid("577E56A7-83BD-4087-B042-4FFA54E5F193")) // Date Generator
			{
				if (_generatorData == null || _generatorData.Count < 2)
					return false;

				DateTime test;

				if (_generatorData[0] == null || String.IsNullOrEmpty(_generatorData[0].ToString()) || DateTime.TryParse(_generatorData[0].ToString(), out test) == false)
					return false;

				if (_generatorData[1] == null || String.IsNullOrEmpty(_generatorData[1].ToString()) || DateTime.TryParse(_generatorData[1].ToString(), out test) == false)
					return false;
			}

			else if (GeneratorType == new Guid("68290CBD-A327-41BE-A9E6-D6FFD089B953")) // Number Generator
			{
				if (_generatorData == null || _generatorData.Count < 1)
					return false;

				int test;

				if (_generatorData[0] == null || String.IsNullOrEmpty(_generatorData[0].ToString()) || int.TryParse(_generatorData[0].ToString(), out test) == false)
					return false;

				if (_generatorData[1] == null || String.IsNullOrEmpty(_generatorData[1].ToString()) || int.TryParse(_generatorData[1].ToString(), out test) == false)
					return false;
			}

			else if (GeneratorType == new Guid("0086042D-C5E1-4013-9901-2FABDD679136")) // Phone Number Generator
			{
				if (_generatorData == null || _generatorData.Count < 1)
					return false;

				bool test;

				if (_generatorData[0] == null || bool.TryParse(_generatorData[0].ToString(), out test) == false)
					return false;
			}

			else if (GeneratorType == new Guid("7A9EC03D-6713-4C57-84D5-65A45DD3854F")) // String Generator
			{
				if (_generatorData == null || _generatorData.Count < 4)
					return false;

				int test;
				bool test1;

				if (_generatorData[0] == null || String.IsNullOrEmpty(_generatorData[0].ToString()) || int.TryParse(_generatorData[0].ToString(), out test) == false)
					return false;

				if (_generatorData[1] == null || String.IsNullOrEmpty(_generatorData[1].ToString()) || int.TryParse(_generatorData[1].ToString(), out test) == false)
					return false;

				if (_generatorData[2] == null || bool.TryParse(_generatorData[2].ToString(), out test1) == false)
					return false;

				if (_generatorData[3] == null || bool.TryParse(_generatorData[3].ToString(), out test1) == false)
					return false;
			}

			else if (GeneratorType == new Guid("08707085-1263-497E-B008-1CCE0C02EA05")) // Zip Code Generator
			{
				if (_generatorData == null || _generatorData.Count < 1)
					return false;

				bool test;

				if (_generatorData[0] == null || bool.TryParse(_generatorData[0].ToString(), out test) == false)
					return false;
			}

			else if (GeneratorType == new Guid("815C682E-0690-48E7-8F7F-75BCD47DC3E6")) // Clear Generator
			{
				if (_generatorData == null || _generatorData.Count < 1)
					return false;

				bool test;

				if (_generatorData[0] == null || bool.TryParse(_generatorData[0].ToString(), out test) == false)
					return false;
			}

			else if (GeneratorType == new Guid("8440D22A-7ACD-4359-A5D7-3347F933DA54")) // Full Name Generator
			{
				if (_generatorData == null || _generatorData.Count < 2)
					return false;

				bool test;

				if (_generatorData[0] == null || bool.TryParse(_generatorData[0].ToString(), out test) == false)
					return false;

				if (_generatorData[1] == null || bool.TryParse(_generatorData[1].ToString(), out test) == false)
					return false;
			}

			else if (GeneratorType == new Guid("D98D89C8-ECAE-4AB3-897A-8478B0A6EC89")) // Token Generator
			{
				if (_generatorData == null || _generatorData.Count < 1)
					return false;

				if (_generatorData[0] == null || String.IsNullOrEmpty(_generatorData[0].ToString()))
					return false;
			}

			return isValid;
		}
	}
}