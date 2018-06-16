namespace Xunit.FixtureInjection
{
	public interface ICreateCollectionFixture<out TFixture> where TFixture : class
	{
		TFixture CreateCollectionFixture();
	}
}
