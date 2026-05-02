using MemoryAccess.Core;

namespace MemoryAccess.MemoryValues.World;

public class WorldTime(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int YEAR_OFFSET = 0x00134F02;
    private const int DAY_OFFSET = 0x00134F04;
    private const int HOUR_OFFSET = 0x00134EBC;
    private const int MINUTE_OFFSET = 0x00134EBE;

    private WorldTime() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static WorldTime Empty { get; } = new();

    public int Year { get; private set; }

    public int Day { get; private set; }

    public int Hour { get; private set; }

    public int Minute { get; private set; }

    protected override void OnUpdateData()
    {
        Year = mem.ReadInt16(ram.A(YEAR_OFFSET));
        Day = mem.ReadInt16(ram.A(DAY_OFFSET));
        Hour = mem.ReadInt16(ram.A(HOUR_OFFSET));
        Minute = mem.ReadInt16(ram.A(MINUTE_OFFSET));
    }
}