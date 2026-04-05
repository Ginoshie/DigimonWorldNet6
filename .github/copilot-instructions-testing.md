# Copilot Instructions — Testing Guide

> This file supplements `.github/copilot-instructions.md` with comprehensive testing guidelines.

---

## Test Stack

| Package | Version | Purpose |
|---------|---------|---------|
| NUnit | 4.4.0 | Test framework |
| NUnit3TestAdapter | 6.1.0 | IDE/CLI test runner |
| Microsoft.NET.Test.Sdk | 18.0.1 | Test host |
| Shouldly | 4.3.0 | Fluent assertions |

---

## Test Project Structure

Test projects mirror the source project folder structure:

```
Evolution.Calculator.Tests/
├── Builder/
│   └── DigimonBuilder.cs                  # Shared test data builder
├── EvolutionCriteriaCalculation/
│   ├── FromFresh/
│   │   └── FromFreshEvolutionCalculatorTests.cs
│   ├── FromInTraining/
│   │   └── FromInTrainingEvolutionCalculatorTests.cs
│   ├── FromRookieOrChampion/
│   │   ├── FromRookieOrChampionEvolutionCalculatorTests.cs
│   │   └── FromRookieOrChampionEvolutionMapperTests.cs
│   └── FromUltimate/
│       └── FromUltimateEvolutionCalculatorTests.cs

Frontend.WPF.Tests/
├── Conversion/
│   └── (Converter tests)

Shared.Tests/
├── EvolutionStageMapperTests.cs
├── Constants/
│   └── (Constant tests)
```

---

## Test Class Template

```csharp
using System;
using NUnit.Framework;
using Shouldly;

namespace Evolution.Calculator.Tests.EvolutionCriteriaCalculation.FromRookieOrChampion;

[TestFixture]
public sealed class MyClassTests
{
    [Test]
    public void MethodName_ShouldExpectedBehavior_WhenCondition()
    {
        // Arrange
        var sut = new SetupBuilder().Build();

        // Act
        var result = sut.DoSomething();

        // Assert
        result.ShouldBe(expectedValue);
    }

    [Test]
    [TestCase(param1, param2, expectedResult)]
    [TestCase(param1b, param2b, expectedResultB)]
    public void MethodName_ShouldExpectedBehavior_WhenParameterized(Type param1, Type param2, Type expected)
    {
        // Arrange
        var sut = new SetupBuilder().Build();

        // Act
        var result = sut.DoSomething(param1, param2);

        // Assert
        result.ShouldBe(expected);
    }

    private sealed class SetupBuilder
    {
        // Clean state in constructor
        public SetupBuilder()
        {
            // Reset any static state (e.g., Session.HistoricEvolutions.Clear())
        }

        // Fluent configuration methods
        public SetupBuilder WithSomeDependency(SomeType value)
        {
            // Configure...
            return this;
        }

        // Build the system under test
        public MyClass Build()
        {
            return new MyClass();
        }
    }
}
```

---

## Test Naming Convention

```
MethodUnderTest_ShouldExpectedBehavior_WhenCondition
```

### Examples from the codebase:

```
DetermineEvolutionResult_ShouldReturnExpectedDigimon_WhenThereAreNoHistoricEvolutions
DetermineEvolutionResult_ShouldReturnGreymon_WhenBirdramonHasHigherPrioScoreThanGreymonAndGreymonIsNotAHistoricEvolutionAndBirdramonIsAHistoricEvolution
DetermineEvolutionResult_ShouldThrowException_WhenDigimonIsNotARookieOrChampion
DetermineEvolutionResult_ShouldReturnCentarumon_WhenCentarumonAndGarurumonAreEnabled
```

---

## Builder Pattern for Test Data

### DigimonBuilder

Fluent builder for creating `UserDigimon` test instances:

