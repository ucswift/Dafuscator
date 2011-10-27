using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace FirstNameGeneratorTests
	{
		public class with_the_firstname_generator : FixtureBase
		{
			protected Dafuscator.Generators.FirstNameGenerator firstNameGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				firstNameGenerator = new Dafuscator.Generators.FirstNameGenerator(new Dafuscator.Generators.NumberGenerator(),
					new Dafuscator.Providers.FileDataProvider.FileData());
			}
		}

		[TestFixture]
		public class when_generting_a_first_name : with_the_firstname_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string name = firstNameGenerator.GenerateFirstName();
				Console.WriteLine(name);

				Assert.IsNotNullOrEmpty(name);
			}
		}
	}
}