using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Security.Cryptography;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class NumberGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("68290CBD-A327-41BE-A9E6-D6FFD089B953"); }
		}

		public string Name
		{
			get { return "Random Number Generator"; }
		}

		public Type Type
		{
			get { return typeof(INumberGenerator); }
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

	public class NumberGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("68290CBD-A327-41BE-A9E6-D6FFD089B953"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
			{
				var numbers = ((INumberGenerator)generator).GenerateRandomNumbers(double.Parse(data[0].ToString()), int.Parse(data[1].ToString()), int.Parse(data[2].ToString()));

				generatedData = new List<string>();
				foreach (var n in numbers)
				{
					generatedData.Add(n.ToString());
				}
			}

			return generatedData;
		}
	}

	public class NumberGenerator : INumberGenerator
	{
		private RNGCryptoServiceProvider rng;
		private byte[] buffer;

		public NumberGenerator()
		{
			rng = new RNGCryptoServiceProvider();
			buffer = new byte[4];
		}

		private int GetRandomSeed()
		{
			rng.GetBytes(buffer);
			return BitConverter.ToInt32(buffer, 0);
		}

		public int GenerateRandomNumber(int min, int max)
		{
			return new Random(GetRandomSeed()).Next(min, max);
		}

		public int GenerateQuickRandomNumber(int min, int max)
		{
			return new Random(DateTime.Now.Millisecond).Next(min, max);
		}

		public List<int> GenerateRandomNumbers(double count, int min, int max)
		{
			List<int> numbers = new List<int>();

			for (double i = 0; i < count; i++)
			{
				numbers.Add(GenerateRandomNumber(min, max));
			}

			return numbers;
		}
	}
}