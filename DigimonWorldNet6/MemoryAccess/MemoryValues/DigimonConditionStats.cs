using MemoryAccess.Core;

namespace MemoryAccess.MemoryValues;

public sealed class DigimonConditionStats(ProcessMemory mem, PsxRam ram)
{
    private DigimonConditionStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static DigimonConditionStats Empty { get; } = new();

    public bool Sleepy => (Flags & 1) != 0;
    public bool Tired => (Flags & 2) != 0;
    public bool Hungry => (Flags & 4) != 0;
    public bool Poop => (Flags & 8) != 0;
    public bool Unhappy => (Flags & 16) != 0;
    public bool Injured => (Flags & 32) != 0;
    public bool Sick => (Flags & 64) != 0;

    public short Tiredness => mem.ReadInt16(ram.A(0x00138482));

    public short Happiness => mem.ReadInt16(ram.A(0x0013848A));

    public short Discipline => mem.ReadInt16(ram.A(0x00138488));
    
    public short CareMistakes => mem.ReadInt16(ram.A(0x001384B2));
    
    public short Battles => mem.ReadInt16(ram.A(0x001384B4));

    private byte Flags => mem.ReadByte(ram.A(0x00138460));
}