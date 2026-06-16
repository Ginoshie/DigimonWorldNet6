# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Maintaining this file (read first)

This file is the **single source of truth** for how to work in this repo — conventions, design patterns, architecture, and commands. It must never lag the code:

- **Auto-update on code change.** Whenever you change code that affects anything documented here — architecture, the dependency graph, project layout, a convention, or a build/run/test command — update this file in the *same turn*, without being asked. The documentation update is part of the change, not a follow-up: if an edit makes a statement here wrong or incomplete, fix it before you finish.
- **Auto-update on correction.** When the user corrects or confirms a convention or design pattern, record it here in the *same turn*, unprompted — so it only has to be said once. Keep entries concise and tell the user what you changed.
- Never defer either of these or wait to be asked. When prose (including this file) might disagree with the code, verify against the code and correct the prose.

## Project overview

A **Digimon World 1 (PS1)** companion desktop app: **.NET 10 / WPF, Windows-only**. It calculates evolution outcomes from a Digimon's stats, reads live game state from the **DuckStation** emulator via process-memory access (Win32 P/Invoke), and bundles tools (Digi-Wiki, Tamer Vision overlay, cheat sheet, music player). Distributed via **Velopack** auto-updating installer. Current version is in [`Version.props`](DigimonWorldNet6/Version.props) (`AppVersion`).

- **Solution:** `DigimonWorldNet6/DigimonWorld.sln` (note: nested one level under the repo root)
- **SDK:** .NET 10.0 pinned in [`global.json`](DigimonWorldNet6/global.json) (`rollForward: latestMajor`)
- **Language:** C# 14 (via the .NET 10 SDK; `LangVersion` is `default`), nullable enabled everywhere

## Build, run, test

Run from the repo root (`C:\git\DigimonWorldNet6`). There is no separate lint step — analyzer/compiler warnings are the linter, and conventions below are enforced by review.

```powershell
# Build the whole solution
dotnet build DigimonWorldNet6/DigimonWorld.sln

# Run the app (WPF executable)
dotnet run --project DigimonWorldNet6/Frontend.WPF/Frontend.WPF.csproj

# Run all tests
dotnet test DigimonWorldNet6/DigimonWorld.sln

# Run one test project
dotnet test DigimonWorldNet6/Evolution.Calculator.Tests/Evolution.Calculator.Tests.csproj

# Run a single test / class (substring match on fully-qualified name)
dotnet test DigimonWorldNet6/DigimonWorld.sln --filter "FullyQualifiedName~DetermineEvolutionResult_ShouldReturnExpectedDigimon"
dotnet test DigimonWorldNet6/DigimonWorld.sln --filter "FullyQualifiedName~FromRookieOrChampion"
```

## Architecture (the big picture)

### Project layering (leaf → top)

```
Shared                  enums, constants (DigimonTypes), extensions, Rx EventHubs, config DTOs
  ← MemoryAccess        DuckStation process-memory reading via P/Invoke
  ← Domain              UserDigimon, Session (static HistoricEvolutions + UserDigimon.Instance)
  ← Evolution.Calculator.Core   evolution calculation engine (refs Domain, MemoryAccess, Shared)
  ← Frontend.WPF (WinExe)       MVVM UI (refs Domain, Evolution.Calculator.Core)
```

Test projects (`Evolution.Calculator.Tests`, `Frontend.WPF.Tests`, `Shared.Tests`) mirror the source folder structure. `Domain` exposes `InternalsVisibleTo` to `Evolution.Calculator.Tests` and `Frontend.WPF`.

> `UserDigimon` and `Session` live in **`Domain`** — not in `Evolution.Calculator.Core` or `Shared`. `UserDigimon` is now a singleton (`UserDigimon.Instance`); tests still build throwaway instances with `DigimonBuilder`.

### Deliberate simplicity — do NOT "improve" these

This codebase intentionally avoids abstraction. **Do not introduce DI containers, service interfaces, IoC, or navigation abstractions.** `IHost` is used only to resolve `MainWindow` at startup; `EvolutionCalculatorModule.RegisterServices()` is intentionally empty. Instead:

- **Singletons** for core services: `EvolutionCalculator.Instance`, `LiveMemoryReader.Instance` (both `Lazy<T>` + private ctor)
- **Static classes** for cross-cutting concerns: `ServiceRelay` (façade), `UserConfigurationManager`, the EventHubs, `MusicPlayer`, `SoundService`
- **Direct `new`** for ViewModels and `UserControl`s — never resolved from a container
- **Static mutable state** (`Session.HistoricEvolutions`) — kept simple; tests clear it in setup

