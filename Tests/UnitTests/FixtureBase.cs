using NUnit.Framework;

namespace WaveTech.Dafuscator.UnitTests
{
	public class FixtureBase
	{
		[SetUp]
		public void SetupContext()
		{
			Before_each_test();
		}

		[TearDown]
		public void TearDownContext()
		{
			After_each_test();
		}

		protected virtual void Before_each_test()
		{
		}

		protected virtual void After_each_test()
		{
		}
	}
}