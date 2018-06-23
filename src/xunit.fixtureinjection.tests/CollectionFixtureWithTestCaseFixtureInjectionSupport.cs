using System;

namespace Xunit.FixtureInjection.Tests
{
	public class CollectionFixtureWithTestCaseFixtureInjectionSupport : ICreateTestCaseFixtures
	{
		public CollectionFixtureWithTestCaseFixtureInjectionSupport()
		{
			this.Token = Guid.NewGuid();
		}

		public Guid Token { get; }

		TFixture ICreateTestCaseFixtures.CreateTestCaseFixture<TFixture>()
		{
			if (typeof(TFixture) != typeof(TestCaseFixtureRequiringInjection))
				throw new ArgumentException($"Stub Collection Fixture is not intended to create test-case fixtures of type {typeof(TFixture)}");

			return (TFixture) (object) new TestCaseFixtureRequiringInjection(this.Token);
		}
	}
}
