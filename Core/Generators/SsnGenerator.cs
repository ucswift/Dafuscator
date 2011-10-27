using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class SsnGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("C6D94BE5-3AEE-4CB4-952D-8EC39F3E4702"); }
		}

		public string Name
		{
			get { return "Social Security Number Generator"; }
		}

		public Type Type
		{
			get { return typeof(ISsnGenerator); }
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
									OleDbType.VarWChar
				       	};
			}
		}
	}

	public class SsnGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("C6D94BE5-3AEE-4CB4-952D-8EC39F3E4702"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((ISsnGenerator)generator).GenerateSocialSecurityNumbers((double)data[0]);

			return generatedData;
		}
	}

	public class SsnGenerator : ISsnGenerator
	{
		private readonly INumberGenerator numberGenerator;

		public SsnGenerator(INumberGenerator numberGenerator)
		{
			this.numberGenerator = numberGenerator;
		}

		public string GenerateSocialSecurityNumber()
		{
			StringBuilder ssn = new StringBuilder();
			ssn.Append("9");
			ssn.Append(numberGenerator.GenerateRandomNumber(0, 9));
			ssn.Append(numberGenerator.GenerateRandomNumber(0, 9));
			ssn.Append("-");
			ssn.Append(numberGenerator.GenerateRandomNumber(0, 9));
			ssn.Append(numberGenerator.GenerateRandomNumber(0, 9));
			ssn.Append("-");
			ssn.Append(numberGenerator.GenerateRandomNumber(0, 9));
			ssn.Append(numberGenerator.GenerateRandomNumber(0, 9));
			ssn.Append(numberGenerator.GenerateRandomNumber(0, 9));
			ssn.Append(numberGenerator.GenerateRandomNumber(0, 9));

			return ssn.ToString();
		}

		public List<string> GenerateSocialSecurityNumbers(double count)
		{
			HashSet<string> ssns = new HashSet<string>();

			while (ssns.Count < count)
			{
				string ssn = GenerateSocialSecurityNumber();

				if (ssns.Contains(ssn) == false)
				{
					ssns.Add(ssn);
				}
			}

			return ssns.ToList();
		}
	}
}
