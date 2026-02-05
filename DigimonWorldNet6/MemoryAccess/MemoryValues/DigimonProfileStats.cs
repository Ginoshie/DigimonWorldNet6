using MemoryAccess.Core;

namespace MemoryAccess.MemoryValues;

public class DigimonProfileStats
{
    private readonly ProcessMemory _mem;
    private readonly PsxRam _ram;

    private DigimonProfileStats()
    {
        _mem = ProcessMemory.Empty;
        _ram = PsxRam.Empty;
    }

    public DigimonProfileStats(ProcessMemory mem, PsxRam ram)
    {
        _mem = mem;
        _ram = ram;
    }

    public static DigimonProfileStats Empty { get; } = new();

    public byte DigimonType => _mem.ReadByte(_ram.A(0x001557A8));

    public short Weight => _mem.ReadInt16(_ram.A(0x001384A2));
}