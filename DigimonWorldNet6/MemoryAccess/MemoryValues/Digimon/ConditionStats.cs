using MemoryAccess.Core;
using MemoryAccess.MemoryValues.Evolution;
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

    private ConditionStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static ConditionStats Empty { get; } = new();

    public short Tiredness { get; set; }

    public short Happiness { get; set; }

    public short Discipline { get; set; }

    public short CareMistakes { get; set; }

    public short Battles { get; set; }

    public bool Sleepy => (Flags & 1) != 0;
    public bool Tired => (Flags & 2) != 0;
    public bool Hungry => (Flags & 4) != 0;
    public bool Poop => (Flags & 8) != 0;
    public bool Unhappy => (Flags & 16) != 0;
    public bool Injured => (Flags & 32) != 0;
    public bool Sick => (Flags & 64) != 0;

    protected override void UpdateData()
    {
        Tiredness = mem.ReadInt16(ram.A(TIREDNESS_OFFSET));
        Happiness = mem.ReadInt16(ram.A(HAPPINESS_OFFSET));
        Discipline = mem.ReadInt16(ram.A(DISCIPLINE_OFFSET));
        CareMistakes = mem.ReadInt16(ram.A(CARE_MISTAKES_OFFSET));
        Battles = mem.ReadInt16(ram.A(BATTLES_OFFSET));
        Flags = mem.ReadByte(ram.A(FLAGS_OFFSET));

        EmulatorLinkEventHub.SignalDigimonConditionStatsSynchronized();
    }

    private byte Flags { get; set; }
}