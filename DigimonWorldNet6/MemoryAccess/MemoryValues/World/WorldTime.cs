using MemoryAccess.Core;

namespace MemoryAccess.MemoryValues.World;

public class WorldTime(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int YEAR_OFFSET = 0x00134F02;
    private const int DAY_OFFSET = 0x00134F04;
    private const int HOUR_OFFSET = 0x00134EBC;
    private const int MINUTE_OFFSET = 0x00134EBE;

    private int _year;
    private int _day;
    private int _hour;
    private int _minute;

    private WorldTime() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static WorldTime Empty { get; } = new();

    public int Year
    {
        get => _year;
        set
        {
            _year = value;
            mem.WriteInt16(ram.A(YEAR_OFFSET), (short)value);
        }
    }

    public int Day
    {
        get => _day;
        set
        {
            _day = value;
            mem.WriteInt16(ram.A(DAY_OFFSET), (short)value);
        }
    }

    public int Hour
    {
        get => _hour;
        set
        {
            _hour = value;
            mem.WriteInt16(ram.A(HOUR_OFFSET), (short)value);
        }
    }

    public int Minute
    {
        get => _minute;
        set
        {
            _minute = value;
            mem.WriteInt16(ram.A(MINUTE_OFFSET), (short)value);
        }
    }

    protected override void OnUpdateData()
    {
        _year = mem.ReadInt16(ram.A(YEAR_OFFSET));
        _day = mem.ReadInt16(ram.A(DAY_OFFSET));
        _hour = mem.ReadInt16(ram.A(HOUR_OFFSET));
        _minute = mem.ReadInt16(ram.A(MINUTE_OFFSET));
    }
}