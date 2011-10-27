using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace CountryGeneratorTests
	{
		public class with_the_countryname_generator : FixtureBase
		{
			protected Dafuscator.Generators.CountryGenerator countryGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				countryGenerator = new Dafuscator.Generators.CountryGenerator(new Dafuscator.Generators.NumberGenerator(),
					new Dafuscator.Providers.FileDataProvider.FileData());
			}
		}

		[TestFixture]
		public class when_generting_a_country_name : with_the_countryname_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string name = countryGenerator.GenerateCountryName();
				Console.WriteLine(name);

				Assert.IsNotNullOrEmpty(name);
			}
		}
	}
}