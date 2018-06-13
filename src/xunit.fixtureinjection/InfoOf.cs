using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Xunit.FixtureInjection
{
	internal static class InfoOf
	{
		public static MethodInfo Method<T>(Expression<Action<T>> methodExpression)
		{
			return ((MethodCallExpression) methodExpression.Body).Method;
		}
	}
}
