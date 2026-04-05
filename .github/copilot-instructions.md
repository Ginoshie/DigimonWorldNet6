# Copilot Instructions — DigimonWorld .NET Solution

## Project Overview

This is a **Digimon World 1 (PS1)** companion desktop application built with **.NET 10 / WPF**. It calculates evolution outcomes based on the player's Digimon stats, reads live game data from a PS1 emulator (DuckStation) via memory access, and provides tools such as a Digi-Wiki, Tamer Vision overlay, and a built-in music player.

**Solution file:** `DigimonWorldNet6/DigimonWorld.sln`
**SDK:** .NET 10.0 (`global.json` — `"version": "10.0.0"`)
**Language:** C# (latest, including C# 14 preview features like `field` keyword)
**Platform:** Windows-only (WPF, Win32 P/Invoke for memory reading)

---

## Solution Architecture

### Projects & Dependency Graph

```
Frontend.WPF  (WPF executable — UI layer)
  └── Evolution.Calculator.Core  (class library — domain/business logic)
        ├── MemoryAccess  (class library — emulator memory reading via P/Invoke)
        │     └── Shared  (class library — enums, constants, extensions, events, config)
        └── Shared

Evolution.Calculator.Tests  (NUnit test project → Evolution.Calculator.Core)
Frontend.WPF.Tests           (NUnit test project → Frontend.WPF)
Shared.Tests                 (NUnit test project → Shared)
```

### Project Details

| Project | Target | Root Namespace | Key Packages |
|---------|--------|----------------|-------------|
| **Frontend.WPF** | `net10.0-windows` | `DigimonWorld.Frontend.WPF` | CSCore 1.2.1.2, ReactiveUI 22.3.1, System.Reactive 6.1.0, Microsoft.Xaml.Behaviors.Wpf 1.1.135, Microsoft.Extensions.Hosting 10.0.2 |
| **Evolution.Calculator.Core** | `net10.0-windows` | `DigimonWorld.Evolution.Calculator.Core` | Microsoft.Extensions.DependencyInjection 10.0.2 |
| **MemoryAccess** | `net10.0-windows` | `MemoryAccess` | (Shared project ref only) |
| **Shared** | `net10.0-windows` | `Shared` | System.Reactive 6.1.0 |
| **Evolution.Calculator.Tests** | `net10.0-windows` | `Evolution.Calculator.Tests` | NUnit 4.4.0, NUnit3TestAdapter 6.1.0, Shouldly 4.3.0 |

### Design Philosophy

This project intentionally keeps things **simple and direct**. Dependency Injection is deliberately not used beyond the minimal `IHost` bootstrapping required by WPF — there was no added benefit for this codebase. Instead, the solution favors:
- **Singletons** for core services (`EvolutionCalculator.Instance`, `LiveMemoryReader.Instance`)
- **Static classes** for cross-cutting concerns (`ServiceRelay`, `UserConfigurationManager`, EventHub classes, `MusicPlayer`)
- **Direct instantiation** of ViewModels and services (no constructor injection, no service locators)

**Do NOT introduce DI containers, service abstractions, or IoC patterns.** Keep instantiation simple and explicit.

### Key Application Entry Point

`Frontend.WPF/App.xaml.cs`:
- Uses `Microsoft.Extensions.Hosting` with `Host.CreateDefaultBuilder()` for minimal app lifecycle (the `IHost` is only used to resolve `MainWindow`)
- `EvolutionCalculatorModule.RegisterServices()` exists as a placeholder but is empty — services are accessed directly via singletons and static classes
- Starts `LiveMemoryReader.Instance.Start()` on startup
- Registers `CodePagesEncodingProvider` for text encoding support

---

## Coding Conventions

### MUST follow these conventions in all generated code:

