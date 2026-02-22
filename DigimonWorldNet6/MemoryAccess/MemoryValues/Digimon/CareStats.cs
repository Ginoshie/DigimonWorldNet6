using MemoryAccess.Core;
using MemoryAccess.MemoryValues.Evolution;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Digimon;

public class CareStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int POOP_LEVEL_OFFSET = 0x00138478;
    private const int POOPING_TIMER_OFFSET = 0x00138480;
    private const int TIREDNESS_OFFSET = 0x00138482;
    private const int ENERGY_LEVEL_OFFSET = 0x0013849C;
    private const int HUNGRY_TIMER_OFFSET = 0x0013849E;
    private const int STARVATION_TIMER_OFFSET = 0x001384A0;
    private const int LIFESPAN_OFFSET = 0x001384A8;

    private CareStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static CareStats Empty { get; } = new();

    public int PoopLevel { get; set; }

    public int PoopingTimer { get; set; }

    public int Tiredness { get; set; }

    public int EnergyLevel { get; set; }

    public int HungryTimer { get; set; }

    public int StarvationTimer { get; set; }

    public int Lifespan { get; set; }

    protected override void UpdateData()
    {
        PoopLevel = mem.ReadInt16(ram.A(POOP_LEVEL_OFFSET));
        PoopingTimer = mem.ReadInt16(ram.A(POOPING_TIMER_OFFSET));
        Tiredness = mem.ReadInt16(ram.A(TIREDNESS_OFFSET));
        EnergyLevel = mem.ReadInt16(ram.A(ENERGY_LEVEL_OFFSET));
        HungryTimer = mem.ReadInt16(ram.A(HUNGRY_TIMER_OFFSET));
        StarvationTimer = mem.ReadInt16(ram.A(STARVATION_TIMER_OFFSET));
        Lifespan = mem.ReadInt16(ram.A(LIFESPAN_OFFSET));

        EmulatorLinkEventHub.SignalDigimonCareStatsSynchronized();
    }
}