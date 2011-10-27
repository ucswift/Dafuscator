using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class LastNameGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("E0AAB5A4-2056-471D-89A6-D57CB60681EE"); }
		}

		public string Name
		{
			get { return "Last Name Generator"; }
		}

		public Type Type
		{
			get { return typeof(ILastNameGenerator); }
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

	public class LastNameGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("E0AAB5A4-2056-471D-89A6-D57CB60681EE"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((ILastNameGenerator)generator).GenerateLastNames((double)data[0]);

			return generatedData;
		}
	}

	public class LastNameGenerator : ILastNameGenerator
	{
		private static List<string> lastNames;

		private readonly INumberGenerator numberGenerator;
		private readonly IFileDataProvider fileDataProvider;

		public LastNameGenerator(INumberGenerator numberGenerator, IFileDataProvider fileDataProvider)
		{
			this.numberGenerator = numberGenerator;
			this.fileDataProvider = fileDataProvider;
		}

		private List<string> GetLastNames()
		{
			if (lastNames == null || lastNames.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				lastNames = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.LastNames.txt");
			}

			return lastNames;
		}

		public string GenerateLastName()
		{
			List<string> lines = GetLastNames();

			int randomNumber = numberGenerator.GenerateRandomNumber(0, lines.Count - 1);

			return StringUtilities.NormalizeName(lines[randomNumber]);
		}

		public List<string> GenerateLastNames(double count)
		{
			List<string> cities = new List<string>();

			for (double i = 0; i < count; i++)
			{
				cities.Add(GenerateLastName());
			}

			return cities;
		}
	}
}
