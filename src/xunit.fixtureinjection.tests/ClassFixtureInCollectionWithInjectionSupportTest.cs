namespace Xunit.FixtureInjection.Tests
{
	[Collection(CollectionFixtureWithInjectionSupportDefinition.Name)]
	public class ClassFixtureInCollectionWithInjectionSupportTest : IClassFixture<ClassFixtureRequiringInjection>
	{
		private readonly CollectionFixtureWithInjectionSupport collectionFixture;
		private readonly ClassFixtureRequiringInjection classFixture;

		public ClassFixtureInCollectionWithInjectionSupportTest(
			CollectionFixtureWithInjectionSupport collectionFixture,
			ClassFixtureRequiringInjection classFixture)
		{
			this.collectionFixture = collectionFixture;
			this.classFixture = classFixture;
		}

		[Fact]
		public void ExpectInjectedClassFixtureWasCreatedByCollectionFixture()
		{
			Assert.Equal(this.collectionFixture.Token, this.classFixture.InjectedToken);
		}
	}
}
