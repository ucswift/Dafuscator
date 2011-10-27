using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace SsnGeneratorTests
	{
		public class with_the_ssn_generator : FixtureBase
		{
			protected Dafuscator.Generators.SsnGenerator ssnGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

        ssnGenerator = new Dafuscator.Generators.SsnGenerator(new Dafuscator.Generators.NumberGenerator());
			}
		}

		[TestFixture]
		public class when_generating_an_ssn : with_the_ssn_generator
		{
			[Test]
			public void should_have_a_length_of_11()
			{
				string randomSsn = ssnGenerator.GenerateSocialSecurityNumber();
				Console.WriteLine(randomSsn);

				Assert.IsTrue(randomSsn.Length == 11);
			}

			[Test]
			public void should_be_a_valid_ssn()
			{
				string randomSsn = ssnGenerator.GenerateSocialSecurityNumber();
				Console.WriteLine(randomSsn);

				Assert.IsTrue(Regex.IsMatch( randomSsn, @"\d{3}-\d{2}-\d{4}" ));
			}
		}

		[TestFixture]
		public class when_generating_multiple_ssns : with_the_ssn_generator
		{
			[Test]
			public void should_have_a_total_of_50()
			{
				System.Collections.Generic.List<string> randomSsn = ssnGenerator.GenerateSocialSecurityNumbers(50);

				Assert.AreEqual(randomSsn.Count, 50);
			}

			[Test]
			public void should_all_be_valid_ssns()
			{
				System.Collections.Generic.List<string> randomSsns = ssnGenerator.GenerateSocialSecurityNumbers(50);

				foreach (string s in randomSsns)
				{
					Console.WriteLine(s);
					Assert.IsTrue(Regex.IsMatch(s, @"\d{3}-\d{2}-\d{4}"));
				}
			}
		}
	}
}