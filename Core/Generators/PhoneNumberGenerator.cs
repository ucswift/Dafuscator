using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class PhoneNumberGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("0086042D-C5E1-4013-9901-2FABDD679136"); }
		}

		public string Name
		{
			get { return "Phone Number Generator"; }
		}

		public Type Type
		{
			get { return typeof(IPhoneNumberGenerator); }
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

	public class PhoneNumberGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("0086042D-C5E1-4013-9901-2FABDD679136"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
			{
				bool phoneBool = false;

				try
				{
					phoneBool = (bool)data[1];
				}
				catch { }

				generatedData = ((IPhoneNumberGenerator)generator).GeneratePhoneNumbers((double)data[0], phoneBool);
			}

			return generatedData;
		}
	}

	public class PhoneNumberGenerator : IPhoneNumberGenerator
	{
		public readonly INumberGenerator numberGenerator;

		public PhoneNumberGenerator(INumberGenerator numberGenerator)
		{
			this.numberGenerator = numberGenerator;
		}

		public string GeneratePhoneNumber(bool includeAreaCode)
		{
			StringBuilder phoneNumber = new StringBuilder();

			if (includeAreaCode)
			{
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append("-");
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append("-");
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
			}
			else
			{
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append("-");
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
				phoneNumber.Append(numberGenerator.GenerateRandomNumber(0, 9));
			}

			return phoneNumber.ToString();
		}

		public List<string> GeneratePhoneNumbers(double count, bool includeAreaCode)
		{
			HashSet<string> phoneNumbers = new HashSet<string>();

			while (phoneNumbers.Count < count)
			{
				string ssn = GeneratePhoneNumber(includeAreaCode);

				if (phoneNumbers.Contains(ssn) == false)
				{
					phoneNumbers.Add(ssn);
				}
			}

			return phoneNumbers.ToList();
		}
	}
}