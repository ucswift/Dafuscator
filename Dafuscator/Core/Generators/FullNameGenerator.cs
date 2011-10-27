using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class FullNameGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("8440D22A-7ACD-4359-A5D7-3347F933DA54"); }
		}

		public string Name
		{
			get { return "Full Name Generator"; }
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

	public class FullNameGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("8440D22A-7ACD-4359-A5D7-3347F933DA54"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
			{
				bool fullNameBool1 = false;

				try
				{
					fullNameBool1 = (bool)data[1];
				}
				catch { }

				bool fullNameBool2 = false;

				try
				{
					fullNameBool2 = (bool)data[2];
				}
				catch { }

				generatedData = ((IFullNameGenerator)generator).GenerateFullNames((double)data[0], fullNameBool1, fullNameBool2);
			}

			return generatedData;
		}
	}

	public class FullNameGenerator : IFullNameGenerator
	{
		private readonly IFirstNameGenerator _firstNameGenerator;
		private readonly ILastNameGenerator _lastNameGenerator;
		private readonly ICharacterGenerator _characterGenerator;

		public FullNameGenerator(IFirstNameGenerator firstNameGenerator, ILastNameGenerator lastNameGenerator, ICharacterGenerator characterGenerator)
		{
			_firstNameGenerator = firstNameGenerator;
			_lastNameGenerator = lastNameGenerator;
			_characterGenerator = characterGenerator;
		}

		public string GenerateFullName(bool includeMiddle, bool middleFullName)
		{
			StringBuilder fullName = new StringBuilder();

			fullName.Append(_firstNameGenerator.GenerateFirstName());
			fullName.Append(" ");

			if (includeMiddle)
			{
				if (middleFullName)
					fullName.Append(_firstNameGenerator.GenerateFirstName());
				else
					fullName.Append(_characterGenerator.GenerateRandomCharacter(false));

				fullName.Append(" ");
			}

			fullName.Append(_lastNameGenerator.GenerateLastName());

			return fullName.ToString();
		}

		public List<string> GenerateFullNames(double count, bool includeMiddle, bool middleFullName)
		{
			List<string> fullNames = new List<string>();

			for (double i = 0; i < count; i++)
			{
				fullNames.Add(GenerateFullName(includeMiddle, middleFullName));
			}

			return fullNames;
		}
	}
}