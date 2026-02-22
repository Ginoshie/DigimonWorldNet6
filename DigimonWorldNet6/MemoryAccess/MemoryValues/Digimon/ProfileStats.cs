using System.Text;
using MemoryAccess.Core;
using MemoryAccess.MemoryValues.Evolution;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Digimon;

public sealed class ProfileStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int DIGIMON_TYPE_OFFSET = 0x001557A8;
    private const int WEIGHT_OFFSET = 0x001384A2;
    private const int NAME_OFFSET = 0x00155810;
    private const int NAME_LENGTH = 12;
    private const string DEFAULT_USER_DIGIMON_NAME = "Unknown";
    private const int SLEEPY_HOUR = 0x00138464;
    private const int SLEEPY_MINUTE = 0x00138466;
    private const int WAKEUP_HOUR = 0x00138468;
    private const int WAKEUP_MINUTE = 0x0013846A;
    private const int STANDARD_AWAKE_TIME = 0x0013846C;
    private const int STANDARD_SLEEP_TIME = 0x0013846E;
    private const int POOP_LEVEL = 0x00138478;
    private const int VIRUS_BAR = 0x0013847E;
    private const int POOPING_TIMER = 0x00138480;
    private const int TIREDNESS = 0x00138482;
    private const int ENERGY_LEVEL = 0x0013849C;
    private const int HUNGRY_TIMER = 0x0013849E;
    private const int STARVATION_TIMER = 0x001384A0;
    private const int REMAINING_LIFESPAN_IN_HOURS = 0x001384A0;
    private const int AGE_IN_DAYS = 0x001384AA;
    private const int EVOLUTION_AGE_IN_HOURS = 0x001384B6;
    private const int FIRST_SPECIAL = 0x12ced2;
    private const int DIGIMON_TYPE_MULTIPLIER = 0x34;
    private const int SECOND_TECHNIQUE_OFFSET = 1;
    private const int THIRD_TECHNIQUE_OFFSET = 2;
    private const int TYPE_OFFSET = -2;

    private ProfileStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public byte FirstSpecial { get; private set; }
    public byte SecondSpecial { get; private set; }
    public byte ThirdSpecial { get; private set; }
    public byte Type { get; private set; }
    public static ProfileStats Empty { get; } = new();

    public byte DigimonType { get; private set; }

    public short Weight { get; private set; }

    public string Name { get; private set; } = DEFAULT_USER_DIGIMON_NAME;

    public int SleepyHour { get; private set; }

    public int SleepyMinute { get; private set; }

    public int WakeUpHour { get; private set; }

    public int WakeUpMinute { get; private set; }

    public int StandardAwakeTime { get; private set; }

    public int StandardSleepTime { get; private set; }

    // 1 = 10 min ingame before needing to poop
    public int PoopLevel { get; private set; }

    public int VirusBar { get; private set; }

    public int PoopingTimer { get; private set; }

    public int Tiredness { get; private set; }

    public int EngergyLevel { get; private set; }

    public int HungryTimer { get; private set; }

    public int StarvationTimer { get; private set; }

    public int RemainingLifeSpanInHours { get; private set; }

    public int AgeInDays { get; private set; }

    public int EvolutionAgeInHours { get; private set; }

    protected override void UpdateData()
    {
        DigimonType = mem.ReadByte(ram.A(DIGIMON_TYPE_OFFSET));
        if (DigimonType != 0)
        {
            FirstSpecial = mem.ReadByte(ram.A(FIRST_SPECIAL+DigimonType*DIGIMON_TYPE_MULTIPLIER));
            SecondSpecial = mem.ReadByte(ram.A(FIRST_SPECIAL+DigimonType*DIGIMON_TYPE_MULTIPLIER+SECOND_TECHNIQUE_OFFSET));
            ThirdSpecial = mem.ReadByte(ram.A(FIRST_SPECIAL+DigimonType*DIGIMON_TYPE_MULTIPLIER+THIRD_TECHNIQUE_OFFSET));
            Type = mem.ReadByte(ram.A(FIRST_SPECIAL+DigimonType*DIGIMON_TYPE_MULTIPLIER+TYPE_OFFSET));
        }

        Weight = mem.ReadInt16(ram.A(WEIGHT_OFFSET));
        byte[] buffer = mem.ReadBytes(ram.A(NAME_OFFSET), NAME_LENGTH);
        Name = DecodeName(buffer);
        SleepyHour = mem.ReadInt16(ram.A(SLEEPY_HOUR));
        SleepyMinute = mem.ReadInt16(ram.A(SLEEPY_MINUTE));
        WakeUpHour = mem.ReadInt16(ram.A(WAKEUP_HOUR));
        WakeUpMinute = mem.ReadInt16(ram.A(WAKEUP_MINUTE));
        StandardAwakeTime = mem.ReadInt16(ram.A(STANDARD_AWAKE_TIME));
        StandardSleepTime = mem.ReadInt16(ram.A(STANDARD_SLEEP_TIME));
        PoopLevel = mem.ReadInt16(ram.A(POOP_LEVEL));
        VirusBar = mem.ReadInt16(ram.A(VIRUS_BAR));
        PoopingTimer = mem.ReadInt16(ram.A(POOPING_TIMER));
        Tiredness = mem.ReadInt16(ram.A(TIREDNESS));
        EngergyLevel = mem.ReadInt16(ram.A(ENERGY_LEVEL));
        HungryTimer = mem.ReadInt16(ram.A(HUNGRY_TIMER));
        StarvationTimer = mem.ReadInt16(ram.A(STARVATION_TIMER));
        RemainingLifeSpanInHours = mem.ReadInt16(ram.A(REMAINING_LIFESPAN_IN_HOURS));
        AgeInDays = mem.ReadInt16(ram.A(AGE_IN_DAYS));
        EvolutionAgeInHours = mem.ReadInt16(ram.A(EVOLUTION_AGE_IN_HOURS));

        EmulatorLinkEventHub.SignalDigimonProfileStatsSynchronized();
    }

    private static string DecodeName(byte[] buffer)
    {
        Encoding shiftJis = Encoding.GetEncoding(932);

        int terminatorIndex = Array.IndexOf(buffer, (byte)0);

        int length = terminatorIndex >= 0
            ? terminatorIndex
            : buffer.Length;

        return shiftJis
            .GetString(buffer, 0, length)
            .Normalize(NormalizationForm.FormKC);
    }
}