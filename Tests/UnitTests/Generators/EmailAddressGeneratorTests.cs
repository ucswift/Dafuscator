using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace EmailAddressGeneratorTests
	{
		public class with_the_email_address_generator : FixtureBase
		{
			protected Dafuscator.Generators.EmailAddressGenerator emailAddressGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				emailAddressGenerator = new Dafuscator.Generators.EmailAddressGenerator(
					new Dafuscator.Generators.StringGenerator(), 
					new Dafuscator.Generators.LastNameGenerator(new Dafuscator.Generators.NumberGenerator(), new Dafuscator.Providers.FileDataProvider.FileData()),
					new Dafuscator.Generators.CompanyNameGenerator(new Dafuscator.Generators.NumberGenerator(), new Dafuscator.Providers.FileDataProvider.FileData()));
			}
		}

		[TestFixture]
		public class when_generating_an_email_address : with_the_email_address_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string emailAddress = emailAddressGenerator.GenerateEmailAddress();
				Console.WriteLine(emailAddress);

				Assert.IsNotNullOrEmpty(emailAddress);
			}
		}
	}
}
