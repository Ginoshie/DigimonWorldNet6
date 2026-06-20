using MemoryAccess.Core;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Digimon;

public class CareStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int POOP_LEVEL_OFFSET = 0x00138478;
    private const int VIRUS_BAR_OFFSET = 0x0013847E;
    private const int POOPING_TIMER_OFFSET = 0x00138480;
    private const int ENERGY_LEVEL_OFFSET = 0x0013849C;
    private const int HUNGRY_TIMER_OFFSET = 0x0013849E;
    private const int STARVATION_TIMER_OFFSET = 0x001384A0;
    private const int LIFESPAN_OFFSET = 0x001384A8;

    private int _poopLevel;
    private int _virusBar;
    private int _poopingTimer;
    private int _energyLevel;
    private int _hungryTimer;
    private int _starvationTimer;
    private int _lifespan;

    private CareStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static CareStats Empty { get; } = new();

    public int PoopLevel
    {
        get => _poopLevel;
        set
        {
            _poopLevel = value;
            mem.WriteInt16(ram.A(POOP_LEVEL_OFFSET), (short)value);
        }
    }

    public int VirusBar
    {
        get => _virusBar;
        set
        {
            _virusBar = value;
            mem.WriteInt16(ram.A(VIRUS_BAR_OFFSET), (short)value);
        }
    }

    public int PoopingTimer
    {
        get => _poopingTimer;
        set
        {
            _poopingTimer = value;
            mem.WriteInt16(ram.A(POOPING_TIMER_OFFSET), (short)value);
        }
    }

    public int EnergyLevel
    {
        get => _energyLevel;
        set
        {
            _energyLevel = value;
            mem.WriteInt16(ram.A(ENERGY_LEVEL_OFFSET), (short)value);
        }
    }

    public int HungryTimer
    {
        get => _hungryTimer;
        set
        {
            _hungryTimer = value;
            mem.WriteInt16(ram.A(HUNGRY_TIMER_OFFSET), (short)value);
        }
    }

    public int StarvationTimer
    {
        get => _starvationTimer;
        set
        {
            _starvationTimer = value;
            mem.WriteInt16(ram.A(STARVATION_TIMER_OFFSET), (short)value);
        }
    }

    public int Lifespan
    {
        get => _lifespan;
        set
        {
            _lifespan = value;
            mem.WriteInt16(ram.A(LIFESPAN_OFFSET), (short)value);
        }
    }

    protected override void OnUpdateData()
    {
        _poopLevel = mem.ReadInt16(ram.A(POOP_LEVEL_OFFSET));
        _virusBar = mem.ReadInt16(ram.A(VIRUS_BAR_OFFSET));
        _poopingTimer = mem.ReadInt16(ram.A(POOPING_TIMER_OFFSET));
        _energyLevel = mem.ReadInt16(ram.A(ENERGY_LEVEL_OFFSET));
        _hungryTimer = mem.ReadInt16(ram.A(HUNGRY_TIMER_OFFSET));
        _starvationTimer = mem.ReadInt16(ram.A(STARVATION_TIMER_OFFSET));
        _lifespan = mem.ReadInt16(ram.A(LIFESPAN_OFFSET));

        EmulatorLinkEventHub.SignalDigimonCareStatsSynchronized();
    }
}