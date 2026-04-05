# Copilot Instructions — Memory Access Layer

> This file supplements `.github/copilot-instructions.md` with deep details on the MemoryAccess project.

---

## Overview

The MemoryAccess project reads live game data from a PS1 emulator (DuckStation) by directly reading the emulator's process memory using Win32 P/Invoke. It finds the PSX RAM region via byte-signature scanning and then reads specific memory offsets that correspond to in-game values.

---

## Core Classes

### ProcessMemory (`MemoryAccess/Core/ProcessMemory.cs`)

Wraps Win32 `ReadProcessMemory` P/Invoke for reading emulator process memory.

```csharp
public class ProcessMemory
{
    // Opens process with full access (0x1F0FFF)
    public ProcessMemory(Process process) { Handle = OpenProcess(0x1F0FFF, false, process.Id); }

    public IntPtr Handle { get; }

    // Read methods
    public virtual byte ReadByte(IntPtr addr)    // reads 1 byte
    public virtual byte[] ReadBytes(IntPtr addr, int length)  // reads N bytes with full error checking
    public virtual short ReadInt16(IntPtr addr)  // reads 2 bytes as Int16
    public virtual int ReadInt32(IntPtr addr)    // reads 4 bytes as Int32

    // Null Object pattern
    public static ProcessMemory Empty { get; } = new EmptyProcessMemory();

    // EmptyProcessMemory returns 0/empty for all reads (safe when disconnected)
    private class EmptyProcessMemory : ProcessMemory { ... }
}
```

**Key patterns:**
- Uses virtual methods so `EmptyProcessMemory` can override with safe defaults
- `ReadBytes` includes full error handling (`Win32Exception`, `IOException` for partial reads)
- P/Invoke declarations are `private static extern`

### PsxRam (`MemoryAccess/Core/PsxRam.cs`)

Dynamically locates the PSX RAM base address in the emulator's memory by scanning for a known byte signature.

```csharp
public class PsxRam
{
    public PsxRam(ProcessMemory mem)
    {
        // Scan for a 64-byte MIPS instruction signature
        Base = ScanForPattern(mem.Handle, sig);
        Base -= 0x90800;  // Adjust to actual PSX RAM start
    }

    public IntPtr Base { get; }

    // Convert a game offset to an absolute address
    public virtual IntPtr A(int offset) => Base + offset;

    // Null Object
    public static PsxRam Empty { get; } = new EmptyPsxRam();
}
```

**Signature scanning:**
- Uses `VirtualQueryEx` to enumerate readable memory regions
- Performs brute-force byte pattern matching in committed memory pages
- The signature is a specific MIPS instruction sequence from the DuckStation build
- After finding the signature, subtracts `0x90800` to get the PSX RAM base

### MemoryValueSyncBase (`MemoryAccess/MemoryValues/MemoryValueSyncBase.cs`)

Abstract base class for all auto-syncing memory value readers.

```csharp
public abstract class MemoryValueSyncBase : IDisposable
{
    private readonly SerialDisposable _updateDataSubscription = new();

    protected MemoryValueSyncBase()
    {
        // Subscribe to emulator connection state
        EmulatorLinkEventHub.EmulatorConnectedObservable.Subscribe(OnEmulatorConnected);
    }

    private void OnEmulatorConnected(bool isConnected)
    {
        if (isConnected)
            // Poll every 1 second
            _updateDataSubscription.Disposable = Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => OnUpdateData());
        else
            _updateDataSubscription.Dispose();
    }

    protected virtual void OnUpdateData() { }  // Override to read memory
    public void UpdateData() => OnUpdateData(); // Manual trigger (used on first attach)
}
```

**Key behaviors:**
- `SerialDisposable` ensures the previous polling subscription is disposed when a new one starts
- Auto-starts polling when emulator connects, auto-stops when it disconnects
- Subclasses override `OnUpdateData()` to read their specific memory values
- `UpdateData()` is called once manually on attach for immediate data

---

## Memory Value Readers

All in `MemoryAccess/MemoryValues/` — each extends `MemoryValueSyncBase` and uses primary constructors:

### ParameterStats (`Digimon/ParameterStats.cs`)

Combat stats: HP, MP, Offense, Defense, Speed, Brains, CurrentHP, CurrentMP, Lives.

```csharp
public sealed class ParameterStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int OFFENSE_OFFSET = 0x001557E0;
    private const int DEFENSE_OFFSET = 0x001557E2;
    private const int SPEED_OFFSET   = 0x001557E4;
    private const int BRAINS_OFFSET  = 0x001557E6;
    private const int HP_OFFSET      = 0x001557F0;
    private const int MP_OFFSET      = 0x001557F2;
    // ...

    protected override void OnUpdateData()
    {
        Offense = mem.ReadInt16(ram.A(OFFENSE_OFFSET));
        Defense = mem.ReadInt16(ram.A(DEFENSE_OFFSET));
        // ...
        EmulatorLinkEventHub.SignalDigimonParameterStatsSynchronized();
    }
}
```

### ConditionStats (`Digimon/ConditionStats.cs`)

Non-combat stats: Happiness, Discipline, etc.

### ProfileStats (`Digimon/ProfileStats.cs`)

Digimon identity: Type/name, weight.

### CareStats (`Digimon/CareStats.cs`)

Care-related: Care mistakes count.

### TechniqueStats (`Digimon/TechniqueStats.cs`)

Technique data: Known technique count, battle count.

### HistoricEvolutions (`Evolution/HistoricEvolutions.cs`)

Reads bit flags from multiple memory addresses to determine which Digimon have been previously evolved.

