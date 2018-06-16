namespace Xunit.FixtureInjection.Tests
{
	[Collection(CollectionDefinitionWithInjectionSupport.Name)]
	public class CollectionDefinitionWithInjectionSupportTest : IClassFixture<ClassFixtureRequiringInjection>
	{
		private readonly CollectionFixtureRequiringInjection collectionFixture;
		private readonly ClassFixtureRequiringInjection classFixture;

		public CollectionDefinitionWithInjectionSupportTest(
			CollectionFixtureRequiringInjection collectionFixture,
			ClassFixtureRequiringInjection classFixture)
		{
			this.collectionFixture = collectionFixture;
			this.classFixture = classFixture;
		}

		[Fact]
		public void ExpectInjectedCollectionFixtureWasCreatedByCollectionDefinition()
		{
			Assert.Equal(CollectionDefinitionWithInjectionSupport.Token, this.collectionFixture.InjectedToken);
		}

		[Fact]
		public void ExpectInjectedClassFixtureWasCreatedByCollectionFixture()
		{
			Assert.Equal(this.collectionFixture.InjectedToken, this.classFixture.InjectedToken);
		}
	}
}
