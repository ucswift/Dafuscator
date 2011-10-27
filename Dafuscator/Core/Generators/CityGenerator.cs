using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class CityGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("9EE9C145-2600-4BDD-9197-0C4FE0DE731E"); }
		}

		public string Name
		{
			get { return "City Name Generator"; }
		}

		public Type Type
		{
			get { return typeof(ICityGenerator); }
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

	public class CityGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("9EE9C145-2600-4BDD-9197-0C4FE0DE731E"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((ICityGenerator)generator).GenerateCityNames((double)data[0]);

			return generatedData;
		}
	}

	public class CityGenerator : ICityGenerator
	{
		private static List<string> cityLines;

		private readonly INumberGenerator numberGenerator;
		private readonly IFileDataProvider fileDataProvider;

		public CityGenerator(INumberGenerator numberGenerator, IFileDataProvider fileDataProvider)
		{
			this.numberGenerator = numberGenerator;
			this.fileDataProvider = fileDataProvider;
		}

		private List<string> GetCityNames()
		{
			if (cityLines == null || cityLines.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				cityLines = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.City.txt");
			}

			return cityLines;
		}

		public string GenerateCityName()
		{
			List<string> lines = GetCityNames();

			int randomNumber = numberGenerator.GenerateRandomNumber(0, lines.Count - 1);

			return lines[randomNumber];
		}

		public List<string> GenerateCityNames(double count)
		{
			List<string> cities = new List<string>();

			for (double i = 0; i < count; i++)
			{
				cities.Add(GenerateCityName());
			}

			return cities;
		}
	}
}
