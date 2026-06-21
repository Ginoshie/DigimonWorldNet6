using MemoryAccess.Core;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Digimon;

public sealed class CombatStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int FINISHER_GOAL_OFFSET = 0x0013D658;
    private const int FINISHER_PROGRESS_OFFSET = 0x0013D65A;
    private const int POISON_TIMER_OFFSET = 0x0013D65C;
    private const int CONFUSED_TIMER_OFFSET = 0x0013D65E;
    private const int STUN_TIMER_OFFSET = 0x0013D660;
    private const int FLATTEN_TIMER_OFFSET = 0x0013D662;
    private const int FLATTEN_ATTACK_TIMER_OFFSET = 0x0013D664;
    private const int COOLDOWN_OFFSET = 0x0013D668;
    private const int DUMB_TIMER_OFFSET = 0x0013D66A;
    private const int STATUS_EFFECTS_OFFSET = 0x0013D674;

    private short _finisherGoal;
    private short _finisherProgress;
    private short _poisonTimer;
    private short _confusedTimer;
    private short _stunTimer;
    private short _flattenTimer;
    private short _flattenAttackTimer;
    private int _cooldown;
    private short _dumbTimer;
    private int _statusEffects;

    private CombatStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static CombatStats Empty { get; } = new();

    public short FinisherGoal
    {
        get => _finisherGoal;
        set
        {
            _finisherGoal = value;
            mem.WriteInt16(ram.A(FINISHER_GOAL_OFFSET), value);
        }
    }

    public short FinisherProgress
    {
        get => _finisherProgress;
        set
        {
            _finisherProgress = value;
            mem.WriteInt16(ram.A(FINISHER_PROGRESS_OFFSET), value);
        }
    }

    public short PoisonTimer
    {
        get => _poisonTimer;
        set
        {
            _poisonTimer = value;
            mem.WriteInt16(ram.A(POISON_TIMER_OFFSET), value);
        }
    }

    public short ConfusedTimer
    {
        get => _confusedTimer;
        set
        {
            _confusedTimer = value;
            mem.WriteInt16(ram.A(CONFUSED_TIMER_OFFSET), value);
        }
    }

    public short StunTimer
    {
        get => _stunTimer;
        set
        {
            _stunTimer = value;
            mem.WriteInt16(ram.A(STUN_TIMER_OFFSET), value);
        }
    }

    public short FlattenTimer
    {
        get => _flattenTimer;
        set
        {
            _flattenTimer = value;
            mem.WriteInt16(ram.A(FLATTEN_TIMER_OFFSET), value);
        }
    }

    public short FlattenAttackTimer
    {
        get => _flattenAttackTimer;
        set
        {
            _flattenAttackTimer = value;
            mem.WriteInt16(ram.A(FLATTEN_ATTACK_TIMER_OFFSET), value);
        }
    }

    public int Cooldown
    {
        get => _cooldown;
        set
        {
            _cooldown = value;
            mem.WriteByte(ram.A(COOLDOWN_OFFSET), (byte)value);
        }
    }

    public short DumbTimer
    {
        get => _dumbTimer;
        set
        {
            _dumbTimer = value;
            mem.WriteInt16(ram.A(DUMB_TIMER_OFFSET), value);
        }
    }

    public int StatusEffects
    {
        get => _statusEffects;
        set
        {
            _statusEffects = value;
            mem.WriteByte(ram.A(STATUS_EFFECTS_OFFSET), (byte)value);
        }
    }

    protected override void OnUpdateData()
    {
        _finisherGoal = mem.ReadInt16(ram.A(FINISHER_GOAL_OFFSET));
        _finisherProgress = mem.ReadInt16(ram.A(FINISHER_PROGRESS_OFFSET));
        _poisonTimer = mem.ReadInt16(ram.A(POISON_TIMER_OFFSET));
        _confusedTimer = mem.ReadInt16(ram.A(CONFUSED_TIMER_OFFSET));
        _stunTimer = mem.ReadInt16(ram.A(STUN_TIMER_OFFSET));
        _flattenTimer = mem.ReadInt16(ram.A(FLATTEN_TIMER_OFFSET));
        _flattenAttackTimer = mem.ReadInt16(ram.A(FLATTEN_ATTACK_TIMER_OFFSET));
        _cooldown = mem.ReadByte(ram.A(COOLDOWN_OFFSET));
        _dumbTimer = mem.ReadInt16(ram.A(DUMB_TIMER_OFFSET));
        _statusEffects = mem.ReadByte(ram.A(STATUS_EFFECTS_OFFSET));

        EmulatorLinkEventHub.SignalDigimonCombatStatsSynchronized();
    }
}
