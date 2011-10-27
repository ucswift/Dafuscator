using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using WaveTech.Dafuscator.Model.Interfaces.Generators;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Generators
{
	public class AccountNumberGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("771AE335-4700-43C5-938A-C9E1890D837E"); }
		}

		public string Name
		{
			get { return "Account Number Generator"; }
		}

		public Type Type
		{
			get { return typeof(IAccountNumberGenerator); }
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

	public class AccountNumberGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("771AE335-4700-43C5-938A-C9E1890D837E"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((IAccountNumberGenerator)generator).GenerateAccountNumbers((double)data[0]);

			return generatedData;
		}
	}

	public class AccountNumberGenerator : IAccountNumberGenerator
	{
		private readonly ITokenReplacementProvider tokenReplacementProvider;
		private readonly string token = "AXX-#A#XX#A";

		public AccountNumberGenerator(ITokenReplacementProvider tokenReplacementProvider)
		{
			this.tokenReplacementProvider = tokenReplacementProvider;
		}

		public string GenerateAccountNumber()
		{
			return tokenReplacementProvider.ProcessToken(token);
		}

		public List<string> GenerateAccountNumbers(double count)
		{
			HashSet<string> accountNumbers = new HashSet<string>();

			while (accountNumbers.Count < count)
			{
				string accountNumber = GenerateAccountNumber();

				if (accountNumbers.Contains(accountNumber) == false)
				{
					accountNumbers.Add(accountNumber);
				}
			}

			return accountNumbers.ToList();
		}
	}
}
