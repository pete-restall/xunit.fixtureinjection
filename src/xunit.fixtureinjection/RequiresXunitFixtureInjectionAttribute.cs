using System;
using Xunit.Sdk;

namespace Xunit.FixtureInjection
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[TestFrameworkDiscoverer("Xunit.Sdk.TestFrameworkTypeDiscoverer", "xunit.execution.{Platform}")]
	public class RequiresXunitFixtureInjectionAttribute : Attribute, ITestFrameworkAttribute
	{
		public RequiresXunitFixtureInjectionAttribute(
			string typeName = "Xunit.FixtureInjection.SdkHooks.XunitTestFrameworkWithInjectionSupport",
			string assemblyName = "xunit.fixtureinjection")
		{
		}
	}
}
