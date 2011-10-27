using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace HexGeneratorTests
	{
		public class with_the_hex_generator : FixtureBase
		{
			protected Dafuscator.Generators.HexGenerator hexGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				hexGenerator = new Dafuscator.Generators.HexGenerator();
			}
		}

		[TestFixture]
		public class when_generating_a_hex_number : with_the_hex_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string hexNumber = hexGenerator.GenerateHexNumber();
				Console.WriteLine(hexNumber);

				Assert.IsNotNullOrEmpty(hexNumber);
			}
		}
	}
}