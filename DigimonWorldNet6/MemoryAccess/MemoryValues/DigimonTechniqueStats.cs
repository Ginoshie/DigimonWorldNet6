using System.Numerics;
using MemoryAccess.Core;

namespace MemoryAccess.MemoryValues;

public sealed class DigimonTechniqueStats
{
    // ===== Technique flag addresses =====
    private const int FIRE_ADDR   = 0x00155800;
    private const int AIR_ADDR    = 0x00155801;
    private const int ICE_ADDR    = 0x00155802;
    private const int MECH_ADDR   = 0x00155803;
    private const int EARTH_ADDR  = 0x00155804;
    private const int BATTLE_ADDR = 0x00155805;
    
    private readonly ProcessMemory _mem;
    private readonly PsxRam _ram;

    private DigimonTechniqueStats()
    {
        _mem = ProcessMemory.Empty;
        _ram = PsxRam.Empty;
    }

    public DigimonTechniqueStats(ProcessMemory mem, PsxRam ram)
    {
        _mem = mem;
        _ram = ram;
    }

    public static DigimonTechniqueStats Empty { get; } = new();
    
    public int LearnedTechniqueCount()
    {
        return CountBits(FIRE_ADDR)
               + CountBits(AIR_ADDR)
               + CountBits(ICE_ADDR)
               + CountBits(MECH_ADDR)
               + CountBits(EARTH_ADDR)
               + CountBits(BATTLE_ADDR);
    }

    // ===== Fire attacks =====
    public bool FireTower       => HasBit(FIRE_ADDR, 0);
    public bool ProminenceBeam => HasBit(FIRE_ADDR, 1);
    public bool SpitFire       => HasBit(FIRE_ADDR, 2);
    public bool RedInferno     => HasBit(FIRE_ADDR, 3);
    public bool MagmaBomb      => HasBit(FIRE_ADDR, 4);
    public bool HeatLaser      => HasBit(FIRE_ADDR, 5);
    public bool InfinityBurn  => HasBit(FIRE_ADDR, 6);
    public bool Meltdown       => HasBit(FIRE_ADDR, 7);

    // ===== Battle attacks =====
    public bool Tremar         => HasBit(BATTLE_ADDR, 0);
    public bool MuscleCharge  => HasBit(BATTLE_ADDR, 1);
    public bool WarCry        => HasBit(BATTLE_ADDR, 2);
    public bool SonicJab      => HasBit(BATTLE_ADDR, 3);
    public bool DynamiteKick => HasBit(BATTLE_ADDR, 4);
    public bool Counter      => HasBit(BATTLE_ADDR, 5);
    public bool MegatonPunch => HasBit(BATTLE_ADDR, 6);
    public bool BusterDive   => HasBit(BATTLE_ADDR, 7);

    // ===== Air attacks =====
    public bool ThunderJustice  => HasBit(AIR_ADDR, 0);
    public bool SpinningShot   => HasBit(AIR_ADDR, 1);
    public bool ElectricCloud => HasBit(AIR_ADDR, 2);
    public bool MegaloSpark   => HasBit(AIR_ADDR, 3);
    public bool StaticElect   => HasBit(AIR_ADDR, 4);
    public bool WindCutter    => HasBit(AIR_ADDR, 5);
    public bool ConfusedStorm => HasBit(AIR_ADDR, 6);
    public bool Hurricane     => HasBit(AIR_ADDR, 7);

    // ===== Earth attacks =====
    public bool PoisonPowder  => HasBit(EARTH_ADDR, 0);
    public bool Bug           => HasBit(EARTH_ADDR, 1);
    public bool MassMorph    => HasBit(EARTH_ADDR, 2);
    public bool InsectPlague => HasBit(EARTH_ADDR, 3);
    public bool CharmPerfume => HasBit(EARTH_ADDR, 4);
    public bool PoisonClaw   => HasBit(EARTH_ADDR, 5);
    public bool DangerSting  => HasBit(EARTH_ADDR, 6);
    public bool GreenTrap    => HasBit(EARTH_ADDR, 7);

    // ===== Ice attacks =====
    public bool GigaFreeze    => HasBit(ICE_ADDR, 0);
    public bool IceStatue    => HasBit(ICE_ADDR, 1);
    public bool WinterBlast  => HasBit(ICE_ADDR, 2);
    public bool IceNeedle    => HasBit(ICE_ADDR, 3);
    public bool WaterBlit    => HasBit(ICE_ADDR, 4);
    public bool AquaMagic    => HasBit(ICE_ADDR, 5);
    public bool AuroraFreeze => HasBit(ICE_ADDR, 6);
    public bool TearDrop     => HasBit(ICE_ADDR, 7);

    // ===== Mech attacks =====
    public bool PowerCrane      => HasBit(MECH_ADDR, 0);
    public bool AllRangeBeam   => HasBit(MECH_ADDR, 1);
    public bool MetalSprinter  => HasBit(MECH_ADDR, 2);
    public bool PulseLaser     => HasBit(MECH_ADDR, 3);
    public bool DeleteProgram  => HasBit(MECH_ADDR, 4);
    public bool DGDimension    => HasBit(MECH_ADDR, 5);
    public bool FullPotential  => HasBit(MECH_ADDR, 6);
    public bool ReverseProgram => HasBit(MECH_ADDR, 7);
    
    private int CountBits(int address)
    {
        return BitOperations.PopCount(_mem.ReadByte(_ram.A(address)));
    }

    private bool HasBit(int address, int bit)
    {
        byte value = _mem.ReadByte(_ram.A(address));
        return (value & (1 << bit)) != 0;
    }
}
