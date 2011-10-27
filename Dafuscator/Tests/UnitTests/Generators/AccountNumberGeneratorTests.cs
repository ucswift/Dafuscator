using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace AccountNumberGeneratorTests
	{
		public class with_the_accountnumber_generator : FixtureBase
		{
			protected Dafuscator.Generators.AccountNumberGenerator accountNumberGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				accountNumberGenerator = new Dafuscator.Generators.AccountNumberGenerator(
					new Dafuscator.Providers.TokenReplacementProvider.TokenReplacement(
						new Dafuscator.Generators.NumberGenerator(),
						new Dafuscator.Generators.CharacterGenerator()));
			}
		}

		[TestFixture]
		public class when_generting_a_account_number : with_the_accountnumber_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string accountNumber = accountNumberGenerator.GenerateAccountNumber();
				Console.WriteLine(accountNumber);

				Assert.IsNotNullOrEmpty(accountNumber);
			}
		}
	}
}