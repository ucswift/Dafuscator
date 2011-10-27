using System;
using System.Collections.Generic;
using System.Data.OleDb;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class ZipCodeGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("08707085-1263-497E-B008-1CCE0C02EA05"); }
		}

		public string Name
		{
			get { return "Zip Code Generator"; }
		}

		public Type Type
		{
			get { return typeof(IZipCodeGenerator); }
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

	public class ZipCodeGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("08707085-1263-497E-B008-1CCE0C02EA05"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
			{
				bool data1 = false;

				try
				{
					data1 = (bool)data[1];
				}
				catch { }

				generatedData = ((IZipCodeGenerator)generator).GenerateZipCodes((double)data[0], data1);
			}

			return generatedData;
		}
	}

	public class ZipCodeGenerator : IZipCodeGenerator
	{
		private readonly ITokenReplacementProvider tokenReplacementProvider;
		private readonly string token = "#####";
		private readonly string token4 = "#####-####";

		public ZipCodeGenerator(ITokenReplacementProvider tokenReplacementProvider)
		{
			this.tokenReplacementProvider = tokenReplacementProvider;
		}

		public string GenerateZipCode(bool append4DigitCode)
		{
			if (append4DigitCode)
				return tokenReplacementProvider.ProcessToken(token4);
			else
				return tokenReplacementProvider.ProcessToken(token);
		}

		public List<string> GenerateZipCodes(double count, bool append4DigitCode)
		{
			List<string> zips = new List<string>();

			for (double i = 0; i < count; i++)
			{
				zips.Add(GenerateZipCode(append4DigitCode));
			}

			return zips;
		}
	}
}
