namespace Xunit.FixtureInjection
{
	public interface ICollectionFixtureRequiringInjectionSupport<TFixture> : ICollectionFixture<TFixture>, ICreateCollectionFixture<TFixture>
		where TFixture : class
	{
	}
}