```csharp
public sealed class DigimonBuilder
{
    private DigimonName _digimonName;
    private int _hp, _mp, _off, _def, _speed, _brains;
    private int _careMistakes, _weight, _happiness, _discipline, _battles, _techniqueCount;

    public DigimonBuilder WithDigimonType(DigimonName digimonName) { _digimonName = digimonName; return this; }
    public DigimonBuilder WithHP(int hp) { _hp = hp; return this; }
    public DigimonBuilder WithMP(int mp) { _mp = mp; return this; }
    public DigimonBuilder WithOff(int off) { _off = off; return this; }
    public DigimonBuilder WithDef(int def) { _def = def; return this; }
    public DigimonBuilder WithSpeed(int speed) { _speed = speed; return this; }
    public DigimonBuilder WithBrains(int brains) { _brains = brains; return this; }
    public DigimonBuilder WithCareMistakes(int careMistakes) { _careMistakes = careMistakes; return this; }
    public DigimonBuilder WithWeight(int weight) { _weight = weight; return this; }
    public DigimonBuilder WithHappiness(int happiness) { _happiness = happiness; return this; }
    public DigimonBuilder WithDiscipline(int discipline) { _discipline = discipline; return this; }
    public DigimonBuilder WithBattles(int battles) { _battles = battles; return this; }
    public DigimonBuilder WithTechniqueCount(int techniqueCount) { _techniqueCount = techniqueCount; return this; }

    public UserDigimon Build() => new(_digimonName, _hp, _mp, _off, _def, _speed, _brains,
        _careMistakes, _weight, _happiness, _discipline, _battles, _techniqueCount);
}
```

### SetupBuilder (Inner Class Pattern)

Each test class has its own inner `SetupBuilder` for SUT construction:

```csharp
private sealed class SetupBuilder
{
    public SetupBuilder()
    {
        // ALWAYS clear static state
        Session.HistoricEvolutions.Clear();
    }

    public SetupBuilder WithHistoricEvolution(DigimonName digimonName)
    {
        Session.HistoricEvolutions.Add(digimonName);
        return this;
    }

    public FromRookieOrChampionEvolutionCalculator Build()
    {
        return new FromRookieOrChampionEvolutionCalculator();
    }
}
```

---

## Assertion Patterns with Shouldly

### Value assertions
```csharp
result.ShouldBe(EvolutionResult.Greymon);
result.ShouldBe(42);
result.ShouldBeTrue();
result.ShouldBeFalse();
result.ShouldBeNull();
result.ShouldNotBeNull();
```

### Collection assertions
```csharp
list.ShouldContain(item);
list.ShouldNotContain(item);
list.ShouldBeEmpty();
list.ShouldNotBeEmpty();
list.Count.ShouldBe(5);
```

### Exception assertions
```csharp
// Use Action delegate pattern (NOT Assert.Throws)
Action throwingAction = () => sut.DoSomething(invalidInput);
throwingAction.ShouldThrow<ArgumentException>();
throwingAction.ShouldThrow<KeyNotFoundException>();
throwingAction.ShouldThrow<Exception>(); // For any exception
```

### String assertions
```csharp
result.ShouldBe("expected string");
result.ShouldStartWith("prefix");
result.ShouldContain("substring");
```

---

## Parameterized Test Patterns

### Full evolution test with all stats

```csharp
[Test]
[TestCase(DigimonName.Agumon, 1000, 1000, 250, 200, 500, 150, 3, 20, 80, 80, 0, 10, EvolutionResult.Birdramon)]
[TestCase(DigimonName.Agumon, 1500, 1000, 100, 100, 100, 150, 0, 20, 80, 80, 0, 10, EvolutionResult.Centarumon)]
public void DetermineEvolutionResult_ShouldReturnExpectedDigimon_WhenThereAreNoHistoricEvolutions(
    DigimonName digimonName,
    int hp, int mp, int off, int def, int speed, int brains,
    int careMistakes, int weight, int happiness, int discipline, int battles, int techniqueCount,
    EvolutionResult evolutionResult)
{
    // Arrange
    var sut = new SetupBuilder().Build();
    UserDigimon userDigimon = new DigimonBuilder()
        .WithDigimonType(digimonName)
        .WithHP(hp).WithMP(mp)
        .WithOff(off).WithDef(def)
        .WithSpeed(speed).WithBrains(brains)
        .WithCareMistakes(careMistakes).WithWeight(weight)
        .WithHappiness(happiness).WithDiscipline(discipline)
        .WithBattles(battles).WithTechniqueCount(techniqueCount)
        .Build();

    // Act
    EvolutionResult result = sut.DetermineEvolutionResult(userDigimon);

    // Assert
    result.ShouldBe(evolutionResult);
}
```

