using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Security.Cryptography;
using WaveTech.Dafuscator.Generators.Data;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class CharacterGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("A7AC88F5-8C61-4F3E-8066-23A4CCF19ED5"); }
		}

		public string Name
		{
			get { return "Random Character Generator"; }
		}

		public Type Type
		{
			get { return typeof(ICharacterGenerator); }
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
									OleDbType.VarWChar,
									OleDbType.Char
				       	};
			}
		}
	}

	public class CharacterGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("A7AC88F5-8C61-4F3E-8066-23A4CCF19ED5"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
			{
				bool charBool = false;

				try
				{
					charBool = (bool)data[1];
				}
				catch { }

				var characters = ((ICharacterGenerator)generator).GenerateRandomCharacters((double)data[0], charBool);

				generatedData = new List<string>();
				foreach (var c in characters)
				{
					generatedData.Add(c.ToString());
				}
			}

			return generatedData;
		}
	}

	public class CharacterGenerator : ICharacterGenerator
	{
		public char GenerateRandomCharacter(bool includeDigits)
		{
			byte[] randomBytes = new byte[1];
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			rng.GetBytes(randomBytes);
			int rand = Convert.ToInt32(randomBytes[0]);

			if (includeDigits)
				return CharacterNumberMap.Map[rand % 36];
			else
				return CharacterOnlyMap.Map[rand % 26];
		}

		public List<char> GenerateRandomCharacters(double count, bool includeDigits)
		{
			List<char> chars = new List<char>();

			for (double i = 0; i < count; i++)
			{
				chars.Add(GenerateRandomCharacter(includeDigits));
			}

			return chars;
		}
	}
}
