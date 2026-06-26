---
name: cheatsheet-values
description: How cheat-sheet input fields work in the Frontend.WPF project of this Digimon World app — the value view-models (MemoryValueViewModel / NumericMemoryValueViewModel / LongMemoryValueViewModel), the root-attached behaviors that reach every field by binding-path, value locking/freeze, the lock highlight, and TextBox context-menu suppression. Use when adding or changing cheat-sheet fields, lockable values, or right-click/edit input behavior.
---

# Cheat-sheet values, behaviors & locking

## Value view-models
Cheat-sheet fields bind to small VMs under `…/CheatSheet/Models/`:
- `MemoryValueViewModel` (bool), `NumericMemoryValueViewModel` (int), `LongMemoryValueViewModel` (long).
- All implement `IRefreshable` (the 1s poll calls `Refresh()`); the numeric/long ones also implement `IEditableValue` (`IsEditing`); all implement `ILockableValue { bool IsLocked; void PushLockedValueToMemory(); }`.
- `CheatSheetViewModel` aggregates them into `_refreshables` (and `_lockables = _refreshables.OfType<ILockableValue>()`), and drives them from merged EventHub observables on `SynchronizationContext.Current`.
- Add a new value: construct the VM with getter/setter lambdas over `LiveMemoryReader.Instance.X`, add it to the relevant `_refreshables.AddRange(...)` block. Inventory wraps two `NumericMemoryValueViewModel`s per slot.

## Reaching fields without per-field wiring
A single `Behavior<FrameworkElement>` (`Microsoft.Xaml.Behaviors`) is attached **once** to the cheat-sheet root `Border` in `CheatSheetUserControl.xaml`. It class-handles a routed input event with `handledEventsToo: true`, walks up from `e.OriginalSource` to the input control, and resolves that control's VM from its binding expression — `expression.ResolvedSource`, else walk the dotted `Path` (`TechnicalRng.Value` → the `TechnicalRng` VM). Two behaviors use this:
- `SuspendRefreshWhileEditingBehavior` — focus → sets `IEditableValue.IsEditing` so the poll doesn't fight typing.
- `LockOnRightClickBehavior` — right-click → toggles `ILockableValue.IsLocked` and sets the `LockState.IsLocked` attached property on the control. (No subscription/tracking needed: nothing else mutates `IsLocked`, so set it directly.)

## Locking = Cheat-Engine freeze
While `IsLocked`, `Refresh()` skips the read (display stays put). A dedicated `Observable.Interval(FREEZE_INTERVAL_MS = 100ms)` loop in `CheatSheetViewModel` (separate from the 1s read poll; both in its `CompositeDisposable`) calls `PushLockedValueToMemory()` on each lockable, re-asserting the held value (`_lastNotifiedValue`). Runtime-only — no persistence. A poll-based freeze can't win a sub-frame race (e.g. a value the game consumes the same frame it writes it); that's a known limit.

## Lock highlight
The `LockState.IsLocked` attached property (`Behaviors/LockState.cs`) is set by the behavior; each input's control template has a `Trigger` recoloring its **visible border** to `#D4D604`. Do **not** use an adorner overlay — it double-scales under `LayoutTransform` and bleeds onto other sections when the owning section is `Visibility`-collapsed.

## Suppressing the default TextBox context menu
`ContextMenuBehavior.IsDisabled` (set in `DefaultTextBoxStyle`) assigns the TextBox a **non-null empty `ContextMenu`** *and* handles `ContextMenuOpening`. The empty menu is load-bearing: WPF's `TextEditor` injects its Cut/Copy/Paste menu via a class handler that runs before any instance handler, but only when `ContextMenu` is `null` — so handling the event alone does not suppress it.
