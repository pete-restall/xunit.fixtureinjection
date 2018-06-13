namespace Xunit.FixtureInjection.Tests
{
	[CollectionDefinition(Name)]
	public class CollectionFixtureWithInjectionSupportDefinition : ICollectionFixture<CollectionFixtureWithInjectionSupport>
	{
		public const string Name = "CollectionWithInjectionSupport";
	}
}
