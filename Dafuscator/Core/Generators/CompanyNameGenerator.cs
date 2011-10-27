using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class CompanyNameGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("8B0696C8-2501-4CAF-89CD-2A927810B72A"); }
		}

		public string Name
		{
			get { return "Company Name Generator"; }
		}

		public Type Type
		{
			get { return typeof(ICompanyNameGenerator); }
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

	public class CompanyNameGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("8B0696C8-2501-4CAF-89CD-2A927810B72A"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((ICompanyNameGenerator)generator).GenerateCompanyNames((double)data[0], existingColumnData);

			return generatedData;
		}
	}

	public class CompanyNameGenerator : ICompanyNameGenerator
	{
		private static List<string> companyStarts;
		private static List<string> companyMiddles;
		private static List<string> companyEnds;

		private readonly INumberGenerator numberGenerator;
		private readonly IFileDataProvider fileDataProvider;

		public CompanyNameGenerator(INumberGenerator numberGenerator, IFileDataProvider fileDataProvider)
		{
			this.numberGenerator = numberGenerator;
			this.fileDataProvider = fileDataProvider;
		}

		private List<string> GetCompanyStarts()
		{
			if (companyStarts == null || companyStarts.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				companyStarts = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.CompanyNameBeginnings.txt");
			}

			return companyStarts;
		}

		private List<string> GetCompanyMiddles()
		{
			if (companyMiddles == null || companyMiddles.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				companyMiddles = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.CompanyNameMiddles.txt");
			}

			return companyMiddles;
		}

		private List<string> GetCompanyEnds()
		{
			if (companyEnds == null || companyEnds.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				companyEnds = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.CompanyNameEndings.txt");
			}

			return companyEnds;
		}

		public string GenerateCompanyName()
		{
			StringBuilder companyName = new StringBuilder();

			int randomNumber = numberGenerator.GenerateRandomNumber(0, GetCompanyStarts().Count - 1);

			companyName.Append(GetCompanyStarts()[randomNumber]);

			randomNumber = numberGenerator.GenerateRandomNumber(0, GetCompanyMiddles().Count - 1);
			companyName.Append(GetCompanyMiddles()[randomNumber]);

			companyName.Append(" ");

			randomNumber = numberGenerator.GenerateRandomNumber(0, GetCompanyEnds().Count - 1);
			companyName.Append(GetCompanyEnds()[randomNumber]);

			return companyName.ToString();
		}

		public string GenerateCompanyNameWithoutSuffix()
		{
			StringBuilder companyName = new StringBuilder();

			int randomNumber = numberGenerator.GenerateRandomNumber(0, GetCompanyStarts().Count - 1);

			companyName.Append(GetCompanyStarts()[randomNumber]);

			randomNumber = numberGenerator.GenerateRandomNumber(0, GetCompanyMiddles().Count - 1);
			companyName.Append(GetCompanyMiddles()[randomNumber]);

			return companyName.ToString();
		}

		public List<string> GenerateCompanyNames(double count, HashSet<string> existingColumnData)
		{
			HashSet<string> companyNames = new HashSet<string>();

			while (companyNames.Count < count)
			{
				string companyName = GenerateCompanyName();

				if (companyNames.Contains(companyName, new StringUtilities.IgnoreCaseStringComparer()) == false &&
										existingColumnData.Contains(companyName, new StringUtilities.IgnoreCaseStringComparer()) == false)
				{
					companyNames.Add(companyName);
				}
			}

			return companyNames.ToList();
		}
	}
}