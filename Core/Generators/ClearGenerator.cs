using System;
using System.Collections.Generic;
using System.Data.OleDb;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class ClearGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("815C682E-0690-48E7-8F7F-75BCD47DC3E6"); }
		}

		public string Name
		{
			get { return "Clear Generator"; }
		}

		public Type Type
		{
			get { return typeof(IClearGenerator); }
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

	public class ClearGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("815C682E-0690-48E7-8F7F-75BCD47DC3E6"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			// TODO: Clear is very strange, must look at refactoring/removing this explicit generator

			generatedData = new List<string>();

			return generatedData;
		}
	}

	public class ClearGenerator : IClearGenerator
	{
		public string GenerateClear(bool emptyInstead)
		{
			if (emptyInstead)
				return String.Empty;
			else
				return null;
		}

		public List<string> GenerateClears(double count, bool emptyInstead)
		{
			List<string> clears = new List<string>();

			for (double i = 0; i < count; i++)
			{
				clears.Add(GenerateClear(emptyInstead));
			}

			return clears;
		}
	}
}
