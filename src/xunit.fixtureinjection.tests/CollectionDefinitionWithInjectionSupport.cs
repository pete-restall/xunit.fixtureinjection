using System;

namespace Xunit.FixtureInjection.Tests
{
	[CollectionDefinition(Name)]
	public class CollectionDefinitionWithInjectionSupport : ICollectionFixtureRequiringInjectionSupport<CollectionFixtureRequiringInjection>
	{
		public const string Name = "CollectionDefinitionWithInjectionSupport";

		CollectionFixtureRequiringInjection ICreateCollectionFixture<CollectionFixtureRequiringInjection>.CreateCollectionFixture()
		{
			return new CollectionFixtureRequiringInjection(Token);
		}

		public static Guid Token { get; } = Guid.NewGuid();
	}
}