### Parameter order convention

Always use this consistent parameter order for Digimon stats:
```
DigimonName, HP, MP, Off, Def, Speed, Brains, CareMistakes, Weight, Happiness, Discipline, Battles, TechniqueCount
```

---

## Testing Edge Cases to Cover

### Evolution Calculator Tests

1. **Happy path:** Each Digimon evolves correctly with meeting stats
2. **Boundary values:** Stats exactly at threshold (passing vs failing)
3. **No evolution possible:** Stats below all thresholds → `EvolutionResult.None` or `EvolutionResult.Numemon` (fallback)
4. **Historic evolution priority:** New evolution preferred over previously-seen evolution
5. **All evolutions are historic:** Falls back to normal score-based ranking
6. **Wrong stage input:** Fresh/InTraining/Ultimate Digimon passed to Rookie/Champion calculator → exception
7. **GameVariant filtering:** Same Digimon has different evolution paths in Original vs Vice
8. **Weight margin:** Weight at exactly ±5 boundary
9. **Care mistakes direction:** Max vs Min criteria
10. **3-of-4 rule:** Evolution enabled with exactly 3 criteria, disabled with only 2

### Memory Access Tests

- Test with `ProcessMemory.Empty` and `PsxRam.Empty` (Null Object behavior)
- Test `HistoricEvolutions` bit flag logic
- Test `EvolutionStageMapper` for all known Digimon

### Frontend Tests

- Value converter output for various inputs
- Validation rule acceptance/rejection ranges
- Extension method outputs

---

## Running Tests

```bash
# Run all tests
dotnet test DigimonWorldNet6/DigimonWorld.sln

# Run specific test project
dotnet test DigimonWorldNet6/Evolution.Calculator.Tests/Evolution.Calculator.Tests.csproj

# Run tests with filter
dotnet test --filter "ClassName~FromRookieOrChampion"

# Run with verbose output
dotnet test --verbosity normal
```

---

## Static State Management in Tests

**Critical:** Some classes use static state that persists across tests:
- `Session.HistoricEvolutions` — must be cleared in `SetupBuilder` constructor
- `UserConfigurationManager` — reads from `userconfig.json` (may need mock or reset)

Always reset static state at the start of test setup, not in teardown. Use `SetupBuilder()` constructor for this.

---

## Adding Tests for New Features

### For a new evolution criteria:

```csharp
[TestFixture]
public sealed class FromRookieOrChampionEvolutionCalculatorTests
{
    // Add new [TestCase] attributes to existing parameterized test methods
    [TestCase(DigimonName.NewDigimon, 5000, 5000, 500, 500, 500, 500, 0, 40, 95, 95, 0, 49, EvolutionResult.NewUltimate)]

    // OR create a focused test method for the new evolution:
    [Test]
    public void DetermineEvolutionResult_ShouldReturnNewUltimate_WhenNewDigimonMeetsAllCriteria()
    {
        // Arrange
        var sut = new SetupBuilder().Build();
        UserDigimon userDigimon = new DigimonBuilder()
            .WithDigimonType(DigimonName.NewDigimon)
            /* ... all stats ... */
            .Build();

        // Act
        EvolutionResult result = sut.DetermineEvolutionResult(userDigimon);

        // Assert
        result.ShouldBe(EvolutionResult.NewUltimate);
    }
}
```

### For a new service/utility:

```csharp
[TestFixture]
public sealed class MyNewServiceTests
{
    [Test]
    public void MyMethod_ShouldReturnExpected_WhenGivenValidInput()
    {
        // Arrange
        var sut = new MyNewService();

        // Act
        var result = sut.MyMethod(validInput);

        // Assert
        result.ShouldBe(expectedOutput);
    }

    [Test]
    public void MyMethod_ShouldThrow_WhenGivenInvalidInput()
    {
        // Arrange
        var sut = new MyNewService();

        // Act
        Action act = () => sut.MyMethod(invalidInput);

        // Assert
        act.ShouldThrow<ArgumentException>();
    }
}
```

