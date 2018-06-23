namespace Xunit.FixtureInjection.Tests
{
	[CollectionDefinition(Name)]
	public class CollectionFixtureWithClassFixtureInjectionSupportDefinition : ICollectionFixture<CollectionFixtureWithClassFixtureInjectionSupport>
	{
		public const string Name = "CollectionWithClassFixtureInjectionSupport";
	}
}
