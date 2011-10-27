using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace ZipCodeGeneratorTests
	{
		public class with_the_zipcode_generator : FixtureBase
		{
			protected Dafuscator.Generators.ZipCodeGenerator zipCodeGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				zipCodeGenerator = new Dafuscator.Generators.ZipCodeGenerator(
					new Dafuscator.Providers.TokenReplacementProvider.TokenReplacement(
						new Dafuscator.Generators.NumberGenerator(),
						new Dafuscator.Generators.CharacterGenerator()));
			}
		}

		[TestFixture]
		public class when_generting_a_normal_zip_code : with_the_zipcode_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string accountNumber = zipCodeGenerator.GenerateZipCode(false);
				Console.WriteLine(accountNumber);

				Assert.IsNotNullOrEmpty(accountNumber);
			}
		}

		[TestFixture]
		public class when_generting_a_with4_zip_code : with_the_zipcode_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string accountNumber = zipCodeGenerator.GenerateZipCode(true);
				Console.WriteLine(accountNumber);

				Assert.IsNotNullOrEmpty(accountNumber);
			}
		}
	}
}