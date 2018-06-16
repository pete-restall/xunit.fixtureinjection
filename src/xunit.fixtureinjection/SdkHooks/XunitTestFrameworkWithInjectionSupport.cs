using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.FixtureInjection.SdkHooks
{
	public class XunitTestFrameworkWithInjectionSupport : XunitTestFramework
	{
		public XunitTestFrameworkWithInjectionSupport(IMessageSink messageSink)
			: base(messageSink)
		{
		}

		protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
		{
			return new XunitTestFrameworkExecutorWithInjectionSupport(
				assemblyName,
				this.SourceInformationProvider,
				this.DiagnosticMessageSink);
		}
	}
}
