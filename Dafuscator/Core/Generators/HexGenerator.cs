using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class HexGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("0FB971CF-130E-4CB1-85F0-9952A6D3563E"); }
		}

		public string Name
		{
			get { return "Hex Number Generator"; }
		}

		public Type Type
		{
			get { return typeof(IHexGenerator); }
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
									OleDbType.Binary
				       	};
			}
		}
	}

	public class HexGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("0FB971CF-130E-4CB1-85F0-9952A6D3563E"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((IHexGenerator)generator).GenerateHexNumbers((double)data[0]);

			return generatedData;
		}
	}

	public class HexGenerator : IHexGenerator
	{
		public string GenerateHexNumber()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("0x25504488");

			string hex1 = Guid.NewGuid().ToString();
			hex1 = hex1.Replace("-", "");

			sb.Append(hex1);

			string hex2 = Guid.NewGuid().ToString();
			hex2 = hex2.Replace("-", "");

			sb.Append(hex2);

			return sb.ToString();
		}

		public List<string> GenerateHexNumbers(double count)
		{
			List<string> numbers = new List<string>();

			for (double i = 0; i < count; i++)
			{
				numbers.Add(GenerateHexNumber());
			}

			return numbers;
		}
	}
}
