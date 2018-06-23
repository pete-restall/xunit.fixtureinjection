namespace Xunit.FixtureInjection.Tests
{
	[Collection(CollectionFixtureWithTestCaseFixtureInjectionSupportDefinition.Name)]
	public class TestCaseFixtureInCollectionWithInjectionSupportTest
	{
		private readonly CollectionFixtureWithTestCaseFixtureInjectionSupport collectionFixture;
		private readonly TestCaseFixtureRequiringInjection testCaseFixture;

		public TestCaseFixtureInCollectionWithInjectionSupportTest(
			CollectionFixtureWithTestCaseFixtureInjectionSupport collectionFixture,
			TestCaseFixtureRequiringInjection testCaseFixture)
		{
			this.collectionFixture = collectionFixture;
			this.testCaseFixture = testCaseFixture;
		}

		[Fact]
		public void ExpectInjectedTestCaseFixtureDependencyWasCreatedByCollectionFixture()
		{
			Assert.Equal(this.collectionFixture.Token, this.testCaseFixture.InjectedToken);
		}
	}
}
