using MemoryAccess.Core;

namespace MemoryAccess.MemoryValues;

public class DigimonProfileStats(ProcessMemory mem, PsxRam ram)
{
    private DigimonProfileStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static DigimonProfileStats Empty { get; } = new();

    public byte DigimonType => mem.ReadByte(ram.A(0x001557A8));

    public short Weight => mem.ReadInt16(ram.A(0x001384A2));
}