using System;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace LoginNameGeneratorTests
	{
		public class with_the_loginname_generator : FixtureBase
		{
			protected Dafuscator.Generators.LoginNameGenerator loginNameGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				loginNameGenerator = new Dafuscator.Generators.LoginNameGenerator(
						new Dafuscator.Generators.CharacterGenerator(),
						new Dafuscator.Generators.NumberGenerator(),
						new Dafuscator.Generators.LastNameGenerator(
							new Dafuscator.Generators.NumberGenerator(),
							new Dafuscator.Providers.FileDataProvider.FileData()));
			}
		}

		[TestFixture]
		public class when_generting_a_login_name : with_the_loginname_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string login = loginNameGenerator.GenerateLoginName();
				Console.WriteLine(login);

				Assert.IsNotNullOrEmpty(login);
			}
		}
	}
}