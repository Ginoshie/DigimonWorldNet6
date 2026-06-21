using MemoryAccess.Core;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Technical;

public class Technical(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int CURRENT_RNG_OFFSET = 0x00009010;
    private const int AGE_IN_DAYS_OFFSET = 0x001384AA;
    private const int EVOLUTION_AGE_OFFSET = 0x001384B6;
    private const int EVOLVE_TRIGGER_OFFSET = 0x00134E50;
    private const int UPGRADE_COUNTER_HP_OFFSET = 0x001384C6;
    private const int UPGRADE_COUNTER_MP_OFFSET = 0x001384C8;
    private const int UPGRADE_COUNTER_OFFENSE_OFFSET = 0x001384CA;
    private const int UPGRADE_COUNTER_DEFENSE_OFFSET = 0x001384CE;
    private const int UPGRADE_COUNTER_SPEED_OFFSET = 0x001384D0;

    private uint _currentRng;
    private int _evolveTrigger;
    private int _ageInDays;
    private int _evolutionAgeInHours;
    private int _upgradeCounterHp;
    private int _upgradeCounterMp;
    private int _upgradeCounterOffense;
    private int _upgradeCounterDefense;
    private int _upgradeCounterSpeed;

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

    public int AgeInDays
    {
        get => _ageInDays;
        set
        {
            _ageInDays = value;
            mem.WriteInt16(ram.A(AGE_IN_DAYS_OFFSET), unchecked((short)value));
        }
    }

    public int EvolutionAgeInHours
    {
        get => _evolutionAgeInHours;
        set
        {
            _evolutionAgeInHours = value;
            mem.WriteInt16(ram.A(EVOLUTION_AGE_OFFSET), unchecked((short)value));
        }
    }

    public int UpgradeCounterHp
    {
        get => _upgradeCounterHp;
        set
        {
            _upgradeCounterHp = value;
            mem.WriteInt16(ram.A(UPGRADE_COUNTER_HP_OFFSET), unchecked((short)value));
        }
    }

    public int UpgradeCounterMp
    {
        get => _upgradeCounterMp;
        set
        {
            _upgradeCounterMp = value;
            mem.WriteInt16(ram.A(UPGRADE_COUNTER_MP_OFFSET), unchecked((short)value));
        }
    }

    public int UpgradeCounterOffense
    {
        get => _upgradeCounterOffense;
        set
        {
            _upgradeCounterOffense = value;
            mem.WriteInt16(ram.A(UPGRADE_COUNTER_OFFENSE_OFFSET), unchecked((short)value));
        }
    }

    public int UpgradeCounterDefense
    {
        get => _upgradeCounterDefense;
        set
        {
            _upgradeCounterDefense = value;
            mem.WriteInt16(ram.A(UPGRADE_COUNTER_DEFENSE_OFFSET), unchecked((short)value));
        }
    }

    public int UpgradeCounterSpeed
    {
        get => _upgradeCounterSpeed;
        set
        {
            _upgradeCounterSpeed = value;
            mem.WriteInt16(ram.A(UPGRADE_COUNTER_SPEED_OFFSET), unchecked((short)value));
        }
    }

    protected override void OnUpdateData()
    {
        _currentRng = (uint)mem.ReadInt32(ram.A(CURRENT_RNG_OFFSET));
        _ageInDays = mem.ReadInt16(ram.A(AGE_IN_DAYS_OFFSET));
        _evolutionAgeInHours = mem.ReadInt16(ram.A(EVOLUTION_AGE_OFFSET));
        _evolveTrigger = (ushort)mem.ReadInt16(ram.A(EVOLVE_TRIGGER_OFFSET));
        _upgradeCounterHp = mem.ReadInt16(ram.A(UPGRADE_COUNTER_HP_OFFSET));
        _upgradeCounterMp = mem.ReadInt16(ram.A(UPGRADE_COUNTER_MP_OFFSET));
        _upgradeCounterOffense = mem.ReadInt16(ram.A(UPGRADE_COUNTER_OFFENSE_OFFSET));
        _upgradeCounterDefense = mem.ReadInt16(ram.A(UPGRADE_COUNTER_DEFENSE_OFFSET));
        _upgradeCounterSpeed = mem.ReadInt16(ram.A(UPGRADE_COUNTER_SPEED_OFFSET));

        EmulatorLinkEventHub.SignalDigimonCareStatsSynchronized();
    }
}
