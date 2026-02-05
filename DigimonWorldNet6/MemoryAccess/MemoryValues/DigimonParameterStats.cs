using MemoryAccess.Core;

namespace MemoryAccess.MemoryValues;

public sealed class DigimonParameterStats
{
    private readonly ProcessMemory _mem;
    private readonly PsxRam _ram;

    private DigimonParameterStats()
    {
        _mem = ProcessMemory.Empty;
        _ram = PsxRam.Empty;
    }
    
    public DigimonParameterStats(ProcessMemory mem, PsxRam ram)
    {
        _mem = mem; 
        _ram = ram;
    }

    public static DigimonParameterStats Empty { get; } = new();

    public int Offense => _mem.ReadInt16(_ram.A(0x001557E0));

    public int Defense => _mem.ReadInt16(_ram.A(0x001557E2));

    public int Speed => _mem.ReadInt16(_ram.A(0x001557E4));

    public int Brains => _mem.ReadInt16(_ram.A(0x001557E6));

    public int HP => _mem.ReadInt16(_ram.A(0x001557F0));

    public int MP => _mem.ReadInt16(_ram.A(0x001557F2));

    public int CurrentHP => _mem.ReadInt16(_ram.A(0x001557F4));

    public int CurrentMP => _mem.ReadInt16(_ram.A(0x001557F6));

    public byte Lives => _mem.ReadByte(_ram.A(0x00155824));
}
