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

`LiveMemoryReader.Instance` monitors for the emulator process, then `PsxRam` locates the PSX RAM base by byte-signature scan and `ProcessMemory` wraps `ReadProcessMemory`/`WriteProcessMemory` (reads on the 1s poll; writes when a stat property setter is assigned — e.g. cheat-sheet checkbox toggles persist into game RAM). Each stat reader extends `MemoryValueSyncBase` (auto-polls every 1s while connected) and signals an EventHub on read. Every reader exposes a **Null Object `Empty`** instance used while disconnected.

### WPF frontend

MVVM with no framework magic. `BaseViewModel` (`INotifyPropertyChanged` + `SetField<T>`) → `BaseWindowViewModel` / `PaneBaseViewModel`. Commands are `CommandHandler` (parameterless) and `RelayCommand<T>`. Windows are opened by the caller: `new Window`, `new ViewModel(window)`, set `DataContext`, then `Show()`/`ShowDialog()`. Music/SFX use **NAudio** (`WaveOutEvent`). **WPF style convention (must follow):** every `Style` lives in **its own file** under `ResourceDictionaries/Styles/` — never inline, never lumped together with an unrelated style. The first/primary style for a given control type is *the default*, keyed `DefaultXxxStyle` (e.g. `DefaultButtonStyle`, `DefaultComboboxStyle`, `DefaultCheckboxStyle`); there is **at most one default per element + style-type**. Styles are merged into each consumer's `MergedDictionaries` and applied explicitly via `Style="{StaticResource DefaultXxxStyle}"`. UserControl-specific styles and `DataTemplate`s each live in their own file (same one-per-file rule) in a `Styles/` or `DataTemplates/` subfolder **beside that UserControl**, merged into its `MergedDictionaries`. **Gotcha:** `App.xaml` defines *implicit* (keyless) styles for `TextBlock` (DW1 font, bold), `Control` (DW1 font), and **`Grid` (background `#2E3841`)** — so every `Grid` paints that dark background. When laying out content over a different-colored surface (e.g. a list/panel), use a `StackPanel`/`Border` or set `Background="Transparent"`, or the `Grid` will show a dark box. **Shared dimensions convention:** when a size is reused (especially square button/icon/label dimensions) or is likely to be tweaked, declare it once as a resource — `<system:Double x:Key="ButtonDimension">41</system:Double>` (`xmlns:system="clr-namespace:System;assembly=System.Runtime"`) — and reference it via `{StaticResource ButtonDimension}` for both `Width` and `Height`, rather than hard-coding the number in multiple places. This keeps elements square and makes the dimension adjustable in one spot. **No comments in XAML:** keep `.xaml` files free of `<!-- … -->` comments — element/attribute names should be self-explanatory; do not annotate sections or styles inline.

### Cheat-sheet input behaviors & value locking

Cheat-sheet inputs are reached **without per-field wiring** by attaching a single `Behavior<FrameworkElement>` (`Microsoft.Xaml.Behaviors`) once to the cheat-sheet root `Border` (in `CheatSheetUserControl.xaml`). Each behavior class-handles a routed input event with `handledEventsToo: true`, walks up from `e.OriginalSource` to the input control, and resolves that control's value VM by reading its binding expression and walking the dotted `Path` (e.g. `TechnicalRng.Value` → the `TechnicalRng` VM). This pattern is shared by `SuspendRefreshWhileEditingBehavior` (sets `IEditableValue.IsEditing` so the 1s poll doesn't fight typing) and `LockOnRightClickBehavior` (right-click toggles `ILockableValue.IsLocked`).

Cheat-sheet value VMs (`MemoryValueViewModel`, `NumericMemoryValueViewModel`, `LongMemoryValueViewModel`) implement `IRefreshable` and now also `ILockableValue { bool IsLocked; void PushLockedValueToMemory(); }`. **Locking = Cheat-Engine-style freeze:** while locked, `Refresh()` skips the read (display stays put) and a dedicated `Observable.Interval(FREEZE_INTERVAL_MS = 100ms)` loop in `CheatSheetViewModel` (separate from the 1s read poll, both in its `CompositeDisposable`) re-asserts the held value via `PushLockedValueToMemory()`. The lock is purely runtime (no persistence). The highlight is driven by the `LockState.IsLocked` **attached property** the behavior sets on the control; each control template has a `Trigger` recoloring its **visible border** to `#D4D604` (never an adorner overlay — those break under layout transforms and collapsed sections).

**Suppressing the default TextBox context menu:** the `ContextMenuBehavior.IsDisabled` attached property (set in `DefaultTextBoxStyle`) assigns the TextBox a non-null empty `ContextMenu` **and** handles `ContextMenuOpening`. The empty menu is load-bearing: WPF's `TextEditor` injects its Cut/Copy/Paste menu via a class handler that runs before any instance handler, but only when `ContextMenu` is `null` — so handling the event alone does not suppress it.

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

Behavioral guidelines to reduce common LLM coding mistakes. Merge with project-specific instructions as needed.

**Tradeoff:** These guidelines bias toward caution over speed. For trivial tasks, use judgment.

## 1. Think Before Coding

**Don't assume. Don't hide confusion. Surface tradeoffs.**

Before implementing:
- State your assumptions explicitly. If uncertain, ask.
- If multiple interpretations exist, present them - don't pick silently.
- If a simpler approach exists, say so. Push back when warranted.
- If something is unclear, stop. Name what's confusing. Ask.

## 2. Simplicity First

**Minimum code that solves the problem. Nothing speculative.**

- No features beyond what was asked.
- No abstractions for single-use code.
- No "flexibility" or "configurability" that wasn't requested.
- No error handling for impossible scenarios.
- If you write 200 lines and it could be 50, rewrite it.

Ask yourself: "Would a senior engineer say this is overcomplicated?" If yes, simplify.

## 3. Surgical Changes

**Touch only what you must. Clean up only your own mess.**

When editing existing code:
- Don't "improve" adjacent code, comments, or formatting.
- Don't refactor things that aren't broken.
- Match existing style, even if you'd do it differently.
- If you notice unrelated dead code, mention it - don't delete it.

When your changes create orphans:
- Remove imports/variables/functions that YOUR changes made unused.
- Don't remove pre-existing dead code unless asked.

The test: Every changed line should trace directly to the user's request.

## 4. Goal-Driven Execution

**Define success criteria. Loop until verified.**

Transform tasks into verifiable goals:
- "Add validation" → "Write tests for invalid inputs, then make them pass"
- "Fix the bug" → "Write a test that reproduces it, then make it pass"
- "Refactor X" → "Ensure tests pass before and after"

For multi-step tasks, state a brief plan:
```
1. [Step] → verify: [check]
2. [Step] → verify: [check]
3. [Step] → verify: [check]
```

Strong success criteria let you loop independently. Weak criteria ("make it work") require constant clarification.

---

**These guidelines are working if:** fewer unnecessary changes in diffs, fewer rewrites due to overcomplication, and clarifying questions come before implementation rather than after mistakes.