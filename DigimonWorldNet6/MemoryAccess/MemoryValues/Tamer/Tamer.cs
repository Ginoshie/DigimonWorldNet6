using MemoryAccess.Core;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Tamer;

public class Tamer(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int TAMER_LEVEL_OFFSET = 0x001557A4;
    private const int BITS_OFFSET = 0x00134EB8;
    private const int MERIT_POINTS_OFFSET = 0x00134FC4;

    private int _tamerLevel;
    private int _bits;
    private int _meritPoints;

    private Tamer() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static Tamer Empty { get; } = new();

    public int TamerLevel
    {
        get => _tamerLevel;
        set
        {
            _tamerLevel = value;
            mem.WriteByte(ram.A(TAMER_LEVEL_OFFSET), (byte)value);
        }
    }

    public int Bits
    {
        get => _bits;
        set
        {
            _bits = value;
            mem.WriteInt32(ram.A(BITS_OFFSET), value);
        }
    }

    public int MeritPoints
    {
        get => _meritPoints;
        set
        {
            _meritPoints = value;
            mem.WriteInt16(ram.A(MERIT_POINTS_OFFSET), (short)value);
        }
    }

    protected override void OnUpdateData()
    {
        _tamerLevel = mem.ReadByte(ram.A(TAMER_LEVEL_OFFSET));
        _bits = mem.ReadInt32(ram.A(BITS_OFFSET));
        _meritPoints = mem.ReadInt16(ram.A(MERIT_POINTS_OFFSET));

        EmulatorLinkEventHub.SignalDigimonCareStatsSynchronized();
    }
}
