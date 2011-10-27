using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class FirstNameGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("20238491-5651-47DA-99EF-40AE726B51A5"); }
		}

		public string Name
		{
			get { return "First Name Generator"; }
		}

		public Type Type
		{
			get { return typeof(IFirstNameGenerator); }
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

	public class FirstNameGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("20238491-5651-47DA-99EF-40AE726B51A5"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((IFirstNameGenerator)generator).GenerateFirstNames((double)data[0]);

			return generatedData;
		}
	}

	public class FirstNameGenerator : IFirstNameGenerator
	{
		private static List<string> firstNames;

		private readonly INumberGenerator numberGenerator;
		private readonly IFileDataProvider fileDataProvider;

		public FirstNameGenerator(INumberGenerator numberGenerator, IFileDataProvider fileDataProvider)
		{
			this.numberGenerator = numberGenerator;
			this.fileDataProvider = fileDataProvider;
		}

		private List<string> GetFirstNames()
		{
			if (firstNames == null || firstNames.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				firstNames = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.FirstNames.txt");
			}

			return firstNames;
		}

		public string GenerateFirstName()
		{
			List<string> lines = GetFirstNames();

			int randomNumber = numberGenerator.GenerateRandomNumber(0, lines.Count - 1);

			return StringUtilities.NormalizeName(lines[randomNumber]);
		}

		public List<string> GenerateFirstNames(double count)
		{
			List<string> cities = new List<string>();

			for (double i = 0; i < count; i++)
			{
				cities.Add(GenerateFirstName());
			}

			return cities;
		}
	}
}
