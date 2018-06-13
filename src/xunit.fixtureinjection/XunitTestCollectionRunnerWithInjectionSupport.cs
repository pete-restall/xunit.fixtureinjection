using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.FixtureInjection
{
	public class XunitTestCollectionRunnerWithInjectionSupport : XunitTestCollectionRunner
	{
		private readonly IMessageSink diagnosticMessageSink;

		public XunitTestCollectionRunnerWithInjectionSupport(
			ITestCollection testCollection,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageBus messageBus,
			ITestCaseOrderer testCaseOrderer,
			ExceptionAggregator aggregator,
			CancellationTokenSource cancellationTokenSource)
			: base(testCollection, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
		{
			this.diagnosticMessageSink = diagnosticMessageSink;
		}

		protected override Task<RunSummary> RunTestClassAsync(
			ITestClass testClass,
			IReflectionTypeInfo @class,
			IEnumerable<IXunitTestCase> testCases) =>
				new XunitTestClassRunnerWithInjectionSupport(
					testClass,
					@class,
					testCases,
					this.diagnosticMessageSink,
					this.MessageBus,
					this.TestCaseOrderer,
					new ExceptionAggregator(this.Aggregator),
					this.CancellationTokenSource,
					this.CollectionFixtureMappings)
						.RunAsync();
	}
}
