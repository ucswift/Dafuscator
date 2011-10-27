using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class SecurityNameGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("DF7794E2-C7DE-4C18-8FB5-3D457EE7D721"); }
		}

		public string Name
		{
			get { return "Security Name Generator"; }
		}

		public Type Type
		{
			get { return typeof(ISecurityNameGenerator); }
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

	public class SecurityNameGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("DF7794E2-C7DE-4C18-8FB5-3D457EE7D721"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((ISecurityNameGenerator)generator).GenerateSecurityNames((double)data[0], existingColumnData);

			return generatedData;
		}
	}

	public class SecurityNameGenerator : ISecurityNameGenerator
	{
		private static List<string> securityNames;

		private readonly INumberGenerator numberGenerator;
		private readonly IFileDataProvider fileDataProvider;

		public SecurityNameGenerator(INumberGenerator numberGenerator, IFileDataProvider fileDataProvider)
		{
			this.numberGenerator = numberGenerator;
			this.fileDataProvider = fileDataProvider;
		}

		private List<string> GetSecurityNames()
		{
			if (securityNames == null || securityNames.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				securityNames = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.SecurityNames.txt");
			}

			return securityNames;
		}

		public string GenerateSecurityName()
		{
			int randomNumber = numberGenerator.GenerateRandomNumber(0, GetSecurityNames().Count - 1);

			return GetSecurityNames()[randomNumber].Trim();
		}

		public List<string> GenerateSecurityNames(double count, HashSet<string> existingSecurityNames)
		{
			HashSet<string> names = new HashSet<string>();

			while (names.Count < count)
			{
				string securityName = GenerateSecurityName();

				if (names.Contains(securityName, new StringUtilities.IgnoreCaseStringComparer()) == false &&
					existingSecurityNames.Contains(securityName, new StringUtilities.IgnoreCaseStringComparer()) == false)
				{
					names.Add(securityName);
				}
			}

			return names.ToList();
		}
	}
}