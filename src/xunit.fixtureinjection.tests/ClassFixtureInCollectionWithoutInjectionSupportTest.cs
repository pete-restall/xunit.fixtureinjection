namespace Xunit.FixtureInjection.Tests
{
	[Collection(CollectionFixtureWithoutInjectionSupportDefinition.Name)]
	public class ClassFixtureInCollectionWithoutInjectionSupportTest : IClassFixture<ClassFixtureNotRequiringInjection>
	{
		private readonly CollectionFixtureWithoutInjectionSupport collectionFixture;
		private readonly ClassFixtureNotRequiringInjection classFixture;

		public ClassFixtureInCollectionWithoutInjectionSupportTest(
			CollectionFixtureWithoutInjectionSupport collectionFixture,
			ClassFixtureNotRequiringInjection classFixture)
		{
			this.collectionFixture = collectionFixture;
			this.classFixture = classFixture;
		}

		[Fact]
		public void ExpectInjectedClassFixtureWasCreatedByStandardXunitActivator()
		{
			Assert.Same(this.collectionFixture, this.classFixture.CollectionFixture);
		}
	}
}
