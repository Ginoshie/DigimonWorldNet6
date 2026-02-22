using MemoryAccess.Core;
using MemoryAccess.MemoryValues.Evolution;

namespace MemoryAccess.MemoryValues;

public class EvolutionStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int AGE_IN_HOURS_FOR_EVOLVE_OFFSET = 0x001384B6;

    private EvolutionStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }
    
    public static EvolutionStats Empty { get; } = new();
    
    public int EvoAge { get; private set; }

    protected override void UpdateData()
    {
        EvoAge = mem.ReadInt16(ram.A(AGE_IN_HOURS_FOR_EVOLVE_OFFSET));
    }
}