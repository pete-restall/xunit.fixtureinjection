using System;

namespace Xunit.FixtureInjection.Tests
{
	public class CollectionFixtureWithClassFixtureInjectionSupport : ICreateClassFixtures
	{
		public CollectionFixtureWithClassFixtureInjectionSupport()
		{
			this.Token = Guid.NewGuid();
		}

		public Guid Token { get; }

		TFixture ICreateClassFixtures.CreateClassFixture<TFixture>()
		{
			if (typeof(TFixture) != typeof(ClassFixtureRequiringInjection))
				throw new ArgumentException($"Stub Collection Fixture is not intended to create class fixtures of type {typeof(TFixture)}");

			return (TFixture) (object) new ClassFixtureRequiringInjection(this.Token);
		}
	}
}
