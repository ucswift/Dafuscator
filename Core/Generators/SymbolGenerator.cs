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
	public class SymbolGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("7D084AD4-1A1F-444E-98B5-A6DFDD7B0C59"); }
		}

		public string Name
		{
			get { return "Symbol Generator"; }
		}

		public Type Type
		{
			get { return typeof(ISymbolGenerator); }
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

	public class SymbolGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("7D084AD4-1A1F-444E-98B5-A6DFDD7B0C59"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((ISymbolGenerator)generator).GenerateSymbols((double)data[0], existingColumnData);

			return generatedData;
		}
	}

	public class SymbolGenerator : ISymbolGenerator
	{
		private static List<string> symbols;

		private readonly INumberGenerator numberGenerator;
		private readonly IFileDataProvider fileDataProvider;

		public SymbolGenerator(INumberGenerator numberGenerator, IFileDataProvider fileDataProvider)
		{
			this.numberGenerator = numberGenerator;
			this.fileDataProvider = fileDataProvider;
		}

		private List<string> GetSymbols()
		{
			if (symbols == null || symbols.Count <= 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				symbols = fileDataProvider.GetDataFromEmbededFile(assembly, "WaveTech.Dafuscator.Generators.Data.Symbols.txt");
			}

			return symbols;
		}

		public string GenerateSymbol()
		{
			int randomNumber = numberGenerator.GenerateRandomNumber(0, GetSymbols().Count - 1);

			return GetSymbols()[randomNumber].ToUpper();
		}

		public List<string> GenerateSymbols(double count, HashSet<string> existingSecurityNames)
		{
			HashSet<string> symbols = new HashSet<string>();

			while (symbols.Count < count)
			{
				string symbol = GenerateSymbol();

				if (symbols.Contains(symbol, new StringUtilities.IgnoreCaseStringComparer()) == false &&
					existingSecurityNames.Contains(symbol, new StringUtilities.IgnoreCaseStringComparer()) == false)
				{
					symbols.Add(symbol);
				}
			}

			return symbols.ToList();
		}
	}
}