using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace CharacterGeneratorTests
	{
		public class with_the_character_generator : FixtureBase
		{
			protected Dafuscator.Generators.CharacterGenerator characterGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				characterGenerator = new Dafuscator.Generators.CharacterGenerator();
			}
		}

		[TestFixture]
		public class when_generating_a_character : with_the_character_generator
		{
			[Test]
			public void should_generate_a_number_or_letter()
			{
				char randomChar = characterGenerator.GenerateRandomCharacter(true);

				Assert.IsTrue(Char.IsLetterOrDigit(randomChar));
			}
		}

		[TestFixture]
		public class when_not_wanting_a_number : with_the_character_generator
		{
			[Test]
			public void should_not_generate_a_number()
			{
				char randomChar = characterGenerator.GenerateRandomCharacter(false);

				Assert.IsFalse(Char.IsDigit(randomChar));
			}
		}

		[TestFixture]
		public class when_generating_many_characters : with_the_character_generator
		{
			[Test]
			public void should_generate_10()
			{
				char randomChar = characterGenerator.GenerateRandomCharacter(true);

				Assert.IsTrue(Char.IsLetterOrDigit(randomChar));
			}
		}
	}
}