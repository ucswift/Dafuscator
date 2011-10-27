using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace NumberGeneratorTests
	{
		public class with_the_number_generator : FixtureBase
		{
			protected Dafuscator.Generators.NumberGenerator numberGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				numberGenerator = new Dafuscator.Generators.NumberGenerator();
			}
		}

		[TestFixture]
		public class when_using_1_to_100 : with_the_number_generator
		{
			[Test]
			public void should_between_1_and_100()
			{
				int randomNumber = numberGenerator.GenerateRandomNumber(1, 100);

				Assert.IsTrue(randomNumber >= 1 && randomNumber <= 100);
			}
		}

		[TestFixture]
		public class when_using_10_to_20 : with_the_number_generator
		{
			[Test]
			public void should_between_10_and_20()
			{
				int randomNumber = numberGenerator.GenerateRandomNumber(10, 20);

				Assert.IsTrue(randomNumber >= 10 && randomNumber <= 20);
			}
		}

		[TestFixture]
		public class when_generating_many_numbers : with_the_number_generator
		{
			[Test]
			public void should_generate_10()
			{
				System.Collections.Generic.List<int> numbers = numberGenerator.GenerateRandomNumbers(10, 1, 100);

				Assert.AreEqual(numbers.Count, 10);
			}

			[Test] 
			public void all_10_should_between_1_and_100()
			{
				System.Collections.Generic.List<int> numbers = numberGenerator.GenerateRandomNumbers(10, 1, 100);

				foreach (int i in numbers)
				{
					Assert.IsTrue(i >= 1 && i <= 100);
				}
			}
		}
	}
}