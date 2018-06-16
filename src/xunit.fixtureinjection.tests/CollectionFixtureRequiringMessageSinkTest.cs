namespace Xunit.FixtureInjection.Tests
{
	[Collection(CollectionDefinitionRequiringMessageSink.Name)]
	public class CollectionFixtureRequiringMessageSinkTest
	{
		private readonly CollectionFixtureRequiringMessageSink collectionFixture;

		public CollectionFixtureRequiringMessageSinkTest(CollectionFixtureRequiringMessageSink collectionFixture)
		{
			this.collectionFixture = collectionFixture;
		}

		[Fact]
		public void ExpectInjectedCollectionFixtureHasMessageSink()
		{
			Assert.NotNull(this.collectionFixture.MessageSink);
		}
	}
}
