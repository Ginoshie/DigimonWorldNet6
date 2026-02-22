using System.Numerics;
using MemoryAccess.Core;
using MemoryAccess.MemoryValues.Evolution;
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
    public bool FireTower => HasBit(FireFlags, 0);
    public bool ProminenceBeam => HasBit(FireFlags, 1);
    public bool SpitFire => HasBit(FireFlags, 2);
    public bool RedInferno => HasBit(FireFlags, 3);
    public bool MagmaBomb => HasBit(FireFlags, 4);
    public bool HeatLaser => HasBit(FireFlags, 5);
    public bool InfinityBurn => HasBit(FireFlags, 6);
    public bool Meltdown => HasBit(FireFlags, 7);

    // ===== Battle attacks =====
    public bool Tremar => HasBit(BattleFlags, 0);
    public bool MuscleCharge => HasBit(BattleFlags, 1);
    public bool WarCry => HasBit(BattleFlags, 2);
    public bool SonicJab => HasBit(BattleFlags, 3);
    public bool DynamiteKick => HasBit(BattleFlags, 4);
    public bool Counter => HasBit(BattleFlags, 5);
    public bool MegatonPunch => HasBit(BattleFlags, 6);
    public bool BusterDive => HasBit(BattleFlags, 7);

    // ===== Air attacks =====
    public bool ThunderJustice => HasBit(AirFlags, 0);
    public bool SpinningShot => HasBit(AirFlags, 1);
    public bool ElectricCloud => HasBit(AirFlags, 2);
    public bool MegaloSpark => HasBit(AirFlags, 3);
    public bool StaticElect => HasBit(AirFlags, 4);
    public bool WindCutter => HasBit(AirFlags, 5);
    public bool ConfusedStorm => HasBit(AirFlags, 6);
    public bool Hurricane => HasBit(AirFlags, 7);

    // ===== Earth attacks =====
    public bool PoisonPowder => HasBit(EarthFlags, 0);
    public bool Bug => HasBit(EarthFlags, 1);
    public bool MassMorph => HasBit(EarthFlags, 2);
    public bool InsectPlague => HasBit(EarthFlags, 3);
    public bool CharmPerfume => HasBit(EarthFlags, 4);
    public bool PoisonClaw => HasBit(EarthFlags, 5);
    public bool DangerSting => HasBit(EarthFlags, 6);
    public bool GreenTrap => HasBit(EarthFlags, 7);

    // ===== Ice attacks =====
    public bool GigaFreeze => HasBit(IceFlags, 0);
    public bool IceStatue => HasBit(IceFlags, 1);
    public bool WinterBlast => HasBit(IceFlags, 2);
    public bool IceNeedle => HasBit(IceFlags, 3);
    public bool WaterBlit => HasBit(IceFlags, 4);
    public bool AquaMagic => HasBit(IceFlags, 5);
    public bool AuroraFreeze => HasBit(IceFlags, 6);
    public bool TearDrop => HasBit(IceFlags, 7);

    // ===== Mech attacks =====
    public bool PowerCrane => HasBit(MechFlags, 0);
    public bool AllRangeBeam => HasBit(MechFlags, 1);
    public bool MetalSprinter => HasBit(MechFlags, 2);
    public bool PulseLaser => HasBit(MechFlags, 3);
    public bool DeleteProgram => HasBit(MechFlags, 4);
    public bool DgDimension => HasBit(MechFlags, 5);
    public bool FullPotential => HasBit(MechFlags, 6);
    public bool ReverseProgram => HasBit(MechFlags, 7);

    protected override void UpdateData()
    {
        FireFlags = mem.ReadByte(ram.A(FIRE_ADDR));
        AirFlags = mem.ReadByte(ram.A(AIR_ADDR));
        IceFlags = mem.ReadByte(ram.A(ICE_ADDR));
        MechFlags = mem.ReadByte(ram.A(MECH_ADDR));
        EarthFlags = mem.ReadByte(ram.A(EARTH_ADDR));
        BattleFlags = mem.ReadByte(ram.A(BATTLE_ADDR));
        
        EmulatorLinkEventHub.SignalDigimonTechniqueStatsSynchronized();
    }

    private static bool HasBit(byte value, int bit)
        => (value & (1 << bit)) != 0;
}