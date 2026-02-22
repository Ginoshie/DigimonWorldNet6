using MemoryAccess.Core;
using MemoryAccess.MemoryValues.Evolution;
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

    private ParameterStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static ParameterStats Empty { get; } = new();

    public short Offense { get; private set; }

    public short Defense { get; private set; }

    public short Speed { get; private set; }

    public short Brains { get; private set; }

    public short HP { get; private set; }

    public short MP { get; private set; }

    public short CurrentHP { get; private set; }

    public short CurrentMP { get; private set; }

    public byte Lives { get; private set; }

    protected override void UpdateData()
    {
        Offense = mem.ReadInt16(ram.A(OFFENSE_OFFSET));
        Defense = mem.ReadInt16(ram.A(DEFENSE_OFFSET));
        Speed = mem.ReadInt16(ram.A(SPEED_OFFSET));
        Brains = mem.ReadInt16(ram.A(BRAINS_OFFSET));
        HP = mem.ReadInt16(ram.A(HP_OFFSET));
        MP = mem.ReadInt16(ram.A(MP_OFFSET));
        CurrentHP = mem.ReadInt16(ram.A(CURRENT_HP_OFFSET));
        CurrentMP = mem.ReadInt16(ram.A(CURRENT_MP_OFFSET));
        Lives = mem.ReadByte(ram.A(LIVES_OFFSET));

        EmulatorLinkEventHub.SignalDigimonParameterStatsSynchronized();
    }
}