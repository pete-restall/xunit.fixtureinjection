using System;

namespace Xunit.FixtureInjection.Tests
{
	public class ClassFixtureRequiringInjection
	{
		public ClassFixtureRequiringInjection(Guid token)
		{
			this.InjectedToken = token;
		}

		public Guid InjectedToken { get; }
	}
}
