# Copilot Instructions — Bug Finding, Debugging & Troubleshooting

> This file supplements `.github/copilot-instructions.md` with guidelines for finding, diagnosing, and fixing bugs.

---

## Common Bug Categories

### 1. Reactive Subscription Leaks

**Symptom:** Memory grows over time, stale event handlers fire, duplicate UI updates.

**Cause:** Rx subscriptions not collected in `CompositeDisposable` or `Dispose()` not called.

**How to find:**
- Search for `.Subscribe(` calls not wrapped in `CompositeDisposable`
- Check that `IDisposable` is implemented on classes with subscriptions
- Verify `Dispose()` is called on window close / UserControl unload

**Fix pattern:**
```csharp
// WRONG — leak!
EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(OnConnected);

// CORRECT — tracked and disposed
_disposables = new CompositeDisposable(
    EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(OnConnected)
);
// ...
public void Dispose() => _disposables.Dispose();
```

### 2. Cross-Thread UI Updates

**Symptom:** `InvalidOperationException` — "The calling thread cannot access this object because a different thread owns it."

**Cause:** Rx observable fires on a background thread (e.g., `Observable.Interval`, `Task.Run`), and the handler updates a WPF property.

**How to find:**
- Search for `.Subscribe(` calls that update UI-bound properties
- Check if `ObserveOn(SynchronizationContext.Current!)` is present in the chain

**Fix:**
```csharp
// Add ObserveOn before Subscribe for UI updates
myObservable
    .ObserveOn(SynchronizationContext.Current!)
    .Subscribe(value => MyUiProperty = value);
```

### 3. Evolution Criteria Order Bugs

**Symptom:** Wrong evolution result when multiple evolutions are enabled.

**Cause:** Evolution criteria lists in the mapper have a specific priority order (first = highest priority). Reordering changes results.

**How to find:**
- Check the order of `IEvolutionCriteria` instances in the mapper's evolution lists
- Verify the iteration order matches the game's actual priority

**Example:**
```csharp
// Order matters! GreymonEvolutionCriteria is checked BEFORE MeramonEvolutionCriteria
private IEnumerable<IEvolutionCriteria> AgumonEvolutions { get; } =
[
    new GreymonEvolutionCriteria(),    // Priority 1
    new MeramonEvolutionCriteria(),    // Priority 2
    new BirdramonEvolutionCriteria(),  // Priority 3
    // ...
];
```

### 4. GameVariant Flag Logic Errors

**Symptom:** Evolution criteria visible/hidden in the wrong game mode.

**Cause:** Incorrect `IncludeGameVariantFlags` / `ExcludeGameVariantFlags` on `Digimon` constants.

**How to find:**
- Check `DigimonTypes.cs` for the Digimon's flags
- Verify `IsAvailableIn()` logic: exclude always wins; then check include flags
- Watch for missing `ExcludeGameVariantFlags` parameter (defaults to `0`)

**Key rules:**
```
- Exclude always wins over Include
- Original mode: must have Original flag set
- Vice mode: must have Vice flag, then required patches must be present
- Extra active patches don't disqualify (only required patches matter)
```

### 5. Static State Pollution in Tests

**Symptom:** Tests pass individually but fail when run together; flaky test results.

**Cause:** `Session.HistoricEvolutions` or `UserConfigurationManager` state leaks between tests.

**Fix:** Always clear static state in `SetupBuilder` constructor:
```csharp
private sealed class SetupBuilder
{
    public SetupBuilder()
    {
        Session.HistoricEvolutions.Clear(); // MUST do this
    }
}
```

### 6. Memory Offset Errors

**Symptom:** Incorrect game values displayed; values that change at wrong times.

**Cause:** Wrong hex offset or wrong data type (`ReadByte` vs `ReadInt16` vs `ReadInt32`).

**How to diagnose:**
- Cross-reference offsets with `DW1_NTSC.CT` cheat table
- Verify data type matches the game's memory layout (1 byte vs 2 bytes vs 4 bytes)
- Check that the offset is relative to PSX RAM base (not absolute)

### 7. Null Object Pattern Inconsistencies

**Symptom:** `NullReferenceException` when emulator is not connected.

**Cause:** New `MemoryValueSyncBase` subclass missing `Empty` static property.

**Checklist for new memory value types:**
- [ ] Private parameterless constructor exists: `private MyStats() : this(ProcessMemory.Empty, PsxRam.Empty) { }`
- [ ] Static `Empty` property: `public static MyStats Empty { get; } = new();`
- [ ] Default property in `LiveMemoryReader`: `public MyStats MyStats { get; private set; } = MyStats.Empty;`

### 8. Carried-Over Stats Bug (Original vs Vice)

**Symptom:** Evolution scores differ from game in Original/Vice modes.

**Cause:** Carried-over stat averaging behaves differently between game versions.

