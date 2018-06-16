using Xunit.Abstractions;

namespace Xunit.FixtureInjection.Tests
{
	[CollectionDefinition(Name)]
	public class CollectionDefinitionRequiringMessageSink : ICollectionFixtureRequiringInjectionSupport<CollectionFixtureRequiringMessageSink>
	{
		public const string Name = "CollectionRequiringMessageSink";

		private readonly IMessageSink messageSink;

		public CollectionDefinitionRequiringMessageSink(IMessageSink messageSink)
		{
			this.messageSink = messageSink;
		}

		CollectionFixtureRequiringMessageSink ICreateCollectionFixture<CollectionFixtureRequiringMessageSink>.CreateCollectionFixture()
		{
			return new CollectionFixtureRequiringMessageSink(this.messageSink);
		}
	}
}
