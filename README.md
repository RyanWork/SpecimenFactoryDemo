# SpecimenFactoryDemo
Implementation of a `ISpecimenBuilder` to allow for AutoFixture to automatically generate `Mock<T>` for each constructor argument on a system under test.

Functionally, this attempts to provide the same benefits that AutoMoqCustomization brings when adding a customization to an `IFixture` instance. The advantage of this is when AutoMoqCustomization cannot determine how to generate a Mock of a concrete type (Ex. referencing a `Mock<T>.Object` generally throws if AutoFixture cannot evaluate a proxy for all dependencies on that Mock object).

## Usage
```csharp
_fixture = new Fixture();  
_fixture.Customizations.Add(new AutoMockSpecimenFactory());
```

## AutoMoqCustomization vs AutoMockSpecimenFactory
Assume the following: 
```csharp
public class Bar
{
}

public class Foo
{
  public Foo(Bar bar)
  {
  }
}

public class Baz
{
  public Baz(Bar bar, Foo foo)
  {
  }
}
```
If `Baz` is the system under test, both `AutoMoqCustomization` and `AutoMockSpecimenFactory` will throw when attempting to instantiate these sets of objects through AutoFixture. This is because AutoFixture will not know how to generate a `Mock<Bar>` object to use when creating the dependency graph because we are trying to mock a concrete type.

`AutoMockSpecimenFactory` attempts to solve this issue. All that is required is that `Foo` contains a parameterless constructor and `AutoMockSpecimenFactory` will effectively "Freeze" a Mock instance without throwing. Adding a parameterless constructor does not fix this issue in an `AutoMoqCustomization` test context.

This would allow for the use of `Fixture.Create` on an object that contains concrete dependencies and also exclude manually creating these dependencies to potentially `new()` a system under test manually.
