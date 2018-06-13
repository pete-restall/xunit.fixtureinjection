namespace Xunit.FixtureInjection
{
	public interface ICreateClassFixtures
	{
		TFixture CreateClassFixture<TFixture>() where TFixture : class;
	}
}
