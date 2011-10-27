using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace CityGeneratorTests
	{
		public class with_the_cityname_generator : FixtureBase
		{
			protected Dafuscator.Generators.CityGenerator cityGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				cityGenerator = new Dafuscator.Generators.CityGenerator(new Dafuscator.Generators.NumberGenerator(),
					new Dafuscator.Providers.FileDataProvider.FileData());
			}
		}

		[TestFixture]
		public class when_generting_a_city_name : with_the_cityname_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string name = cityGenerator.GenerateCityName();
				Console.WriteLine(name);

				Assert.IsNotNullOrEmpty(name);
			}
		}
	}
}