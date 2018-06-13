# What is this ?
[xunit.fixtureinjection](https://github.com/pete-restall/xunit.fixtureinjection)
is a library to support integration testing with [xUnit](https://github.com/xunit/xunit)
It does this by providing a set of shims that facilitate Dependency Injection
of [Collection and Class Fixtures](https://xunit.github.io/docs/shared-context).

This framework is DI Container agnostic because it simply provides a set of Factory
Method hooks.  For example, I use it with [Ninject](http://www.ninject.org/) on a
[Raspberry Pi](https://www.raspberrypi.org/) to do hardware Integration Tests.
The Collection Fixtures handle global shared resources that don't need to be reset
between tests, such as access to kernel logs, while [Ninject's](http://www.ninject.org/)
[Named Scopes](https://github.com/ninject/Ninject.Extensions.NamedScope) allow
the Class Fixtures to provide deterministic disposal of loaded kernel modules
between test classes in a completely transparent manner.

# Why use this ?
While [xUnit](https://github.com/xunit/xunit) can be used as a general purpose testing
framework, its primary goal is Unit Testing.  To write Integration Tests in any
meaningful way it is necessary to write a lot of boilerplate.  Solutions exist,
such as the [Service Locator Anti-Pattern](http://blog.ploeh.dk/2010/02/03/ServiceLocatorisanAnti-Pattern/)
or the [xunit.ioc](https://github.com/daniel-chambers/xunit.ioc) library.  The former
is just hideous and the latter (at this time) only supports
[xUnit 1](https://github.com/xunit/xunit).

# In a Nutshell
Mark your assembly as requiring injection:

```C#
using Xunit.FixtureInjection;

[assembly: RequiresXunitFixtureInjection]
```

## Collection Fixtures as [Composition Roots](http://blog.ploeh.dk/2011/07/28/CompositionRoot/)
Create your Collection Definition:

```C#
	[CollectionDefinition("MyCollection")]
	public class CollectionFixtureWithInjectionSupportDefinition : ICollectionFixture<CollectionFixtureWithInjectionSupport>
	{
	}
```

Create your Collection Fixture (with empty constructor or taking an IMessageSink, as per the
usual xUnit rules) and mark it with ICreateClassFixtures.  Be sure to use an explicit interface
definition to 'hide' this Service Locator like horridness:

```C#
	public class CollectionFixtureWithInjectionSupport : ICreateClassFixtures
	{
		TFixture ICreateClassFixtures.CreateClassFixture<TFixture>()
		{
			return ...;
		}
	}
```

Create your Class Fixture with any dependencies it requires:

```C#
	public class ClassFixtureRequiringInjection
	{
		public ClassFixtureRequiringInjection(Foo foo, Bar bar, ...)
		{
			...
		}

		...
	}
```

Use the Class Fixture in any tests that are part of the Test Collection:

```C#
	[Collection("MyCollection")]
	public class ClassFixtureInCollectionWithInjectionSupportTest : IClassFixture<ClassFixtureRequiringInjection>
	{
		public ClassFixtureInCollectionWithInjectionSupportTest(ClassFixtureRequiringInjection classFixture)
		{
			...
		}

		[Fact]
		public void ExpectXunitFixtureInjectionIsAwesome()
		{
			...
		}
	}
```

## Collection Definitions as [Composition Roots](http://blog.ploeh.dk/2011/07/28/CompositionRoot/)
This is the same as above, but the difference is in the way that the Collection Definition is
written.  Note that the Collection Definition constructor must be empty or take a single
IMessageSink argument.

```C#
	[CollectionDefinition("MyOtherCollection")]
	public class CollectionFixtureDefinition : ICollectionFixtureRequiringInjection<CollectionFixtureRequiringInjection>
	{
		public CollectionFixtureRequiringInjection CreateCollectionFixture()
		{
			return ...;
		}
	}
```

The Collection Fixture is then free to request dependencies

```C#
	public class CollectionFixtureRequiringInjection
	{
		public CollectionFixtureRequiringInjection(Foo foo, Bar bar, ...)
		{
			...
		}
	}
```

The Collection Fixture can also implement ICreateClassFixtures if desired.

Simples.

# Builds
[![Main CI](https://ci.appveyor.com/api/projects/status/ad199gnwd4lyc6wm)](https://ci.appveyor.com/project/pete-restall/xunit.fixtureinjection)
