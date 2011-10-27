using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class EmailAddressGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("DC9329D0-1415-48D2-B21D-D246487EFD90"); }
		}

		public string Name
		{
			get { return "Email Address Generator"; }
		}

		public Type Type
		{
			get { return typeof(IEmailAddressGenerator); }
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

	public class EmailAddressGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("DC9329D0-1415-48D2-B21D-D246487EFD90"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((IEmailAddressGenerator)generator).GenerateEmailAddresses((double)data[0], existingColumnData);

			return generatedData;
		}
	}

	public class EmailAddressGenerator : IEmailAddressGenerator
	{
		private readonly IStringGenerator stringGenerator;
		private readonly ILastNameGenerator lastNameGenerator;
		private readonly ICompanyNameGenerator companyNameGenerator;

		public EmailAddressGenerator(IStringGenerator stringGenerator, ILastNameGenerator lastNameGenerator,
			ICompanyNameGenerator companyNameGenerator)
		{
			this.stringGenerator = stringGenerator;
			this.lastNameGenerator = lastNameGenerator;
			this.companyNameGenerator = companyNameGenerator;
		}

		public string GenerateEmailAddress()
		{
			StringBuilder emailAddress = new StringBuilder();

			emailAddress.Append(stringGenerator.GenerateRandomString(1, 2, false, false).ToUpper());
			emailAddress.Append(lastNameGenerator.GenerateLastName());
			emailAddress.Append("@");
			emailAddress.Append(companyNameGenerator.GenerateCompanyNameWithoutSuffix());

			if (DateTime.Now.Second % 3 == 0)
			{
				emailAddress.Append(".org");
			}
			else if (DateTime.Now.Second % 2 == 0)
			{
				emailAddress.Append(".net");
			}
			else
			{
				emailAddress.Append(".com");
			}

			return emailAddress.ToString();
		}

		public List<string> GenerateEmailAddresses(double count, HashSet<string> existingColumnData)
		{
			HashSet<string> emailAddresses = new HashSet<string>();

			while (emailAddresses.Count < count)
			{
				string emailAddress = GenerateEmailAddress();

				if (emailAddresses.Contains(emailAddress, new StringUtilities.IgnoreCaseStringComparer()) == false &&
						existingColumnData.Contains(emailAddress, new StringUtilities.IgnoreCaseStringComparer()) == false)
				{
					emailAddresses.Add(emailAddress);
				}
			}

			return emailAddresses.ToList();
		}
	}
}
