using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Reflection;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class StateGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("042D4D72-2266-42F3-8E8C-CDA9BD6D465B"); }
		}

		public string Name
		{
			get { return "State Name Generator"; }
		}

		public Type Type
		{
			get { return typeof(IStateGenerator); }
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

	public class StateGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("042D4D72-2266-42F3-8E8C-CDA9BD6D465B"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((IStateGenerator)generator).GenerateStateNames((double)data[0]);

			return generatedData;
		}
	}

	public class StateGenerator : IStateGenerator
	{
		private static List<string> stateNames;

		private readonly INumberGenerator numberGenerator;
		private readonly IFileDataProvider fileDataProvider;

		public StateGenerator(INumberGenerator numberGenerator, IFileDataProvider fileDataProvider)
		{
			this.numberGenerator = numberGenerator;
			this.fileDataProvider = fileDataProvider;
		}

		private List<string> GetStateNames()
		{
			if (stateNames == null || stateNames.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				stateNames = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.State.txt");
			}

			return stateNames;
		}

		public string GenerateStateName()
		{
			List<string> lines = GetStateNames();

			int randomNumber = numberGenerator.GenerateRandomNumber(0, lines.Count - 1);

			return lines[randomNumber];
		}

		public List<string> GenerateStateNames(double count)
		{
			List<string> cities = new List<string>();

			for (double i = 0; i < count; i++)
			{
				cities.Add(GenerateStateName());
			}

			return cities;
		}
	}
}