using System;

namespace Xunit.FixtureInjection.Tests
{
	public class TestCaseFixtureRequiringInjection
	{
		public TestCaseFixtureRequiringInjection(Guid token)
		{
			this.InjectedToken = token;
		}

		public Guid InjectedToken { get; }
	}
}