### Cross-component communication: Rx EventHubs

The primary way components talk across project boundaries is **static EventHub classes** in `Shared/Services/Events/` exposing `System.Reactive` observables (`EmulatorLinkEventHub`, `DigimonStatsEventHub`, `MusicPlayerEventHub`, `HistoricEvolutionEventhub`). Pattern: private `Subject`/`BehaviorSubject` → public `IObservable<T>` → public static `Signal…()` method. Use `BehaviorSubject<T>` when late subscribers need the latest value, `Subject<Unit>` for fire-and-forget pings.

Consumers collect subscriptions in a `CompositeDisposable` and dispose them in `Dispose()`. **UI-bound subscriptions must `.ObserveOn(SynchronizationContext.Current!)`** — EventHub signals and the 1-second memory poll fire on background threads.

### Evolution engine (Evolution.Calculator.Core)

`EvolutionCalculator.CalculateEvolutionResult(UserDigimon)` dispatches by `EvolutionStage` (Strategy pattern) to a stage calculator (`FromFresh`/`FromInTraining`/`FromRookieOrChampion`/`FromUltimate`). Each pulls a priority-ordered list of `IEvolutionCriteria` from a mapper, filtered by the active `GameVariant`; an evolution is "enabled" when **≥3 of its 4 criteria categories** (Stats / CareMistakes / Weight / BonusCriteria) are met; the highest-scoring enabled evolution wins, with **new evolutions prioritized over historic ones**. `GameVariant` is a `[Flags]` enum (Original / Vice / patches); criteria-list order is load-bearing.

### Memory access (MemoryAccess)

`LiveMemoryReader.Instance` monitors for the emulator process, then `PsxRam` locates the PSX RAM base by byte-signature scan and `ProcessMemory` wraps `ReadProcessMemory`. Each stat reader extends `MemoryValueSyncBase` (auto-polls every 1s while connected) and signals an EventHub on read. Every reader exposes a **Null Object `Empty`** instance used while disconnected.

### WPF frontend

MVVM with no framework magic. `BaseViewModel` (`INotifyPropertyChanged` + `SetField<T>`) → `BaseWindowViewModel` / `PaneBaseViewModel`. Commands are `CommandHandler` (parameterless) and `RelayCommand<T>`. Windows are opened by the caller: `new Window`, `new ViewModel(window)`, set `DataContext`, then `Show()`/`ShowDialog()`. Music/SFX use **NAudio** (`WaveOutEvent`).

## Conventions that must not be violated

1. **File-scoped namespaces** (`namespace X.Y.Z;`).
2. **`sealed` by default** unless designed for inheritance.
3. **Primary constructors** on data/service classes where natural.
4. **`field` keyword** for semi-auto properties — no hand-written backing fields.
5. **Braces always** on `if`/`else`/`else if` — even single-statement bodies. Never single-line `if (x) return;`.
6. **`else` / `else if` / `catch` go directly after the closing `}`**, no blank line between.
7. **No trailing blank line** at the end of a file.
8. Nullable enabled; constants `UPPER_SNAKE_CASE`; private fields `_camelCase`.
9. **Explicit `using`s** in `Frontend.WPF` and `Evolution.Calculator.Core` (no `ImplicitUsings`); `Shared` and `MemoryAccess` use `ImplicitUsings`.
10. New `MemoryValueSyncBase` subclasses **must** provide an `Empty` Null Object (private parameterless ctor → `this(ProcessMemory.Empty, PsxRam.Empty)`) and a default in `LiveMemoryReader`.

### Tests

NUnit 4 + **Shouldly** (`result.ShouldBe(...)`, never `Assert.AreEqual`). `[TestFixture] sealed` classes; method names `Method_ShouldExpected_WhenCondition`; AAA comments. Use `DigimonBuilder` for `UserDigimon` and an inner `SetupBuilder` for the SUT. **The `SetupBuilder` constructor must `Session.HistoricEvolutions.Clear()`** to avoid static-state bleed. Exceptions: `Action act = () => …; act.ShouldThrow<T>();`.

## License

© 2026 Ginoshie — non-commercial use only; modification, redistribution, or reuse of the source in other projects is prohibited.