1. **File-scoped namespaces** — always use `namespace X.Y.Z;` (no braces wrapping the file)
2. **`sealed` classes by default** — mark classes as `sealed` unless designed for inheritance
3. **Primary constructors** — use primary constructors on data-oriented classes (e.g., `public sealed class Foo(int bar) : Base`)
4. **`field` keyword (C# 14)** — use `field` for semi-auto properties instead of explicit backing fields:
   ```csharp
   public bool Connected
   {
       get;
       private set
       {
           if (field == value) return;
           field = value;
           OnPropertyChanged(nameof(Connected));
       }
   }
   ```
5. **Nullable reference types** — always enabled (`<Nullable>enable</Nullable>`); use `?` for nullable references
6. **Constants** — `UPPER_SNAKE_CASE` for `const` and `static readonly` constants: `private const int OFFENSE_OFFSET = 0x001557E0;`
7. **Private fields** — prefix with underscore: `_instance`, `_disposables`, `_cts`
8. **Expression-bodied members** — use where concise and readable:
   ```csharp
   public static EvolutionResult CalculateEvolutionResult(UserDigimon userDigimon) => _evolutionCalculator.CalculateEvolutionResult(userDigimon);
   ```
9. **Explicit `using` statements** — `Frontend.WPF` and `Evolution.Calculator.Core` do NOT use `ImplicitUsings`; always write explicit imports. `Shared` and `MemoryAccess` DO use `ImplicitUsings`.
10. **`LangVersion` is `default`** — leverages latest C# features available with the SDK

### Naming Conventions

| Element | Convention | Example |
|---------|-----------|---------|
| Classes | PascalCase, `sealed` by default | `public sealed class EvolutionCalculator` |
| Interfaces | `I` prefix + PascalCase | `IEvolutionCriteria`, `IEvolutionCalculator` |
| Enums | PascalCase, no suffix | `EvolutionStage`, `GameVariant` |
| Enum values | PascalCase | `EvolutionStage.InTraining` |
| Constants | UPPER_SNAKE_CASE | `WEIGHT_MARGIN`, `HP_OFFSET` |
| Private fields | `_camelCase` | `_evolutionCalculator` |
| Properties | PascalCase | `EvolutionScore`, `DigimonName` |
| Methods | PascalCase | `CalculateEvolutionResult`, `DetermineEvolutionResult` |
| Local variables | camelCase | `evolutionResult`, `carriedOverStatTotal` |
| Parameters | camelCase | `userDigimon`, `evolutionCriteria` |
| Extension methods | PascalCase, in `static` classes with `Extensions` suffix | `EvolutionResultExtensions.ToDigimonType()` |
| Test classes | `{ClassUnderTest}Tests` | `FromRookieOrChampionEvolutionCalculatorTests` |
| Test methods | `MethodUnderTest_ShouldExpected_WhenCondition` | `DetermineEvolutionResult_ShouldReturnExpectedDigimon_WhenThereAreNoHistoricEvolutions` |

---

## Architecture Patterns

### 1. Reactive Event-Driven Communication (EventHub Pattern)

Cross-cutting communication uses **static EventHub classes** with `System.Reactive` subjects. This is the primary way components communicate across project boundaries.

**EventHub classes (in `Shared/Services/Events/`):**
- `EmulatorLinkEventHub` — emulator connection state, sync signals for each stat type
- `DigimonStatsEventHub` — individual stat sync signals (HP, MP, Off, Def, etc.)
- `MusicPlayerEventHub` — music player lifecycle and control events
- `HistoricEvolutionEventhub` — per-stage historic evolution sync signals

**Pattern:**
```csharp
// Declaration (in EventHub class)
private static readonly BehaviorSubject<bool> _emulatorConnectedSubject = new(false);
public static IObservable<bool> EmulatorConnectedObservable => _emulatorConnectedSubject.AsObservable();
public static void SignalEmulatorConnected(bool isConnected) => _emulatorConnectedSubject.OnNext(isConnected);

// Use BehaviorSubject<T> when the latest value matters (replays to new subscribers)
// Use Subject<T> for fire-and-forget signals
// Use Subject<Unit> for parameterless notifications
```

**Subscribing pattern (consumers collect subscriptions in CompositeDisposable):**
```csharp
_compositeDisposable = new CompositeDisposable(
    EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(OnEmulatorConnected),
    DigimonStatsEventHub.SyncEmulatorHPObservable.Subscribe(_ => SyncHP())
);

// Dispose in Dispose() method
public void Dispose() => _compositeDisposable.Dispose();
```

### 2. Singleton Pattern

Used for core infrastructure services:
- `EvolutionCalculator.Instance` — thread-safe via `Lazy<T>`
- `LiveMemoryReader.Instance` — thread-safe via `Lazy<T>`

```csharp
private static readonly Lazy<EvolutionCalculator> _instance = new(() => new EvolutionCalculator());
public static EvolutionCalculator Instance { get; } = _instance.Value;
private EvolutionCalculator() { } // private constructor
```

### 3. Null Object Pattern

Used extensively in memory access layer — `Empty` static properties return safe defaults when not connected:
```csharp
public static ProcessMemory Empty { get; } = new EmptyProcessMemory();
public static PsxRam Empty { get; } = new EmptyPsxRam();
public static ParameterStats Empty { get; } = new();
```

### 4. Strategy Pattern (Evolution Calculation)

`EvolutionCalculator` dispatches to stage-specific calculators:
```csharp
IEvolutionCalculator evolutionCalculator = userDigimon.EvolutionStage switch
{
    EvolutionStage.Fresh => new FromFreshEvolutionCalculator(),
    EvolutionStage.InTraining => new FromInTrainingEvolutionCalculator(),
    EvolutionStage.Rookie or EvolutionStage.Champion => new FromRookieOrChampionEvolutionCalculator(),
    EvolutionStage.Ultimate => new FromUltimateEvolutionCalculator(),
    _ => throw new ArgumentOutOfRangeException(...)
};
```

### 5. Direct Instantiation (No DI)

The project intentionally avoids dependency injection. Services are accessed directly via singletons, static classes, and explicit `new` calls. Do NOT refactor toward DI — simplicity is the priority.

```csharp
// ViewModels are created directly — not resolved from a container
GeneralConfigWindowViewModel configViewModel = new(configWindow);

// Core services are accessed via singletons
EvolutionCalculator.Instance.CalculateEvolutionResult(userDigimon);
LiveMemoryReader.Instance.Start();

// Static façades provide simple access
ServiceRelay.CalculateEvolutionResult(userDigimon);
```

### 6. Static Façade (ServiceRelay)

`ServiceRelay` acts as a static façade exposing core singletons:
```csharp
public static class ServiceRelay
{
    public static EvolutionResult CalculateEvolutionResult(UserDigimon userDigimon) => _evolutionCalculator.CalculateEvolutionResult(userDigimon);
    public static LiveMemoryReader LiveMemoryReader { get; }
}
```

### 7. Builder Pattern (Test Data)

Test data creation uses fluent builders:
```csharp
UserDigimon userDigimon = new DigimonBuilder()
    .WithDigimonType(DigimonName.Agumon)
    .WithHP(1000).WithMP(1000)
    .WithOff(250).WithDef(200)
    .WithSpeed(500).WithBrains(150)
    .WithCareMistakes(3).WithWeight(20)
    .WithHappiness(80).WithDiscipline(80)
    .WithBattles(0).WithTechniqueCount(10)
    .Build();
```

Tests also use inner `SetupBuilder` classes for system-under-test setup:
```csharp
private sealed class SetupBuilder
{
    public SetupBuilder() { Session.HistoricEvolutions.Clear(); }
    public SetupBuilder WithHistoricEvolution(DigimonName digimonName) { ... return this; }
    public FromRookieOrChampionEvolutionCalculator Build() { ... }
}
```

---

## Domain Model

### Core Enums (`Shared/Enums/`)

- **`DigimonName`** — all Digimon names as enum values (Agumon, Greymon, MetalGreymon, etc.)
- **`EvolutionStage`** — `Fresh`, `InTraining`, `Rookie`, `Champion`, `Ultimate`
- **`EvolutionResult`** — mirrors DigimonName plus `Unknown`, `None`, `NotApplicable`
- **`GameVariant`** — `[Flags]` enum: `Original = 1 << 0`, `Vice = 1 << 1`, `MyotismonPatch = 1 << 2`, `PanjyamonPatch = 1 << 3`
- **`DigimonType`** — Digimon elemental types
- **`DigimonStatName`** — stat identifiers

### Core Data Objects

- **`UserDigimon`** — the player's Digimon with all stats (HP, MP, Off, Def, Speed, Brains, CareMistakes, Weight, Happiness, Discipline, Battles, TechniqueCount). `DigimonName` setter auto-calculates `EvolutionStage` via `EvolutionStageMapper`.
- **`Digimon`** (readonly record struct) — `(int ByteValue, DigimonName DigimonName, GameVariant IncludeGameVariantFlags, GameVariant ExcludeGameVariantFlags)`
- **`IEvolutionCriteria`** — interface for evolution requirements: `Stats`, `CareMistakes`, `Weight`, `BonusCriteria`
- **`MainCriteriaStats`** — stat thresholds (HP, MP, Off, Def, Speed, Brains)
- **`MainCriteriaCareMistakes`** — care mistake count & direction (max vs min)
- **`MainCriteriaWeight`** — target weight ± 5 margin
- **`BonusCriteria`** — happiness, discipline, battles, techniqueCount, precursorDigimon

### Evolution Calculation Flow

1. `UserDigimon` → `EvolutionCalculator.CalculateEvolutionResult()`
2. Dispatches to stage-specific calculator (e.g., `FromRookieOrChampionEvolutionCalculator`)
3. Gets evolution criteria list from mapper (e.g., `FromRookieOrChampionEvolutionMapper`)
4. Mapper selects criteria based on current `GameVariant` using `GameVariantExtensions.IsAvailableIn()`
5. For each potential evolution:
   - Check if 3-of-4 main criteria are met (Stats, CareMistakes, Weight, BonusCriteria)
   - Calculate evolution score (stat average)
   - Apply historic evolution prioritization (new evolutions > already-seen ones)
6. Returns the `EvolutionResult` with the highest score

### Game Variant System

The app supports multiple game versions via `GameVariant` flags:
- **Original** — vanilla Digimon World 1
- **Vice** — Vice v21+ mod (adds new Ultimate evolutions: Weregarurumon, Gigadramon, MetalEtemon, Machinedramon)
- **MyotismonPatch** — adds Myotismon evolution (Vice + patch)
- **PanjyamonPatch** — adds Panjyamon evolution (Vice + patch)

Each Digimon in `DigimonTypes` has `IncludeGameVariantFlags` and `ExcludeGameVariantFlags`. The mapper filters evolution paths based on the active variant.

### Configuration System

- `UserConfiguration` persisted to `userconfig.json` via `UserConfigurationManager`
- Config sections: `SpeakingSimulatorConfig`, `MusicPlayerConfig`, `EvolutionCalculatorConfig`, `EmulatorLinkConfig`
- Changes broadcast via `BehaviorSubject<T>` observables
- Subscribers react to config changes in real-time

---

## WPF Frontend Patterns

### ViewModel Hierarchy

```
BaseViewModel (INotifyPropertyChanged + SetField<T>)
  ├── BaseWindowViewModel (Window, MinimizeCommand, CloseCommand, DragCommand)
  │     ├── MainWindowViewModel
  │     ├── GeneralConfigWindowViewModel
  │     ├── MusicPlayerViewModel
  │     └── AboutAndCreditsWindowViewModel
  └── PaneBaseViewModel (AnimateOffset for sliding panes)
        ├── NavigationLeftPaneViewModelComponent
        ├── HistoricEvolutionsBottomPaneViewModelComponent
        └── EmulatorLinkRightPaneViewModelComponent
```

### Window/ViewModel Wiring

ViewModels are created directly with `new` — this is intentional, not a gap. There is no DI container involved:
```csharp
// In a command handler or constructor:
GeneralConfigWindow configWindow = new() { Owner = Application.Current.MainWindow };
GeneralConfigWindowViewModel configViewModel = new(configWindow);
configWindow.DataContext = configViewModel;
configWindow.ShowDialog();
```

### ICommand Implementations

- **`CommandHandler`** — parameterless `ICommand` using primary constructor: `public sealed class CommandHandler(Action action) : ICommand`
- **`RelayCommand<T>`** — generic typed `ICommand`: `public class RelayCommand<T>(Action<T> execute) : ICommand`

### Pane Animation System

Sliding panes use `PaneBaseViewModel.AnimateOffset()`:
```csharp
// Animates a double value from start to target over 600ms at 60fps with ease-out
protected IObservable<double> AnimateOffset(double start, double target)
```

### Value Converters (`Frontend.WPF/Conversion/`)

- `EnumToImageConverter` — maps enum values to image resources
- `TrueToVisibilityConverter` / `TrueToHiddenConverter` — bool to Visibility
- `DigimonTypeContainsMultiConverter` — multi-value converter for type checking
- `ToolTipTextConverter`

### Validation Rules (`Frontend.WPF/Validation/`)

Base validation rules with derived per-stat rules:
```
Bases/
  ZeroToNineNineNineNineStringValidationRule.cs  (0-9999 range)
  ZeroToNineNineNineValidationRule.cs            (0-999 range)
CombatStats/     (HP, MP, Off, Def, Speed, Brains validations)
NonCombatStats/  (Weight, Happiness, Discipline, etc. validations)
Configuration/   (config-specific validations)
```

### Resources & Assets

- **Font:** `Fonts/DW1_US_Regular.ttf` (custom Digimon World font, included as Resource)
- **Images:** `Images/Digimon/` (Digimon icons, included as Resource)
- **Music:** `Music/**/*.mp3` and `Music/**/*.wav` (copied to output as PreserveNewest)
- **ResourceDictionaries:** WPF style dictionaries for consistent theming

### Services (`Frontend.WPF/Services/`)

- **`MusicPlayer`** — static class using CSCore/WasapiOut for audio playback
- **`SpeakingSimulator`** — typewriter text animation effect with cancellation support
- **`DigimonIconFactory`** / **`TypeIconFactory`** / **`SpecialIconFactory`** — create icons from enum values

---

## Memory Access Layer

### Architecture

```
LiveMemoryReader (Singleton, monitors emulator process)
  └── Attach(Process) → creates:
        ProcessMemory (wraps Win32 ReadProcessMemory P/Invoke)
        PsxRam (finds PSX RAM base via byte-signature scanning)
        ParameterStats, ConditionStats, ProfileStats, CareStats, TechniqueStats, HistoricEvolutions
```

### Memory Reading Pattern

1. `ProcessMemory` wraps `kernel32.dll` P/Invoke (`ReadProcessMemory`, `OpenProcess`)
2. `PsxRam` scans emulator process memory for a known byte signature to locate PSX RAM base
3. All game addresses are offsets from this base: `ram.A(OFFSET)` → `IntPtr`
4. `MemoryValueSyncBase` auto-polls every 1 second via `Observable.Interval` when connected
5. Each stats class reads specific memory offsets and signals completion via EventHub

### Adding New Memory Values

To read a new value from the game's memory:
1. Find the memory offset in the PSX RAM
2. Create or extend a `MemoryValueSyncBase` subclass
3. Add the offset as `private const int MY_VALUE_OFFSET = 0x00XXXXXX;`
4. Override `OnUpdateData()` to read via `mem.ReadInt16(ram.A(MY_VALUE_OFFSET))`
5. Signal completion via the appropriate EventHub signal
6. Add the `Empty` static property with a private parameterless constructor
7. Wire it up in `LiveMemoryReader.Attach()`

---

## Testing Guidelines

### Framework & Assertions

- **Test framework:** NUnit 4.4.0 with NUnit3TestAdapter
- **Assertions:** Shouldly 4.3.0 — use `result.ShouldBe(expected)` not `Assert.AreEqual()`
- **Test runner:** Microsoft.NET.Test.Sdk 18.0.1

### Test Structure

```csharp
[TestFixture]
public sealed class FromRookieOrChampionEvolutionCalculatorTests
{
    [Test]
    [TestCase(DigimonName.Agumon, 1000, 1000, 250, 200, 500, 150, 3, 20, 80, 80, 0, 10, EvolutionResult.Birdramon)]
    public void DetermineEvolutionResult_ShouldReturnExpectedDigimon_WhenThereAreNoHistoricEvolutions(
        DigimonName digimonName, int hp, int mp, int off, int def, int speed, int brains,
        int careMistakes, int weight, int happiness, int discipline, int battles, int techniqueCount,
        EvolutionResult evolutionResult)
    {
        // Arrange
        FromRookieOrChampionEvolutionCalculator sut = new SetupBuilder().Build();
        UserDigimon userDigimon = new DigimonBuilder()
            .WithDigimonType(digimonName)
            .WithHP(hp) /* ... */ .Build();

        // Act
        EvolutionResult result = sut.DetermineEvolutionResult(userDigimon);

        // Assert
        result.ShouldBe(evolutionResult);
    }
}
```

### Test Conventions

1. **Fixture attribute:** `[TestFixture]` on test classes, `sealed`
2. **Test naming:** `MethodName_ShouldExpectedBehavior_WhenCondition`
3. **Parameterized tests:** `[TestCase(...)]` for multiple input scenarios
4. **AAA pattern:** Always use `// Arrange`, `// Act`, `// Assert` comments
5. **Builder pattern:** Use `DigimonBuilder` for test data, `SetupBuilder` (inner class) for SUT setup
6. **Session cleanup:** `SetupBuilder` constructor clears `Session.HistoricEvolutions`
7. **Exception tests:** `Action throwingAction = () => sut.Method(); throwingAction.ShouldThrow<Exception>();`
8. **Test project structure:** Mirrors source project folder structure

### Running Tests

```bash
dotnet test DigimonWorldNet6/DigimonWorld.sln
```

---

## How to Add New Features

### Adding a New Digimon Evolution

1. Add the name to `DigimonName` enum in `Shared/Enums/DigimonName.cs`
2. Add corresponding entry to `EvolutionResult` enum in `Shared/Enums/EvolutionResult.cs`
3. Add stage mapping in `Shared/EvolutionStageMapper.cs`
4. Create evolution criteria class implementing `IEvolutionCriteria` in `Evolution.Calculator.Core/EvolutionCriteria/{GameVariant}/{Stage}/`
5. Register in the appropriate mapper (e.g., `FromRookieOrChampionEvolutionMapper`)
6. If it's a new Game Variant digimon, create the `Digimon` entry in `Shared/Constants/DigimonTypes.cs`
7. Add `HistoricEvolutions` entry in `MemoryAccess/MemoryValues/Evolution/HistoricEvolutions.cs` with correct memory offset/bit
8. Add tests using the `DigimonBuilder` and `SetupBuilder` patterns
9. Add Digimon image to `Frontend.WPF/Images/Digimon/`

### Adding a New UI Feature

1. Create a UserControl under `Frontend.WPF/Windows/Main/UserControls/{FeatureName}/`
2. Create a corresponding ViewModel extending `BaseViewModel`
3. Wire DataContext in the UserControl's code-behind constructor
4. Add navigation entry in `NavigationLeftPaneViewModelComponent`
5. Subscribe to EventHub observables in the ViewModel constructor using `CompositeDisposable`
6. Implement `IDisposable` and dispose subscriptions

### Adding a New EventHub Signal

1. Add a `Subject<T>` field to the appropriate EventHub class
2. Add the `IObservable<T>` public property
3. Add the `Signal...()` public static method
4. Subscribe in consumers via `CompositeDisposable`

### Adding a New Memory Value

1. Find the PSX RAM offset for the value
2. Create or extend a `MemoryValueSyncBase` subclass in `MemoryAccess/MemoryValues/`
3. Add the offset constant and property
4. Override `OnUpdateData()` to read from memory
5. Signal via EventHub when data is read
6. Add `Empty` static property
7. Wire up in `LiveMemoryReader.Attach()`

---

## Common Bug Patterns to Watch For

1. **Forgetting to dispose Rx subscriptions** — always collect in `CompositeDisposable` and dispose
2. **Thread-safety with UI updates** — use `ObserveOn(SynchronizationContext.Current!)` for Rx→UI
3. **EventHub static state leaking in tests** — always clear `Session.HistoricEvolutions` in test setup
4. **GameVariant flag logic** — `IsAvailableIn()` has specific exclude-wins-over-include semantics
5. **Memory offset errors** — offsets are hex values relative to PSX RAM base; verify against cheat tables
6. **Null Object pattern consistency** — new `MemoryValueSyncBase` subclasses must provide `Empty` property
7. **Evolution criteria ordering** — the order of criteria in mapper evolution lists matters for priority
8. **Carried-over stats in Original vs Vice** — Original uses carried-over stats between evolutions, Vice does not

---

## File Organization Summary

```
DigimonWorldNet6/
├── Shared/                          # Leaf dependency — enums, constants, extensions, events, config
│   ├── Enums/                       # All domain enums
│   ├── Constants/                   # DigimonTypes, Digimon record struct, ActiveTimeHour
│   ├── Extensions/                  # Extension methods
│   ├── Services/Events/             # Static EventHub classes (Rx subjects)
│   ├── Services/                    # UserConfigurationManager, ActiveTimeMapper
│   └── Configuration/               # Config DTOs (UserConfiguration, etc.)
├── MemoryAccess/                    # PS1 emulator memory reading
│   ├── Core/                        # ProcessMemory, PsxRam
│   └── MemoryValues/                # Digimon/, Evolution/, World/ stat readers
├── Evolution.Calculator.Core/       # Domain logic
│   ├── DataObjects/                 # UserDigimon, EvolutionCriteria DTOs
│   ├── Interfaces/EvolutionCriteria/# IEvolutionCriteria, IEvolutionCalculator
│   ├── EvolutionCriteria/           # Per-variant/per-stage criteria data classes
│   ├── EvolutionCriteriaCalculation/# Stage-specific calculators and mappers
│   └── Modules/                     # DI module registration
├── Frontend.WPF/                    # WPF UI
│   ├── ViewModelComponents/         # BaseViewModel, CommandHandler, PaneBaseViewModel, RelayCommand
│   ├── Windows/BaseClasses/         # BaseWindow, BaseDialogWindow, BaseWindowViewModel
│   ├── Windows/Main/UserControls/   # Main features (EvolutionCalculator, TamerVision, DigiWiki, etc.)
│   ├── Services/                    # MusicPlayer, SpeakingSimulator, Icon factories
│   ├── Conversion/                  # WPF value converters
│   ├── Validation/                  # WPF validation rules
│   ├── Images/                      # Digimon images
│   ├── Fonts/                       # Custom DW1 font
│   └── Music/                       # Background music files
└── *Tests/                          # Test projects mirroring source structure
```

---

## License

© 2026 Ginoshie — Licensed for non-commercial use only. Modification, redistribution, or reuse of this code in other projects is strictly prohibited.

