namespace Xunit.FixtureInjection.Tests
{
	[CollectionDefinition(Name)]
	public class CollectionFixtureWithoutInjectionSupportDefinition : ICollectionFixture<CollectionFixtureWithoutInjectionSupport>
	{
		public const string Name = "CollectionWithoutInjectionSupport";
	}
}
