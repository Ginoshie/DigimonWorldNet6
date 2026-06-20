using MemoryAccess.Core;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Digimon;

public sealed class ParameterStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int OFFENSE_OFFSET = 0x001557E0;
    private const int DEFENSE_OFFSET = 0x001557E2;
    private const int SPEED_OFFSET = 0x001557E4;
    private const int BRAINS_OFFSET = 0x001557E6;
    private const int HP_OFFSET = 0x001557F0;
    private const int MP_OFFSET = 0x001557F2;
    private const int CURRENT_HP_OFFSET = 0x001557F4;
    private const int CURRENT_MP_OFFSET = 0x001557F6;
    private const int LIVES_OFFSET = 0x00155824;

    private short _offense;
    private short _defense;
    private short _speed;
    private short _brains;
    private short _hp;
    private short _mp;
    private short _currentHp;
    private short _currentMp;

    private ParameterStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static ParameterStats Empty { get; } = new();

    public short Offense
    {
        get => _offense;
        set
        {
            _offense = value;
            mem.WriteInt16(ram.A(OFFENSE_OFFSET), value);
        }
    }

    public short Defense
    {
        get => _defense;
        set
        {
            _defense = value;
            mem.WriteInt16(ram.A(DEFENSE_OFFSET), value);
        }
    }

    public short Speed
    {
        get => _speed;
        set
        {
            _speed = value;
            mem.WriteInt16(ram.A(SPEED_OFFSET), value);
        }
    }

    public short Brains
    {
        get => _brains;
        set
        {
            _brains = value;
            mem.WriteInt16(ram.A(BRAINS_OFFSET), value);
        }
    }

    public short Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            mem.WriteInt16(ram.A(HP_OFFSET), value);
        }
    }

    public short Mp
    {
        get => _mp;
        set
        {
            _mp = value;
            mem.WriteInt16(ram.A(MP_OFFSET), value);
        }
    }

    public short CurrentHp
    {
        get => _currentHp;
        set
        {
            _currentHp = value;
            mem.WriteInt16(ram.A(CURRENT_HP_OFFSET), value);
        }
    }

    public short CurrentMp
    {
        get => _currentMp;
        set
        {
            _currentMp = value;
            mem.WriteInt16(ram.A(CURRENT_MP_OFFSET), value);
        }
    }

    public byte Lives { get; private set; }

    protected override void OnUpdateData()
    {
        _offense = mem.ReadInt16(ram.A(OFFENSE_OFFSET));
        _defense = mem.ReadInt16(ram.A(DEFENSE_OFFSET));
        _speed = mem.ReadInt16(ram.A(SPEED_OFFSET));
        _brains = mem.ReadInt16(ram.A(BRAINS_OFFSET));
        _hp = mem.ReadInt16(ram.A(HP_OFFSET));
        _mp = mem.ReadInt16(ram.A(MP_OFFSET));
        _currentHp = mem.ReadInt16(ram.A(CURRENT_HP_OFFSET));
        _currentMp = mem.ReadInt16(ram.A(CURRENT_MP_OFFSET));
        Lives = mem.ReadByte(ram.A(LIVES_OFFSET));

        EmulatorLinkEventHub.SignalDigimonParameterStatsSynchronized();
    }
}