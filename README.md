# What is this ?
[xunit.fixtureinjection](https://github.com/pete-restall/xunit.fixtureinjection)
is a library to support Integration Testing with [xUnit](https://github.com/xunit/xunit).
It does this by providing a set of shims that facilitate Dependency Injection
for
[Collection, Class and Test Case Fixtures](https://xunit.github.io/docs/shared-context).

This framework is DI Container agnostic because it simply provides a set of Factory
Method hooks into the [xUnit](https://github.com/xunit/xunit) pipeline.  For example,
I use it with [Ninject](http://www.ninject.org/) on a
[Raspberry Pi](https://www.raspberrypi.org/) to do hardware Integration Tests.
The Collection Fixtures handle global shared resources that don't need to be reset
between tests, such as access to kernel logs, while [Ninject's](http://www.ninject.org/)
[Named Scopes](https://github.com/ninject/Ninject.Extensions.NamedScope) allow
the Class Fixtures to provide deterministic disposal of loaded kernel modules
between test classes in a completely transparent manner.  But it's just as easy
to use it with [Autofac](https://autofac.org/) or Poor Man's DI.

# Why use this ?
While [xUnit](https://github.com/xunit/xunit) can be used as a general purpose testing
framework, its primary goal is Unit Testing.  To leverage it for any meaningful Integration
Tests it is necessary to write some boilerplate (which this library does for you) or
compromise in some way architecturally.  Solutions exist, such as the
[Service Locator Anti-Pattern](http://blog.ploeh.dk/2010/02/03/ServiceLocatorisanAnti-Pattern/)
or the [xunit.ioc](https://github.com/daniel-chambers/xunit.ioc) package.  The former
is just hideous and the latter (at this time) only supports [xUnit v1](https://github.com/xunit/xunit).

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

Create your Collection Fixture (with empty constructor or taking an
[IMessageSink](https://github.com/xunit/abstractions.xunit/blob/master/src/xunit.abstractions/Messages/BaseInterfaces/IMessageSink.cs),
as per the usual [xUnit](https://github.com/xunit/xunit) rules) and mark it with
[ICreateClassFixtures](https://github.com/pete-restall/xunit.fixtureinjection/blob/master/src/xunit.fixtureinjection/ICreateClassFixtures.cs)
if you wish to use Class Fixtures, and / or mark it with
[ICreateTestCaseFixtures](https://github.com/pete-restall/xunit.fixtureinjection/blob/master/src/xunit.fixtureinjection/ICreateTestCaseFixtures.cs)
if you wish to use Test Case Fixtures.  Be sure to use an explicit interface
definitions to 'hide' the Service Locator like horridness:

```C#
	public class CollectionFixtureWithInjectionSupport : ICreateClassFixtures, ICreateTestCaseFixtures
	{
		TFixture ICreateClassFixtures.CreateClassFixture<TFixture>()
		{
			return ...;
		}

		TFixture ICreateTestCaseFixtures.CreateTestCaseFixture<TFixture>()
		{
			return ...;
		}
	}
```

Create your Class Fixture or Test Case Fixture with any dependencies it
requires:

```C#
	public class ClassFixtureRequiringInjection
	{
		public ClassFixtureRequiringInjection(Foo foo, Bar bar, ...)
		{
			...
		}

		...
	}

	public class TestCaseFixtureRequiringInjection
	{
		public TestCaseFixtureRequiringInjection(Foo foo, Bar bar, ...)
		{
			...
		}

		...
	}
```

Use the Class Fixture or Test Case Fixture in any tests that are part of the
Test Collection:

```C#
	[Collection("MyCollection")]
	public class ClassFixtureInCollectionWithInjectionSupportTest : IClassFixture<ClassFixtureRequiringInjection>
	{
		public ClassFixtureInCollectionWithInjectionSupportTest(
			ClassFixtureRequiringInjection classFixture,
			TestCaseFixtureRequiringInjection testCaseFixture)
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
[IMessageSink](https://github.com/xunit/abstractions.xunit/blob/master/src/xunit.abstractions/Messages/BaseInterfaces/IMessageSink.cs)
argument.

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

The Collection Fixture is then free to request dependencies:

```C#
	public class CollectionFixtureRequiringInjection
	{
		public CollectionFixtureRequiringInjection(Foo foo, Bar bar, ...)
		{
			...
		}
	}
```

The Collection Fixture can also implement
[ICreateClassFixtures](https://github.com/pete-restall/xunit.fixtureinjection/blob/master/src/xunit.fixtureinjection/ICreateClassFixtures.cs)
and
[ICreateTestCaseFixtures](https://github.com/pete-restall/xunit.fixtureinjection/blob/master/src/xunit.fixtureinjection/ICreateTestCaseFixtures.cs)
if desired.

Simples.

# Builds
[![Main CI](https://ci.appveyor.com/api/projects/status/8rxim6jvtk60xm5w)](https://ci.appveyor.com/project/pete-restall/xunit-fixtureinjection)
