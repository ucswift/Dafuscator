using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace StringGeneratorTests
	{
		public class with_the_string_generator : FixtureBase
		{
			protected Dafuscator.Generators.StringGenerator stringGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				stringGenerator = new Dafuscator.Generators.StringGenerator();
			}
		}

		[TestFixture]
		public class when_generating_a_string : with_the_string_generator
		{
			[Test]
			public void should_be_less_then_50_and_greater_then_10_length()
			{
				string randomString = stringGenerator.GenerateRandomString(10, 50, true, true);
				Console.WriteLine(randomString);

				Assert.IsTrue(randomString.Length >= 10 && randomString.Length <= 50);
			}

			[Test]
			public void should_only_contain_numbers_and_letters()
			{
				string randomString = stringGenerator.GenerateRandomString(10, 50, false, true);
				Console.WriteLine(randomString);

				foreach (char c in randomString)
				{
					Assert.IsTrue(Char.IsLetterOrDigit(c));
				}
			}

			[Test]
			public void should_only_contain_letters()
			{
				string randomString = stringGenerator.GenerateRandomString(10, 50, false, false);
				Console.WriteLine(randomString);

				foreach (char c in randomString)
				{
					Assert.IsTrue(Char.IsLetter(c));
				}
			}
		}
	}
}