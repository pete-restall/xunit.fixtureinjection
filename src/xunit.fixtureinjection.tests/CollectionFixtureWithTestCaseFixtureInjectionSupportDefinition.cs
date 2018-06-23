namespace Xunit.FixtureInjection.Tests
{
	[CollectionDefinition(Name)]
	public class CollectionFixtureWithTestCaseFixtureInjectionSupportDefinition : ICollectionFixture<CollectionFixtureWithTestCaseFixtureInjectionSupport>
	{
		public const string Name = "CollectionWithTestCaseInjectionSupport";
	}
}
