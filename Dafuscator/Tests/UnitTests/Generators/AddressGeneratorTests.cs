using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace AddressGeneratorTests
	{
		public class with_the_address_generator : FixtureBase
		{
			protected Dafuscator.Generators.AddressGenerator addressGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				addressGenerator = new Dafuscator.Generators.AddressGenerator(new Dafuscator.Generators.NumberGenerator(),
					new Dafuscator.Providers.FileDataProvider.FileData());
			}
		}

		[TestFixture]
		public class when_generting_an_address_line_one : with_the_address_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string line1 = addressGenerator.GenerateAddressLine1();
				Console.WriteLine(line1);

				Assert.IsNotNullOrEmpty(line1);
			}
		}
	}
}