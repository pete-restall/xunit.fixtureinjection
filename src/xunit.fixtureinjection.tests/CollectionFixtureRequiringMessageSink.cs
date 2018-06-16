using Xunit.Abstractions;

namespace Xunit.FixtureInjection.Tests
{
	public class CollectionFixtureRequiringMessageSink
	{
		public CollectionFixtureRequiringMessageSink(IMessageSink messageSink)
		{
			this.MessageSink = messageSink;
		}

		public IMessageSink MessageSink { get; }
	}
}
