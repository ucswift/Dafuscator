using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class TokenReplacementGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("6653E317-2034-4B41-A1BB-84B1FE822728"); }
		}

		public string Name
		{
			get { return "Token Replacement Generator"; }
		}

		public Type Type
		{
			get { return typeof(ITokenReplacementGenerator); }
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

	public class TokenReplacementGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("6653E317-2034-4B41-A1BB-84B1FE822728"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((ITokenReplacementGenerator)generator).GenerateReplacedStrings((double)data[0], (string)data[1], existingColumnData);

			return generatedData;
		}
	}

	public class TokenReplacementGenerator : ITokenReplacementGenerator
	{
		private readonly ITokenReplacementProvider _tokenReplacementProvider;

		public TokenReplacementGenerator(ITokenReplacementProvider tokenReplacementProvider)
		{
			_tokenReplacementProvider = tokenReplacementProvider;
		}

		public string GenerateReplacedString(string token)
		{
			return _tokenReplacementProvider.ProcessToken(token);
		}

		public List<string> GenerateReplacedStrings(double count, string token, HashSet<string> existingColumnData)
		{
			HashSet<string> strings = new HashSet<string>();

			while (strings.Count < count)
			{
				string randomString = GenerateReplacedString(token);

				if (strings.Contains(randomString, new StringUtilities.IgnoreCaseStringComparer()) == false &&
							existingColumnData.Contains(randomString, new StringUtilities.IgnoreCaseStringComparer()) == false)
				{
					strings.Add(randomString);
				}
			}

			return strings.ToList();
		}
	}
}