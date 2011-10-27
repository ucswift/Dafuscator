using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests.Generators
{
	namespace CompanyNameGeneratorTests
	{
		public class with_the_companyname_generator : FixtureBase
		{
			protected Dafuscator.Generators.CompanyNameGenerator companyNameGenerator;

			protected override void Before_each_test()
			{
				base.Before_each_test();

				companyNameGenerator = new Dafuscator.Generators.CompanyNameGenerator(new Dafuscator.Generators.NumberGenerator(),
					new Dafuscator.Providers.FileDataProvider.FileData());
			}
		}

		[TestFixture]
		public class when_generting_a_company_name : with_the_companyname_generator
		{
			[Test]
			public void should_not_be_null()
			{
				string name = companyNameGenerator.GenerateCompanyName();
				Console.WriteLine(name);

				Assert.IsNotNullOrEmpty(name);
			}
		}

		[TestFixture]
		public class when_generting_50_company_names : with_the_companyname_generator
		{
			[Test]
			public void should_not_be_null()
			{
				List<string> names = companyNameGenerator.GenerateCompanyNames(50, new HashSet<string>());

				foreach (string name in names)
				{
					Console.WriteLine(name);
          Assert.IsNotNullOrEmpty(name);
				}
			}

			[Test]
			public void should_be_50()
			{
				List<string> names = companyNameGenerator.GenerateCompanyNames(50, new HashSet<string>());

				Assert.AreEqual(50, names.Count);
			}
		}
	}
}