```csharp
public class HistoricEvolutions(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int ADDR_225_OFFSET = 0x001BE00D;
    // ... ADDR_226 through ADDR_233

    private byte _flags225, _flags226, ..., _flags233;

    // Each Digimon is a specific bit in a specific byte
    public bool Agumon => HasBit(ADDR_225_OFFSET, 3);
    public bool Greymon => HasBit(ADDR_225_OFFSET, 5);
    // ...

    // Lookup by DigimonName enum
    public bool IsHistoricEvolution(DigimonName name) =>
        _map.TryGetValue(name, out var entry) && HasBit(entry.addr, entry.bit);

    private bool HasBit(int address, int bit)
    {
        byte value = address switch { ... };  // match to cached byte
        return (value & (1 << bit)) != 0;
    }
}
```

**Pattern for each Digimon:** `(address_offset, bit_position)` — stored in `_map` dictionary.

---

## LiveMemoryReader (`MemoryAccess/LiveMemoryReader.cs`)

The singleton coordinator that manages the emulator connection lifecycle.

### Connection Flow

```
LiveMemoryReader.Start()
  → Task.Run(MonitorEmulatorAsync)
    → Loop every 1 second:
      → Process.GetProcessesByName(emulatorProcessName)
      → If process found and not attached:
        → Attach(process)
          → Create ProcessMemory + PsxRam
          → Create all stat readers
          → Call UpdateData() on each (initial read)
        → Set Connected = true (triggers EmulatorLinkEventHub)
      → If process lost:
        → Set Connected = false
```

### Key Properties

```csharp
public ParameterStats ParameterStats { get; private set; } = ParameterStats.Empty;
public ConditionStats ConditionStats { get; private set; } = ConditionStats.Empty;
public ProfileStats ProfileStats { get; private set; } = ProfileStats.Empty;
public CareStats CareStats { get; private set; } = CareStats.Empty;
public TechniqueStats TechniqueStats { get; private set; } = TechniqueStats.Empty;
public HistoricEvolutions HistoricEvolutions { get; private set; } = HistoricEvolutions.Empty;
```

All properties default to `Empty` instances (Null Object pattern) when not connected.

### Emulator Process Name

Default: `"duckstation-qt-x64-ReleaseLTCG"`
Can be changed via `EmulatorLinkEventHub.EmulatorProcessNameChangedObservable` — triggers Stop→Start cycle.

---

## Adding a New Memory Value Reader

### Step-by-step:

1. **Find the memory offset** — use a cheat table (see `DW1_NTSC.CT`) or memory scanner to find the PSX RAM offset for the value.

2. **Create the reader class:**
   ```csharp
   // MemoryAccess/MemoryValues/{Category}/NewStats.cs
   using MemoryAccess.Core;
   using Shared.Services.Events;

   namespace MemoryAccess.MemoryValues.{Category};

   public sealed class NewStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
   {
       private const int MY_VALUE_OFFSET = 0x00XXXXXX;

       private NewStats() : this(ProcessMemory.Empty, PsxRam.Empty) { }

       public static NewStats Empty { get; } = new();

       public short MyValue { get; private set; }

       protected override void OnUpdateData()
       {
           MyValue = mem.ReadInt16(ram.A(MY_VALUE_OFFSET));
           // Signal to EventHub:
           EmulatorLinkEventHub.SignalNewStatsSynchronized(); // (add this signal first)
       }
   }
   ```

3. **Add EventHub signal** in `Shared/Services/Events/EmulatorLinkEventHub.cs`:
   ```csharp
   private static readonly Subject<Unit> _newStatsSynchronizedSubject = new();
   public static IObservable<Unit> NewStatsSynchronizedObservable => _newStatsSynchronizedSubject.AsObservable();
   public static void SignalNewStatsSynchronized() => _newStatsSynchronizedSubject.OnNext(Unit.Default);
   ```

4. **Wire up in LiveMemoryReader:**
   ```csharp
   // In Attach(Process proc):
   NewStats = new NewStats(mem, ram);
   NewStats.UpdateData();

   // Add property:
   public NewStats NewStats { get; private set; } = NewStats.Empty;
   ```

---

## Memory Offset Reference

All offsets are relative to the PSX RAM base. Values are read as `Int16` (2 bytes) unless otherwise noted.

### Digimon Parameter Stats

| Stat | Offset | Type |
|------|--------|------|
| Offense | `0x001557E0` | Int16 |
| Defense | `0x001557E2` | Int16 |
| Speed | `0x001557E4` | Int16 |
| Brains | `0x001557E6` | Int16 |
| HP | `0x001557F0` | Int16 |
| MP | `0x001557F2` | Int16 |
| Current HP | `0x001557F4` | Int16 |
| Current MP | `0x001557F6` | Int16 |
| Lives | `0x00155824` | Byte |

### Historic Evolution Flags

9 bytes at offsets `0x001BE00D` through `0x001BE015`. Each bit represents whether a specific Digimon has been previously evolved. See `HistoricEvolutions.cs` for the complete bit mapping.

---

## Cheat Table

`DW1_NTSC.CT` is included in the MemoryAccess project and copied to output. This is a Cheat Engine table containing known memory addresses for the NTSC version of Digimon World 1.

---

## Troubleshooting

### Common Issues

1. **"PSX RAM base not found"** — The byte signature doesn't match the DuckStation build. The signature may need updating if the emulator version changes.

2. **Partial reads / corrupted values** — The emulator process may have exited or memory layout changed. The polling loop handles this by catching exceptions and retrying.

3. **Process not found** — The emulator process name doesn't match `_emulatorProcessName`. Users can configure this via the emulator link settings.

4. **Thread safety** — `LiveMemoryReader.Connected` setter signals via EventHub, which may be consumed on different threads. UI consumers must use `ObserveOn(SynchronizationContext.Current!)`.

