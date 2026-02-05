using MemoryAccess.Core;

namespace MemoryAccess.MemoryValues;

public sealed class DigimonConditionStats
{
    private readonly ProcessMemory _mem;
    private readonly PsxRam _ram;

    private DigimonConditionStats()
    {
        _mem = ProcessMemory.Empty;
        _ram = PsxRam.Empty;
    }
    
    public DigimonConditionStats(ProcessMemory mem, PsxRam ram)
    {
        _mem = mem;
        _ram = ram;
    }
    
    public static DigimonConditionStats Empty { get; } = new();

    public bool Sleepy => (Flags & 1) != 0;
    public bool Tired => (Flags & 2) != 0;
    public bool Hungry => (Flags & 4) != 0;
    public bool Poop => (Flags & 8) != 0;
    public bool Unhappy => (Flags & 16) != 0;
    public bool Injured => (Flags & 32) != 0;
    public bool Sick => (Flags & 64) != 0;

    public short Tiredness => _mem.ReadInt16(_ram.A(0x00138482));

    public short Happiness => _mem.ReadInt16(_ram.A(0x0013848A));

    public short Discipline => _mem.ReadInt16(_ram.A(0x00138488));
    
    public short CareMistakes => _mem.ReadInt16(_ram.A(0x001384B2));
    
    public short Battles => _mem.ReadInt16(_ram.A(0x001384B4));

    private byte Flags => _mem.ReadByte(_ram.A(0x00138460));
}