using MemoryAccess.Core;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Technical;

public class Technical(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int CURRENT_RNG_OFFSET = 0x00009010;
    private const int AGE_IN_DAYS_OFFSET = 0x001384AA;
    private const int EVOLUTION_AGE_OFFSET = 0x001384B6;
    private const int EVOLVE_TRIGGER_OFFSET = 0x00134E50;

    private uint _currentRng;
    private int _evolveTrigger;

    private Technical() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static Technical Empty { get; } = new();

    public uint CurrentRng
    {
        get => _currentRng;
        set
        {
            _currentRng = value;
            mem.WriteInt32(ram.A(CURRENT_RNG_OFFSET), value);
        }
    }

    public int EvolveTrigger
    {
        get => _evolveTrigger;
        set
        {
            _evolveTrigger = value;
            mem.WriteInt16(ram.A(EVOLVE_TRIGGER_OFFSET), unchecked((short)value));
        }
    }

    public int AgeInDays { get; private set; }

    public int EvolutionAgeInHours { get; private set; }

    protected override void OnUpdateData()
    {
        _currentRng = (uint)mem.ReadInt32(ram.A(CURRENT_RNG_OFFSET));
        AgeInDays = mem.ReadInt16(ram.A(AGE_IN_DAYS_OFFSET));
        EvolutionAgeInHours = mem.ReadInt16(ram.A(EVOLUTION_AGE_OFFSET));
        _evolveTrigger = (ushort)mem.ReadInt16(ram.A(EVOLVE_TRIGGER_OFFSET));

        EmulatorLinkEventHub.SignalDigimonCareStatsSynchronized();
    }
}
