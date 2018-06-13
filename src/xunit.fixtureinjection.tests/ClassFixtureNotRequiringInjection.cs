namespace Xunit.FixtureInjection.Tests
{
	public class ClassFixtureNotRequiringInjection
	{
		public ClassFixtureNotRequiringInjection(CollectionFixtureWithoutInjectionSupport collectionFixture)
		{
			this.CollectionFixture = collectionFixture;
		}

		public CollectionFixtureWithoutInjectionSupport CollectionFixture { get; }
	}
}
