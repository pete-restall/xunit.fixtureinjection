using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.FixtureInjection.SdkHooks
{
	public class XunitTestClassRunnerWithInjectionSupport : XunitTestClassRunner
	{
		private readonly IDictionary<Type, object> collectionFixtureMappings;

		public XunitTestClassRunnerWithInjectionSupport(
			ITestClass testClass,
			IReflectionTypeInfo @class,
			IEnumerable<IXunitTestCase> testCases,
			IMessageSink diagnosticMessageSink,
			IMessageBus messageBus,
			ITestCaseOrderer testCaseOrderer,
			ExceptionAggregator aggregator,
			CancellationTokenSource cancellationTokenSource,
			IDictionary<Type, object> collectionFixtureMappings)
			: base(testClass, @class, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource, collectionFixtureMappings)
		{
			this.collectionFixtureMappings = collectionFixtureMappings;
		}

		protected override void CreateClassFixture(Type fixtureType)
		{
			var fixtureFactory = this.collectionFixtureMappings.Select(x => x.Value).OfType<ICreateClassFixtures>().SingleOrDefault();
			if (fixtureFactory != null)
			{
				var factoryMethod = GetClassFixtureFactoryMethodFor(fixtureType);
				this.Aggregator.Run(() => this.ClassFixtureMappings[fixtureType] = factoryMethod.Invoke(fixtureFactory, new object[0]));
			}
			else
				base.CreateClassFixture(fixtureType);
		}

		private static MethodInfo GetClassFixtureFactoryMethodFor(Type fixtureType)
		{
			return InfoOf
				.Method<ICreateClassFixtures>(x => x.CreateClassFixture<object>())
				.GetGenericMethodDefinition()
				.MakeGenericMethod(fixtureType);
		}

		protected override bool TryGetConstructorArgument(ConstructorInfo constructor, int index, ParameterInfo parameter, out object argumentValue)
		{
			if (base.TryGetConstructorArgument(constructor, index, parameter, out argumentValue))
				return true;

			var fixtureFactory = this.collectionFixtureMappings.Select(x => x.Value).OfType<ICreateTestCaseFixtures>().SingleOrDefault();
			if (fixtureFactory != null)
			{
				var factoryMethod = GetTestCaseFixtureFactoryMethodFor(parameter.ParameterType);
				argumentValue = factoryMethod.Invoke(fixtureFactory, new object[0]);
				return true;
			}

			return false;
		}

		private static MethodInfo GetTestCaseFixtureFactoryMethodFor(Type fixtureType)
		{
			return InfoOf
				.Method<ICreateTestCaseFixtures>(x => x.CreateTestCaseFixture<object>())
				.GetGenericMethodDefinition()
				.MakeGenericMethod(fixtureType);
		}
	}
}
