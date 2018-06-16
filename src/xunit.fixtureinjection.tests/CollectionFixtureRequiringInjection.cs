using System;

namespace Xunit.FixtureInjection.Tests
{
	public class CollectionFixtureRequiringInjection : ICreateClassFixtures
	{
		public CollectionFixtureRequiringInjection(Guid token)
		{
			this.InjectedToken = token;
		}

		public Guid InjectedToken { get; }

		TFixture ICreateClassFixtures.CreateClassFixture<TFixture>()
		{
			if (typeof(TFixture) != typeof(ClassFixtureRequiringInjection))
				throw new ArgumentException($"Stub Collection Fixture is not intended to create class fixtures of type {typeof(TFixture)}");

			return (TFixture) (object) new ClassFixtureRequiringInjection(this.InjectedToken);
		}
	}
}
