using MemoryAccess.Core;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Digimon;

public sealed class ConditionStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int TIREDNESS_OFFSET = 0x00138482;
    private const int HAPPINESS_OFFSET = 0x0013848A;
    private const int DISCIPLINE_OFFSET = 0x00138488;
    private const int CARE_MISTAKES_OFFSET = 0x001384B2;
    private const int BATTLES_OFFSET = 0x001384B4;
    private const int FLAGS_OFFSET = 0x00138460;

    private short _tiredness;
    private short _happiness;
    private short _discipline;
    private short _careMistakes;
    private short _battles;
    private byte _flags;

    private ConditionStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static ConditionStats Empty { get; } = new();

    public short Tiredness
    {
        get => _tiredness;
        set
        {
            _tiredness = value;
            mem.WriteInt16(ram.A(TIREDNESS_OFFSET), value);
        }
    }

    public short Happiness
    {
        get => _happiness;
        set
        {
            _happiness = value;
            mem.WriteInt16(ram.A(HAPPINESS_OFFSET), value);
        }
    }

    public short Discipline
    {
        get => _discipline;
        set
        {
            _discipline = value;
            mem.WriteInt16(ram.A(DISCIPLINE_OFFSET), value);
        }
    }

    public short CareMistakes
    {
        get => _careMistakes;
        set
        {
            _careMistakes = value;
            mem.WriteInt16(ram.A(CARE_MISTAKES_OFFSET), value);
        }
    }

    public short Battles
    {
        get => _battles;
        set
        {
            _battles = value;
            mem.WriteInt16(ram.A(BATTLES_OFFSET), value);
        }
    }

    public bool Sleepy
    {
        get => HasBit(0);
        set => SetFlag(0, value);
    }

    public bool Tired
    {
        get => HasBit(1);
        set => SetFlag(1, value);
    }

    public bool Hungry
    {
        get => HasBit(2);
        set => SetFlag(2, value);
    }

    public bool Poop
    {
        get => HasBit(3);
        set => SetFlag(3, value);
    }

    public bool Unhappy
    {
        get => HasBit(4);
        set => SetFlag(4, value);
    }

    public bool Injured
    {
        get => HasBit(5);
        set => SetFlag(5, value);
    }

    public bool Sick
    {
        get => HasBit(6);
        set => SetFlag(6, value);
    }

    protected override void OnUpdateData()
    {
        _tiredness = mem.ReadInt16(ram.A(TIREDNESS_OFFSET));
        _happiness = mem.ReadInt16(ram.A(HAPPINESS_OFFSET));
        _discipline = mem.ReadInt16(ram.A(DISCIPLINE_OFFSET));
        _careMistakes = mem.ReadInt16(ram.A(CARE_MISTAKES_OFFSET));
        _battles = mem.ReadInt16(ram.A(BATTLES_OFFSET));
        _flags = mem.ReadByte(ram.A(FLAGS_OFFSET));

        EmulatorLinkEventHub.SignalDigimonConditionStatsSynchronized();
    }

    private bool HasBit(int bit)
        => (_flags & (1 << bit)) != 0;

    private void SetFlag(int bit, bool value)
    {
        _flags = (byte)(value ? _flags | (1 << bit) : _flags & ~(1 << bit));
        mem.WriteByte(ram.A(FLAGS_OFFSET), _flags);
    }
}