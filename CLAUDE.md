# CLAUDE.md

Guidance for working in this repo. Keep it terse; move task-specific procedure into skills (see **Skills** below).

## Maintaining this file

- **Update in the same turn** you change anything documented here (architecture, layering, a convention, a build/run/test command) — unprompted. The doc edit is part of the change.
- When the user corrects a convention, record it here (or in the relevant skill) the same turn. Verify prose against code when they might disagree.

## Project overview

**Digimon World 1 (PS1)** companion desktop app — **.NET 10 / WPF, Windows-only**. Calculates evolution outcomes, reads live game state from the **DuckStation** emulator via process-memory (Win32 P/Invoke), bundles tools (Digi-Wiki, Tamer Vision overlay, cheat sheet, music player). Ships via **Velopack** auto-updater. Version in [`Version.props`](DigimonWorldNet6/Version.props).

- **Solution:** `DigimonWorldNet6/DigimonWorld.sln` (nested one level under repo root)
- **SDK:** .NET 10 pinned in [`global.json`](DigimonWorldNet6/global.json); C# 14, nullable enabled everywhere
- No lint step — compiler/analyzer warnings are the linter.

## Build, run, test

Run from repo root (`C:\git\DigimonWorldNet6`).

```powershell
dotnet build DigimonWorldNet6/DigimonWorld.sln
dotnet run --project DigimonWorldNet6/Frontend.WPF/Frontend.WPF.csproj
dotnet test DigimonWorldNet6/DigimonWorld.sln
dotnet test DigimonWorldNet6/DigimonWorld.sln --filter "FullyQualifiedName~SomeTestNameOrClass"
```

## Architecture

Project layering (leaf → top):

```
Shared                enums, constants, extensions, Rx EventHubs, config DTOs
  ← MemoryAccess      DuckStation process-memory via P/Invoke
  ← Domain            UserDigimon (singleton UserDigimon.Instance), Session (static HistoricEvolutions)
  ← Evolution.Calculator.Core   evolution engine
  ← Frontend.WPF (WinExe)       MVVM UI
```

- Test projects mirror source folders. `Domain` exposes `InternalsVisibleTo` to `Evolution.Calculator.Tests` and `Frontend.WPF`.
- `UserDigimon`/`Session` live in **`Domain`**. Tests build throwaway `UserDigimon`s with `DigimonBuilder`.
- **Cross-project comms = static EventHubs** in `Shared/Services/Events/` (`EmulatorLinkEventHub`, etc.): private `Subject`/`BehaviorSubject` → public `IObservable<T>` → static `Signal…()`. Consumers collect subs in a `CompositeDisposable`; **UI-bound subs must `.ObserveOn(SynchronizationContext.Current!)`** (signals + the 1s poll fire on background threads).
- **Evolution engine:** `EvolutionCalculator.CalculateEvolutionResult` dispatches by `EvolutionStage` (Strategy) to a stage calculator; pulls a priority-ordered `IEvolutionCriteria` list filtered by `GameVariant` (`[Flags]`); evolution "enabled" when **≥3 of 4 criteria categories** met; highest-scoring enabled wins, new prioritized over historic. Criteria-list order is load-bearing.
- **Memory access:** `LiveMemoryReader.Instance` finds the emulator; `PsxRam` locates RAM base by signature scan; `ProcessMemory` wraps Read/Write. → **skill: `memory-values`** before adding a reader.
- **WPF frontend:** MVVM, no framework magic. `BaseViewModel` (`INotifyPropertyChanged` + `SetField<T>`); commands are `CommandHandler` / `RelayCommand<T>`; windows opened by caller (`new Window`, `new ViewModel(window)`, set `DataContext`, `Show()`). NAudio for sound. → **skill: `wpf-styles`** for Style/DataTemplate conventions; **skill: `cheatsheet-values`** for cheat-sheet inputs/locking.

## Deliberate simplicity — do NOT "improve" these

Intentionally avoids abstraction. **No DI containers, service interfaces, IoC, or navigation abstractions.** `IHost` only resolves `MainWindow` at startup; `EvolutionCalculatorModule.RegisterServices()` is intentionally empty.

- **Singletons** for core services (`EvolutionCalculator.Instance`, `LiveMemoryReader.Instance` — `Lazy<T>` + private ctor).
- **Static classes** for cross-cutting concerns (`ServiceRelay`, `UserConfigurationManager`, EventHubs, `MusicPlayer`, `SoundService`).
- **Direct `new`** for ViewModels and `UserControl`s. **Static mutable state** (`Session.HistoricEvolutions`) kept simple; tests clear it.

## Conventions that must not be violated

1. **File-scoped namespaces.**
2. **`sealed` by default** unless designed for inheritance.
3. **Primary constructors** where natural.
4. **`field` keyword** for semi-auto properties — no hand-written backing fields.
5. **Braces always** on `if`/`else`/`else if` — never single-line `if (x) return;`.
6. **`else`/`else if`/`catch`** directly after the closing `}`, no blank line.
7. **No trailing blank line** at end of file.
8. Nullable enabled; constants `UPPER_SNAKE_CASE`; private fields `_camelCase`.
9. **Explicit `using`s** in `Frontend.WPF` and `Evolution.Calculator.Core`; `Shared` and `MemoryAccess` use `ImplicitUsings`.
10. New `MemoryValueSyncBase` subclasses **must** provide an `Empty` Null Object and a default in `LiveMemoryReader` (→ skill `memory-values`).

### Tests

NUnit 4 + **Shouldly** (`result.ShouldBe(...)`, never `Assert.AreEqual`). `[TestFixture] sealed`; methods `Method_ShouldExpected_WhenCondition`; AAA comments. Use `DigimonBuilder` + an inner `SetupBuilder` for the SUT. **`SetupBuilder` ctor must `Session.HistoricEvolutions.Clear()`** (static-state bleed). Exceptions: `Action act = () => …; act.ShouldThrow<T>();`.

## Skills

Invoke before doing the matching task — they hold the detailed conventions:

- **`wpf-styles`** — adding/editing any WPF `Style`, `DataTemplate`, or `ResourceDictionary`.
- **`memory-values`** — adding/modifying a DuckStation memory reader.
- **`cheatsheet-values`** — adding/changing cheat-sheet fields, value locking, or input behaviors.

## Working principles

Bias to caution over speed; use judgment on trivial tasks.

1. **Think before coding.** State assumptions. If multiple interpretations or a simpler approach exist, say so *before* building. If unclear, stop and ask.
2. **Simplicity first.** Minimum code that solves the problem — no speculative features, abstractions, config, or error handling for impossible cases. If 200 lines could be 50, rewrite. Ask: "would a senior call this overcomplicated?" Build the dumbest thing that works; add machinery only against failure modes you can name and have seen.
3. **Surgical changes.** Touch only what the request needs; match existing style; remove only the orphans your change created; mention (don't delete) unrelated dead code.
4. **Goal-driven.** Turn the task into a verifiable check (a test, a repro, "tests pass") and loop until it holds. State a brief plan for multi-step work. If you can't verify locally, say so plainly.

## License

© 2026 Ginoshie — non-commercial use only; modification, redistribution, or reuse of the source in other projects is prohibited.
