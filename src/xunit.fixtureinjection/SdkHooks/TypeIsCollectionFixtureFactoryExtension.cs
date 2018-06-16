using System;
using System.Linq;

namespace Xunit.FixtureInjection.SdkHooks
{
	internal static class TypeIsCollectionFixtureFactoryExtension
	{
		public static bool IsCollectionFixtureFactoryFor(this Type intf, Type fixtureType)
		{
			return
				intf.IsGenericType &&
				intf.GetGenericTypeDefinition() == typeof(ICreateCollectionFixture<>) &&
				intf.GetGenericArguments().Single() == fixtureType;
		}
	}
}
