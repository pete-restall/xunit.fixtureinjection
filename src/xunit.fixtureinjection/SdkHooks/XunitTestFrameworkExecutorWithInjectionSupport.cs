using System.Collections.Generic;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.FixtureInjection.SdkHooks
{
	public class XunitTestFrameworkExecutorWithInjectionSupport : XunitTestFrameworkExecutor
	{
		public XunitTestFrameworkExecutorWithInjectionSupport(
			AssemblyName assemblyName,
			ISourceInformationProvider sourceInformationProvider,
			IMessageSink diagnosticMessageSink)
			: base(assemblyName, sourceInformationProvider, diagnosticMessageSink)
		{
		}

		protected override async void RunTestCases(
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink executionMessageSink,
			ITestFrameworkExecutionOptions executionOptions)
		{
			using (var assemblyRunner =
				new XunitTestAssemblyRunnerWithInjectionSupport(
					this.TestAssembly,
					testCases,
					this.DiagnosticMessageSink,
					executionMessageSink,
					executionOptions))
			{
				await assemblyRunner.RunAsync();
			}
		}
	}
}
