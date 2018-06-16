using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.FixtureInjection.SdkHooks
{
	public class XunitTestCollectionRunnerWithInjectionSupport : XunitTestCollectionRunner
	{
		private readonly IMessageSink diagnosticMessageSink;
		private readonly ITestCollection testCollection;

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
			this.testCollection = testCollection;
		}

		protected override void CreateCollectionFixture(Type fixtureType)
		{
			var factoryInterface = this.AllInterfacesImplementedByCollectionDefinition.SingleOrDefault(x => x.IsCollectionFixtureFactoryFor(fixtureType));
			if (factoryInterface == null)
			{
				base.CreateCollectionFixture(fixtureType);
				return;
			}

			var createFactory = this.GenericCreateFactoryMethod.MakeGenericMethod(fixtureType);
			createFactory.Invoke(this, new object[] {this.testCollection.CollectionDefinition.ToRuntimeType(), fixtureType});
		}

		private IEnumerable<Type> AllInterfacesImplementedByCollectionDefinition =>
			this.testCollection.CollectionDefinition.Interfaces.Select(x => x.ToRuntimeType());

		private MethodInfo GenericCreateFactoryMethod =>
			typeof(XunitTestCollectionRunnerWithInjectionSupport)
				.GetMethod(
					nameof(this.UseFactoryToCreateCollectionFixture),
					BindingFlags.NonPublic | BindingFlags.Instance);

		private void UseFactoryToCreateCollectionFixture<TFixture>(Type definitionType, Type fixtureType) where TFixture : class
		{
			var factory = this.CreateCollectionFixtureFactory<TFixture>(definitionType);
			this.Aggregator.Run(() => this.CollectionFixtureMappings[fixtureType] = factory.CreateCollectionFixture());
		}

		private ICreateCollectionFixture<TFixture> CreateCollectionFixtureFactory<TFixture>(Type definitionType) where TFixture : class
		{
			var constructor = definitionType.GetConstructor(
				BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
				binder: null,
				types: new[] {typeof(IMessageSink)},
				modifiers: null);

			if (constructor != null)
				return (ICreateCollectionFixture<TFixture>) constructor.Invoke(new object[] {this.diagnosticMessageSink});

			return (ICreateCollectionFixture<TFixture>) Activator.CreateInstance(definitionType);
		}

		protected override Task<RunSummary> RunTestClassAsync(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases) =>
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
