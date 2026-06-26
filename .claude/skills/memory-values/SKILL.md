---
name: memory-values
description: How to add or modify a DuckStation game-memory reader in the MemoryAccess project of this Digimon World app (MemoryValueSyncBase subclasses, the Empty Null Object requirement, LiveMemoryReader wiring, read-on-poll/write-on-set, EventHub signalling, bit flags). Use when exposing a new game stat/value or changing how memory is read or written.
---

# Memory access (MemoryAccess project)

`LiveMemoryReader.Instance` monitors for the emulator process; `PsxRam` locates the PSX RAM base by byte-signature scan; `ProcessMemory` wraps `ReadProcessMemory`/`WriteProcessMemory`. Reads happen on the **1-second poll**; writes happen **when a value property's setter is assigned** (e.g. a cheat-sheet edit persists straight into game RAM).

## Adding a reader
1. Create a class extending `MemoryValueSyncBase` (auto-polls every 1s while connected). Offsets as `UPPER_SNAKE_CASE` consts.
2. Each value = a property whose getter returns the cached field and whose setter writes to RAM via `mem.WriteInt16/Int32/Byte(ram.A(OFFSET), value)`.
3. Override `OnUpdateData()` to read all fields, then signal the relevant EventHub (`EmulatorLinkEventHub.SignalDigimon…Synchronized()`).
4. **Required:** provide an `Empty` Null Object — a private parameterless ctor chaining `this(ProcessMemory.Empty, PsxRam.Empty)` and a `public static X Empty { get; } = new();` — and add a default for it in `LiveMemoryReader`. This is what makes reads/writes safe no-ops while disconnected.

## Bit flags
For a packed flag byte, cache the byte and expose `bool` properties via helpers:
```csharp
private bool HasBit(int bit) => (_flags & (1 << bit)) != 0;
private void SetFlag(int bit, bool value)
{
    _flags = (byte)(value ? _flags | (1 << bit) : _flags & ~(1 << bit));
    mem.WriteByte(ram.A(FLAGS_OFFSET), _flags);
}
```
Read-modify-write preserves the other bits. (Cheat Engine `BitStart` N = bit index N, i.e. mask `1 << N`.)

## Reference
`DW1_NTSC.CT` (bundled Cheat Engine table) is the source of addresses/bit positions — grep it for a value before assuming one doesn't exist.
