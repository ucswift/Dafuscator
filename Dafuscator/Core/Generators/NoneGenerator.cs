using System;
using System.Collections.Generic;
using System.Data.OleDb;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class NoneGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("00000000-0000-0000-0000-000000000000"); }
		}

		public string Name
		{
			get { return "None"; }
		}

		public Type Type
		{
			get { return typeof(INoneGenerator); }
		}

		public List<OleDbType> CompatibleDataTypes
		{
			get
			{
				return new List<OleDbType>
				       	{
									OleDbType.LongVarChar,
									OleDbType.LongVarWChar,
									OleDbType.VarChar,
									OleDbType.VarWChar,
									OleDbType.BigInt,
									OleDbType.Decimal,
									OleDbType.Double,
									OleDbType.Integer,
									OleDbType.Numeric,
									OleDbType.Single,
									OleDbType.SmallInt,
									OleDbType.TinyInt,
									OleDbType.UnsignedBigInt,
									OleDbType.UnsignedInt,
									OleDbType.UnsignedSmallInt,
									OleDbType.UnsignedTinyInt
				       	};
			}
		}
	}

	public class NoneGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("00000000-0000-0000-0000-000000000000"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			// TODO: None is even more strange then the clear generator, must look at refactoring/removing this explicit generator

			generatedData = new List<string>();

			return generatedData;
		}
	}

	public class NoneGenerator : INoneGenerator
	{

	}
}