**How it works:**
- **Original:** Stats accumulate across all evolution checks (running average)
- **Vice:** Stats are independent per evolution check (no carry-over)

**Check:** Verify `_dontUseCarriedOverStats` is correctly set based on `GameVariant`:
```csharp
_dontUseCarriedOverStats = config.GameVariant != GameVariant.Original;
```

### 9. HistoricEvolutions Bit Flag Errors

**Symptom:** Wrong Digimon marked as historic/not historic.

**Cause:** Incorrect address or bit position in `_map` dictionary or `HasBit()` calls.

**Note:** Some Digimon share addresses (Machinedramon and Myotismon both at `ADDR_232_OFFSET, bit 6`; Panjyamon and Weregarurumon both at `ADDR_232_OFFSET, bit 7`). This is because they occupy the same slot in different game versions.

### 10. Process Name Mismatch

**Symptom:** Emulator link never connects.

**Cause:** The emulator process name doesn't match the expected name.

**Default:** `"duckstation-qt-x64-ReleaseLTCG"` — different DuckStation builds use different executable names.

---

## Debugging Techniques

### Debugging Evolution Calculations

1. **Create a test case** with exact stats from the game:
   ```csharp
   [TestCase(DigimonName.Agumon, 1000, 1000, 100, 100, 100, 100, 0, 25, 80, 80, 0, 10, EvolutionResult.ExpectedResult)]
   ```
2. **Step through** `EvolutionEnabled()` to see which criteria pass/fail
3. **Check score calculation** — print `EvolutionScoreCalculationResult` values
4. **Verify mapper** — ensure correct evolution criteria list is returned for the GameVariant

### Debugging Memory Access

1. **Verify PSX RAM base** — `Console.WriteLine` in `PsxRam` constructor shows the base address
2. **Compare with Cheat Engine** — open `DW1_NTSC.CT` and verify values match
3. **Check `Connected` state** — ensure `EmulatorLinkEventHub.SignalEmulatorConnected` fires
4. **Test with Empty objects** — verify graceful degradation when disconnected

### Debugging WPF UI

1. **Check DataContext** — ensure it's set correctly (not null)
2. **Check binding errors** — look for binding errors in Debug Output window
3. **Verify thread** — UI updates must happen on the UI thread
4. **Test converters** — verify converter logic with unit tests
5. **Validation rules** — test `Validate()` method directly

---

## Code Review Checklist

When reviewing code changes, verify:

- [ ] `sealed` keyword on new classes (unless designed for inheritance)
- [ ] File-scoped namespace used
- [ ] Constants in `UPPER_SNAKE_CASE`
- [ ] Private fields prefixed with `_`
- [ ] `field` keyword used for semi-auto properties (not explicit backing fields)
- [ ] Rx subscriptions collected in `CompositeDisposable`
- [ ] `IDisposable` implemented when `CompositeDisposable` is used
- [ ] `ObserveOn(SynchronizationContext.Current!)` for UI-bound Rx subscriptions
- [ ] New `MemoryValueSyncBase` subclasses have `Empty` property
- [ ] Tests follow naming convention: `Method_ShouldX_WhenY`
- [ ] Tests use Shouldly assertions (not NUnit `Assert`)
- [ ] Tests use `DigimonBuilder` for test data
- [ ] Tests clear static state in `SetupBuilder` constructor
- [ ] New enums added to all relevant places (DigimonName, EvolutionResult, mapper, etc.)
- [ ] GameVariant flags set correctly on new `Digimon` constants

---

## Performance Considerations

1. **Memory polling interval** — 1 second via `Observable.Interval(TimeSpan.FromSeconds(1))`. Don't reduce without good reason.
2. **Signature scanning** — `PsxRam` constructor scans all readable process memory. This is expensive but runs only once on attach.
3. **Evolution criteria evaluation** — `GetEvolutionCriteria()` uses LINQ with `.Where().Single()` on every calculation. Cache if this becomes a bottleneck.
4. **Pane animations** — 60fps at 600ms. The `AnimateOffset` observable generates many values; ensure subscriptions are properly disposed when panes are destroyed.

---

## Known Technical Debt

1. `FromRookieOrChampionEvolutionMapper` constructor creates all criteria upfront — could be lazy
2. Some EventHub classes use `Subject<Unit>` where `BehaviorSubject` might be more appropriate (subscribers miss signals if they subscribe late)

## Intentional Design Choices (NOT Technical Debt)

The following are deliberate simplicity choices — do NOT refactor these toward "cleaner" patterns:

- `EvolutionCalculatorModule.RegisterServices()` is empty — DI is intentionally not used; services are accessed via singletons and static classes
- ViewModels create `UserControl` instances directly — this is simpler than abstracting navigation
- `Session.HistoricEvolutions` is static mutable state — kept simple; tests just clear it in setup
- Music player is a static class — it's a singleton concern with no need for testability via interfaces

