using System.Numerics;
using MemoryAccess.Core;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Digimon;

public sealed class TechniqueStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    // ===== Technique flag addresses =====
    private const int FIRE_ADDR = 0x00155800;
    private const int AIR_ADDR = 0x00155801;
    private const int ICE_ADDR = 0x00155802;
    private const int MECH_ADDR = 0x00155803;
    private const int EARTH_ADDR = 0x00155804;
    private const int BATTLE_ADDR = 0x00155805;
    private const int FILTH_ADDR = 0x00155806;
    private const int FILTH_ULTIMATE_ADDR = 0x00155807;

    private TechniqueStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static TechniqueStats Empty { get; } = new();

    private byte FireFlags { get; set; }
    private byte AirFlags { get; set; }
    private byte IceFlags { get; set; }
    private byte MechFlags { get; set; }
    private byte EarthFlags { get; set; }
    private byte BattleFlags { get; set; }
    private byte FilthFlags { get; set; }
    private byte FilthUltimateFlags { get; set; }

    public int LearnedTechniqueCount()
    {
        return BitOperations.PopCount(FireFlags)
               + BitOperations.PopCount(AirFlags)
               + BitOperations.PopCount(IceFlags)
               + BitOperations.PopCount(MechFlags)
               + BitOperations.PopCount(EarthFlags)
               + BitOperations.PopCount(BattleFlags);
    }

    // ===== Fire attacks =====
    public bool FireTower
    {
        get => HasBit(FireFlags, 0);
        set => SetFire(0, value);
    }

    public bool ProminenceBeam
    {
        get => HasBit(FireFlags, 1);
        set => SetFire(1, value);
    }

    public bool SpitFire
    {
        get => HasBit(FireFlags, 2);
        set => SetFire(2, value);
    }

    public bool RedInferno
    {
        get => HasBit(FireFlags, 3);
        set => SetFire(3, value);
    }

    public bool MagmaBomb
    {
        get => HasBit(FireFlags, 4);
        set => SetFire(4, value);
    }

    public bool HeatLaser
    {
        get => HasBit(FireFlags, 5);
        set => SetFire(5, value);
    }

    public bool InfinityBurn
    {
        get => HasBit(FireFlags, 6);
        set => SetFire(6, value);
    }

    public bool Meltdown
    {
        get => HasBit(FireFlags, 7);
        set => SetFire(7, value);
    }

    // ===== Battle attacks =====
    public bool Tremar
    {
        get => HasBit(BattleFlags, 0);
        set => SetBattle(0, value);
    }

    public bool MuscleCharge
    {
        get => HasBit(BattleFlags, 1);
        set => SetBattle(1, value);
    }

    public bool WarCry
    {
        get => HasBit(BattleFlags, 2);
        set => SetBattle(2, value);
    }

    public bool SonicJab
    {
        get => HasBit(BattleFlags, 3);
        set => SetBattle(3, value);
    }

    public bool DynamiteKick
    {
        get => HasBit(BattleFlags, 4);
        set => SetBattle(4, value);
    }

    public bool Counter
    {
        get => HasBit(BattleFlags, 5);
        set => SetBattle(5, value);
    }

    public bool MegatonPunch
    {
        get => HasBit(BattleFlags, 6);
        set => SetBattle(6, value);
    }

    public bool BusterDive
    {
        get => HasBit(BattleFlags, 7);
        set => SetBattle(7, value);
    }

    // ===== Air attacks =====
    public bool ThunderJustice
    {
        get => HasBit(AirFlags, 0);
        set => SetAir(0, value);
    }

    public bool SpinningShot
    {
        get => HasBit(AirFlags, 1);
        set => SetAir(1, value);
    }

    public bool ElectricCloud
    {
        get => HasBit(AirFlags, 2);
        set => SetAir(2, value);
    }

    public bool MegaloSpark
    {
        get => HasBit(AirFlags, 3);
        set => SetAir(3, value);
    }

    public bool StaticElect
    {
        get => HasBit(AirFlags, 4);
        set => SetAir(4, value);
    }

    public bool WindCutter
    {
        get => HasBit(AirFlags, 5);
        set => SetAir(5, value);
    }

    public bool ConfusedStorm
    {
        get => HasBit(AirFlags, 6);
        set => SetAir(6, value);
    }

    public bool Hurricane
    {
        get => HasBit(AirFlags, 7);
        set => SetAir(7, value);
    }

    // ===== Earth attacks =====
    public bool PoisonPowder
    {
        get => HasBit(EarthFlags, 0);
        set => SetEarth(0, value);
    }

    public bool Bug
    {
        get => HasBit(EarthFlags, 1);
        set => SetEarth(1, value);
    }

    public bool MassMorph
    {
        get => HasBit(EarthFlags, 2);
        set => SetEarth(2, value);
    }

    public bool InsectPlague
    {
        get => HasBit(EarthFlags, 3);
        set => SetEarth(3, value);
    }

    public bool CharmPerfume
    {
        get => HasBit(EarthFlags, 4);
        set => SetEarth(4, value);
    }

    public bool PoisonClaw
    {
        get => HasBit(EarthFlags, 5);
        set => SetEarth(5, value);
    }

    public bool DangerSting
    {
        get => HasBit(EarthFlags, 6);
        set => SetEarth(6, value);
    }

    public bool GreenTrap
    {
        get => HasBit(EarthFlags, 7);
        set => SetEarth(7, value);
    }

    // ===== Ice attacks =====
    public bool GigaFreeze
    {
        get => HasBit(IceFlags, 0);
        set => SetIce(0, value);
    }

    public bool IceStatue
    {
        get => HasBit(IceFlags, 1);
        set => SetIce(1, value);
    }

    public bool WinterBlast
    {
        get => HasBit(IceFlags, 2);
        set => SetIce(2, value);
    }

    public bool IceNeedle
    {
        get => HasBit(IceFlags, 3);
        set => SetIce(3, value);
    }

    public bool WaterBlit
    {
        get => HasBit(IceFlags, 4);
        set => SetIce(4, value);
    }

    public bool AquaMagic
    {
        get => HasBit(IceFlags, 5);
        set => SetIce(5, value);
    }

    public bool AuroraFreeze
    {
        get => HasBit(IceFlags, 6);
        set => SetIce(6, value);
    }

    public bool TearDrop
    {
        get => HasBit(IceFlags, 7);
        set => SetIce(7, value);
    }

    // ===== Mech attacks =====
    public bool PowerCrane
    {
        get => HasBit(MechFlags, 0);
        set => SetMech(0, value);
    }

    public bool AllRangeBeam
    {
        get => HasBit(MechFlags, 1);
        set => SetMech(1, value);
    }

    public bool MetalSprinter
    {
        get => HasBit(MechFlags, 2);
        set => SetMech(2, value);
    }

    public bool PulseLaser
    {
        get => HasBit(MechFlags, 3);
        set => SetMech(3, value);
    }

    public bool DeleteProgram
    {
        get => HasBit(MechFlags, 4);
        set => SetMech(4, value);
    }

    public bool DgDimension
    {
        get => HasBit(MechFlags, 5);
        set => SetMech(5, value);
    }

    public bool FullPotential
    {
        get => HasBit(MechFlags, 6);
        set => SetMech(6, value);
    }

    public bool ReverseProgram
    {
        get => HasBit(MechFlags, 7);
        set => SetMech(7, value);
    }

    // ===== Filth attacks =====
    public bool OdorSpray
    {
        get => HasBit(FilthFlags, 1);
        set => SetFilth(1, value);
    }

    public bool PoopSpdToss
    {
        get => HasBit(FilthFlags, 2);
        set => SetFilth(2, value);
    }

    public bool BigPoopToss
    {
        get => HasBit(FilthFlags, 3);
        set => SetFilth(3, value);
    }

    public bool BigRndToss
    {
        get => HasBit(FilthFlags, 4);
        set => SetFilth(4, value);
    }

    public bool PoopRndToss
    {
        get => HasBit(FilthFlags, 5);
        set => SetFilth(5, value);
    }

    public bool RndSpdToss
    {
        get => HasBit(FilthFlags, 6);
        set => SetFilth(6, value);
    }

    public bool HorizontalKick
    {
        get => HasBit(FilthFlags, 7);
        set => SetFilth(7, value);
    }

    public bool UltimatePoopHell
    {
        get => HasBit(FilthUltimateFlags, 0);
        set => SetFilthUltimate(0, value);
    }

    protected override void OnUpdateData()
    {
        FireFlags = mem.ReadByte(ram.A(FIRE_ADDR));
        AirFlags = mem.ReadByte(ram.A(AIR_ADDR));
        IceFlags = mem.ReadByte(ram.A(ICE_ADDR));
        MechFlags = mem.ReadByte(ram.A(MECH_ADDR));
        EarthFlags = mem.ReadByte(ram.A(EARTH_ADDR));
        BattleFlags = mem.ReadByte(ram.A(BATTLE_ADDR));
        FilthFlags = mem.ReadByte(ram.A(FILTH_ADDR));
        FilthUltimateFlags = mem.ReadByte(ram.A(FILTH_ULTIMATE_ADDR));

        EmulatorLinkEventHub.SignalDigimonTechniqueStatsSynchronized();
    }

    private void SetFire(int bit, bool value)
    {
        FireFlags = WithBit(FireFlags, bit, value);
        mem.WriteByte(ram.A(FIRE_ADDR), FireFlags);
    }

    private void SetBattle(int bit, bool value)
    {
        BattleFlags = WithBit(BattleFlags, bit, value);
        mem.WriteByte(ram.A(BATTLE_ADDR), BattleFlags);
    }

    private void SetAir(int bit, bool value)
    {
        AirFlags = WithBit(AirFlags, bit, value);
        mem.WriteByte(ram.A(AIR_ADDR), AirFlags);
    }

    private void SetEarth(int bit, bool value)
    {
        EarthFlags = WithBit(EarthFlags, bit, value);
        mem.WriteByte(ram.A(EARTH_ADDR), EarthFlags);
    }

    private void SetIce(int bit, bool value)
    {
        IceFlags = WithBit(IceFlags, bit, value);
        mem.WriteByte(ram.A(ICE_ADDR), IceFlags);
    }

    private void SetMech(int bit, bool value)
    {
        MechFlags = WithBit(MechFlags, bit, value);
        mem.WriteByte(ram.A(MECH_ADDR), MechFlags);
    }

    private void SetFilth(int bit, bool value)
    {
        FilthFlags = WithBit(FilthFlags, bit, value);
        mem.WriteByte(ram.A(FILTH_ADDR), FilthFlags);
    }

    private void SetFilthUltimate(int bit, bool value)
    {
        FilthUltimateFlags = WithBit(FilthUltimateFlags, bit, value);
        mem.WriteByte(ram.A(FILTH_ULTIMATE_ADDR), FilthUltimateFlags);
    }

    private static bool HasBit(byte value, int bit)
        => (value & (1 << bit)) != 0;

    private static byte WithBit(byte value, int bit, bool set)
        => (byte)(set ? value | (1 << bit) : value & ~(1 << bit));
}
