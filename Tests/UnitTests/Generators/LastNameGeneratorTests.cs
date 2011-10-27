using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace LastNameGeneratorTests
	{
		public class with_the_lastname_generator : FixtureBase
		{
			protected Dafuscator.Generators.LastNameGenerator lastNameGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				lastNameGenerator = new Dafuscator.Generators.LastNameGenerator(new Dafuscator.Generators.NumberGenerator(),
					new Dafuscator.Providers.FileDataProvider.FileData());
			}
		}

		[TestFixture]
		public class when_generting_a_country_name : with_the_lastname_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string name = lastNameGenerator.GenerateLastName();
				Console.WriteLine(name);

				Assert.IsNotNullOrEmpty(name);
			}
		}
	}
}