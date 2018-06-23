namespace Xunit.FixtureInjection.Tests
{
	[Collection(CollectionFixtureWithClassFixtureInjectionSupportDefinition.Name)]
	public class ClassFixtureInCollectionWithInjectionSupportTest : IClassFixture<ClassFixtureRequiringInjection>
	{
		private readonly CollectionFixtureWithClassFixtureInjectionSupport collectionFixture;
		private readonly ClassFixtureRequiringInjection classFixture;

		public ClassFixtureInCollectionWithInjectionSupportTest(
			CollectionFixtureWithClassFixtureInjectionSupport collectionFixture,
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
