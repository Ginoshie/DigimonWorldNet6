using MemoryAccess.Core;

namespace MemoryAccess.MemoryValues;

public sealed class DigimonParameterStats(ProcessMemory mem, PsxRam ram)
{
    private DigimonParameterStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static DigimonParameterStats Empty { get; } = new();

    public int Offense => mem.ReadInt16(ram.A(0x001557E0));

    public int Defense => mem.ReadInt16(ram.A(0x001557E2));

    public int Speed => mem.ReadInt16(ram.A(0x001557E4));

    public int Brains => mem.ReadInt16(ram.A(0x001557E6));

    public int HP => mem.ReadInt16(ram.A(0x001557F0));

    public int MP => mem.ReadInt16(ram.A(0x001557F2));

    public int CurrentHP => mem.ReadInt16(ram.A(0x001557F4));

    public int CurrentMP => mem.ReadInt16(ram.A(0x001557F6));

    public byte Lives => mem.ReadByte(ram.A(0x00155824));
}
