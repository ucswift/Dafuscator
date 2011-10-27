using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection;
using System.Text;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class AddressGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("C08EA04D-F7EC-4448-93D8-F18AF870A956"); }
		}

		public string Name
		{
			get { return "Address Generator"; }
		}

		public Type Type
		{
			get { return typeof(IAddressGenerator); }
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

	public class AddressGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("C08EA04D-F7EC-4448-93D8-F18AF870A956"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			// TODO: Below is broken for AddressLine2/AddressLine3 generation

			if (generator != null && data.Length >= 1)
				generatedData = ((IAddressGenerator)generator).GenerateAddressLine1s((double)data[0]);

			return generatedData;
		}
	}

	public class AddressGenerator : IAddressGenerator
	{
		private static List<string> addressLine1s;

		private readonly INumberGenerator numberGenerator;
		private readonly IFileDataProvider fileDataProvider;

		public AddressGenerator(INumberGenerator numberGenerator, IFileDataProvider fileDataProvider)
		{
			this.numberGenerator = numberGenerator;
			this.fileDataProvider = fileDataProvider;
		}

		private List<string> GetAddressLine1s()
		{
			if (addressLine1s == null || addressLine1s.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				addressLine1s = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.Streets.txt");
			}

			return addressLine1s;
		}

		public string GenerateAddressLine1()
		{
			StringBuilder line1 = new StringBuilder();
			List<string> lines = GetAddressLine1s();

			int randomNumber = numberGenerator.GenerateRandomNumber(0, lines.Count - 1);

			line1.Append(numberGenerator.GenerateRandomNumber(10, 9999));
			line1.Append(" ");
			line1.Append(lines[randomNumber]);

			return line1.ToString();
		}

		public List<string> GenerateAddressLine1s(double count)
		{
			List<string> addressLine1s = new List<string>();

			for (double i = 0; i < count; i++)
			{
				addressLine1s.Add(GenerateAddressLine1());
			}

			return addressLine1s;
		}

		public string GenerateAddressLine2()
		{
			throw new NotImplementedException();
		}

		public List<string> GenerateAddressLine2s(double count)
		{
			throw new NotImplementedException();
		}
	}
}
