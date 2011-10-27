using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace StateGeneratorTests
	{
		public class with_the_statename_generator : FixtureBase
		{
			protected Dafuscator.Generators.StateGenerator stateGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				stateGenerator = new Dafuscator.Generators.StateGenerator(new Dafuscator.Generators.NumberGenerator(),
					new Dafuscator.Providers.FileDataProvider.FileData());
			}
		}

		[TestFixture]
		public class when_generting_a_city_name : with_the_statename_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string name = stateGenerator.GenerateStateName();
				Console.WriteLine(name);

				Assert.IsNotNullOrEmpty(name);
			}
		}
	}
}