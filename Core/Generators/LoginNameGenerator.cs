using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class LoginNameGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("9234E6A2-FB29-4F66-B7EA-A219F900A022"); }
		}

		public string Name
		{
			get { return "Login Generator"; }
		}

		public Type Type
		{
			get { return typeof(ILoginNameGenerator); }
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

	public class LoginNameGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("9234E6A2-FB29-4F66-B7EA-A219F900A022"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((ILoginNameGenerator)generator).GenerateLoginNames((double)data[0]);

			return generatedData;
		}
	}

	public class LoginNameGenerator : ILoginNameGenerator
	{
		private readonly ICharacterGenerator characterGenerator;
		private readonly INumberGenerator numberGenerator;
		private readonly ILastNameGenerator lastNameGenerator;

		public LoginNameGenerator(ICharacterGenerator characterGenerator, INumberGenerator numberGenerator, ILastNameGenerator lastNameGenerator)
		{
			this.characterGenerator = characterGenerator;
			this.numberGenerator = numberGenerator;
			this.lastNameGenerator = lastNameGenerator;
		}

		public string GenerateLoginName()
		{
			StringBuilder loginName = new StringBuilder();
			loginName.Append(characterGenerator.GenerateRandomCharacter(false));
			loginName.Append(lastNameGenerator.GenerateLastName());
			loginName.Append(numberGenerator.GenerateQuickRandomNumber(0, 999));

			return loginName.ToString();
		}

		public List<string> GenerateLoginNames(double count)
		{
			HashSet<string> logins = new HashSet<string>();

			while (logins.Count < count)
			{
				string login = GenerateLoginName();

				if (logins.Contains(login) == false)
				{
					logins.Add(login);
				}
			}

			return logins.ToList();
		}
	}
}
