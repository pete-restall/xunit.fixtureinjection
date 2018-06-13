using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.FixtureInjection
{
	public class XunitTestAssemblyRunnerWithInjectionSupport : XunitTestAssemblyRunner
	{
		public XunitTestAssemblyRunnerWithInjectionSupport(
			ITestAssembly testAssembly,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageSink executionMessageSink,
			ITestFrameworkExecutionOptions executionOptions)
			: base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
		{
		}

		protected override Task<RunSummary> RunTestCollectionAsync(
			IMessageBus messageBus,
			ITestCollection testCollection,
			IEnumerable<IXunitTestCase> testCases,
			CancellationTokenSource cancellationTokenSource) =>
				new XunitTestCollectionRunnerWithInjectionSupport(
					testCollection,
					testCases,
					this.DiagnosticMessageSink,
					messageBus,
					this.TestCaseOrderer,
					new ExceptionAggregator(this.Aggregator),
					cancellationTokenSource)
						.RunAsync();
	}
}
