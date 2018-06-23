namespace Xunit.FixtureInjection
{
	public interface ICreateTestCaseFixtures
	{
		TFixture CreateTestCaseFixture<TFixture>() where TFixture : class;
	}
}
