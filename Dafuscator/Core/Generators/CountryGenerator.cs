using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class CountryGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("D98D89C8-ECAE-4AB3-897A-8478B0A6EC89"); }
		}

		public string Name
		{
			get { return "Country Name Generator"; }
		}

		public Type Type
		{
			get { return typeof(ICountryGenerator); }
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

	public class CountryGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("D98D89C8-ECAE-4AB3-897A-8478B0A6EC89"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((ICountryGenerator)generator).GenerateCountryNames((double)data[0]);

			return generatedData;
		}
	}

	public class CountryGenerator : ICountryGenerator
	{
		private static List<string> countryNames;

		private readonly INumberGenerator numberGenerator;
		private readonly IFileDataProvider fileDataProvider;

		public CountryGenerator(INumberGenerator numberGenerator, IFileDataProvider fileDataProvider)
		{
			this.numberGenerator = numberGenerator;
			this.fileDataProvider = fileDataProvider;
		}

		private List<string> GetCountryNames()
		{
			if (countryNames == null || countryNames.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				countryNames = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.Country.txt");
			}

			return countryNames;
		}

		public string GenerateCountryName()
		{
			List<string> lines = GetCountryNames();

			int randomNumber = numberGenerator.GenerateRandomNumber(0, lines.Count - 1);

			return lines[randomNumber];
		}

		public List<string> GenerateCountryNames(double count)
		{
			List<string> cities = new List<string>();

			for (double i = 0; i < count; i++)
			{
				cities.Add(GenerateCountryName());
			}

			return cities;
		}
	}
}