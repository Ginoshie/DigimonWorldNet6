using MemoryAccess.Core;
using Shared.Enums;

namespace MemoryAccess.MemoryValues;

public class HistoricEvolutions
{
    private const int ADDR_225 = 0x001BE00D;
    private const int ADDR_226 = 0x001BE00E;
    private const int ADDR_227 = 0x001BE00F;
    private const int ADDR_228 = 0x001BE010;
    private const int ADDR_229 = 0x001BE011;
    private const int ADDR_230 = 0x001BE012;
    private const int ADDR_231 = 0x001BE013;
    private const int ADDR_232 = 0x001BE014;
    private const int ADDR_233 = 0x001BE015;

    private readonly ProcessMemory _mem;
    private readonly PsxRam _ram;

    private HistoricEvolutions()
    {
        _mem = ProcessMemory.Empty;
        _ram = PsxRam.Empty;
    }

    public HistoricEvolutions(ProcessMemory mem, PsxRam ram)
    {
        _mem = mem;
        _ram = ram;
    }

    public static HistoricEvolutions Empty { get; } = new();

    public bool IsHistoricEvolution(DigimonName name) =>
        _map.TryGetValue(name, out (int addr, int bit) entry)
        && HasBit(entry.addr, entry.bit);

    // ===== Fresh stage =====
    public bool Botamon => HasBit(ADDR_225, 1);
    public bool Poyomon => HasBit(ADDR_228, 5);
    public bool Punimon => HasBit(ADDR_226, 7);
    public bool Yuramon => HasBit(ADDR_230, 3);

    // ===== In-training stage =====
    public bool Koromon => HasBit(ADDR_225, 2);
    public bool Tanemon => HasBit(ADDR_230, 4);
    public bool Tokomon => HasBit(ADDR_228, 6);
    public bool Tsunomon => HasBit(ADDR_227, 0);

    // ===== Rookie stage =====
    public bool Agumon => HasBit(ADDR_225, 3);
    public bool Betamon => HasBit(ADDR_225, 4);
    public bool Biyomon => HasBit(ADDR_230, 5);
    public bool Elecmon => HasBit(ADDR_227, 2);
    public bool Gabumon => HasBit(ADDR_227, 1);
    public bool Kunemon => HasBit(ADDR_229, 0);
    public bool Palmon => HasBit(ADDR_230, 6);
    public bool Patamon => HasBit(ADDR_228, 7);
    public bool Penguinmon => HasBit(ADDR_232, 1);

    // ===== Champion stage =====
    public bool Airdramon => HasBit(ADDR_225, 7);
    public bool Angemon => HasBit(ADDR_227, 4);
    public bool Bakemon => HasBit(ADDR_229, 5);
    public bool Birdramon => HasBit(ADDR_227, 5);
    public bool Centarumon => HasBit(ADDR_229, 4);
    public bool Coelamon => HasBit(ADDR_231, 1);
    public bool Devimon => HasBit(ADDR_225, 6);
    public bool Drimogemon => HasBit(ADDR_229, 6);
    public bool Frigimon => HasBit(ADDR_227, 7);
    public bool Garurumon => HasBit(ADDR_227, 6);
    public bool Greymon => HasBit(ADDR_225, 5);
    public bool Kabuterimon => HasBit(ADDR_227, 3);
    public bool Kokatorimon => HasBit(ADDR_231, 2);
    public bool Kuwagamon => HasBit(ADDR_231, 3);
    public bool Leomon => HasBit(ADDR_231, 0);
    public bool Meramon => HasBit(ADDR_226, 1);
    public bool Mojyamon => HasBit(ADDR_231, 4);
    public bool Monochromon => HasBit(ADDR_230, 7);
    public bool Nanimon => HasBit(ADDR_231, 5);
    public bool Numemon => HasBit(ADDR_226, 3);
    public bool Ogremon => HasBit(ADDR_229, 2);
    public bool Seadramon => HasBit(ADDR_226, 2);
    public bool Shellmon => HasBit(ADDR_229, 3);
    public bool Sukamon => HasBit(ADDR_229, 7);
    public bool Tyrannomon => HasBit(ADDR_226, 0);
    public bool Unimon => HasBit(ADDR_229, 1);
    public bool Vegiemon => HasBit(ADDR_228, 1);
    public bool Whamon => HasBit(ADDR_228, 0);

    // ===== Ultimate stage =====
    public bool Andromon => HasBit(ADDR_230, 0);
    public bool Digitamamon => HasBit(ADDR_232, 0);
    public bool Etemon => HasBit(ADDR_230, 2);
    public bool Giromon => HasBit(ADDR_230, 1);
    public bool HKabuterimon => HasBit(ADDR_232, 4);
    public bool Mamemon => HasBit(ADDR_226, 5);
    public bool MegaSeadramon => HasBit(ADDR_232, 5);
    public bool Megadramon => HasBit(ADDR_231, 6);
    public bool MetalEtemon => HasBit(ADDR_233, 1);
    public bool MetalGreymon => HasBit(ADDR_226, 4);
    public bool MetalMamemon => HasBit(ADDR_228, 3);
    public bool Monzaemon => HasBit(ADDR_226, 6);
    public bool Myotismon => HasBit(ADDR_232, 6);
    public bool Panjyamon => HasBit(ADDR_232, 7);
    public bool Phoenixmon => HasBit(ADDR_232, 3);
    public bool Piximon => HasBit(ADDR_231, 7);
    public bool SkullGreymon => HasBit(ADDR_228, 2);
    public bool Vademon => HasBit(ADDR_228, 4);
    public bool Machinedramon => HasBit(ADDR_232, 6);
    public bool Weregarurumon => HasBit(ADDR_232, 7);
    public bool Gigadramon => HasBit(ADDR_233, 0);

    private static readonly Dictionary<DigimonName, (int addr, int bit)> _map = new()
    {
        { DigimonName.Botamon, (ADDR_225, 1) },
        { DigimonName.Koromon, (ADDR_225, 2) },
        { DigimonName.Agumon, (ADDR_225, 3) },
        { DigimonName.Betamon, (ADDR_225, 4) },
        { DigimonName.Greymon, (ADDR_225, 5) },
        { DigimonName.Devimon, (ADDR_225, 6) },
        { DigimonName.Airdramon, (ADDR_225, 7) },

        { DigimonName.Tyrannomon, (ADDR_226, 0) },
        { DigimonName.Meramon, (ADDR_226, 1) },
        { DigimonName.Seadramon, (ADDR_226, 2) },
        { DigimonName.Numemon, (ADDR_226, 3) },
        { DigimonName.MetalGreymon, (ADDR_226, 4) },
        { DigimonName.Mamemon, (ADDR_226, 5) },
        { DigimonName.Monzaemon, (ADDR_226, 6) },
        { DigimonName.Punimon, (ADDR_226, 7) },

        { DigimonName.Tsunomon, (ADDR_227, 0) },
        { DigimonName.Gabumon, (ADDR_227, 1) },
        { DigimonName.Elecmon, (ADDR_227, 2) },
        { DigimonName.Kabuterimon, (ADDR_227, 3) },
        { DigimonName.Angemon, (ADDR_227, 4) },
        { DigimonName.Birdramon, (ADDR_227, 5) },
        { DigimonName.Garurumon, (ADDR_227, 6) },
        { DigimonName.Frigimon, (ADDR_227, 7) },

        { DigimonName.Whamon, (ADDR_228, 0) },
        { DigimonName.Vegiemon, (ADDR_228, 1) },
        { DigimonName.SkullGreymon, (ADDR_228, 2) },
        { DigimonName.MetalMamemon, (ADDR_228, 3) },
        { DigimonName.Vademon, (ADDR_228, 4) },
        { DigimonName.Poyomon, (ADDR_228, 5) },
        { DigimonName.Tokomon, (ADDR_228, 6) },
        { DigimonName.Patamon, (ADDR_228, 7) },

        { DigimonName.Kunemon, (ADDR_229, 0) },
        { DigimonName.Unimon, (ADDR_229, 1) },
        { DigimonName.Ogremon, (ADDR_229, 2) },
        { DigimonName.Shellmon, (ADDR_229, 3) },
        { DigimonName.Centarumon, (ADDR_229, 4) },
        { DigimonName.Bakemon, (ADDR_229, 5) },
        { DigimonName.Drimogemon, (ADDR_229, 6) },
        { DigimonName.Sukamon, (ADDR_229, 7) },

        { DigimonName.Andromon, (ADDR_230, 0) },
        { DigimonName.Giromon, (ADDR_230, 1) },
        { DigimonName.Etemon, (ADDR_230, 2) },
        { DigimonName.Yuramon, (ADDR_230, 3) },
        { DigimonName.Tanemon, (ADDR_230, 4) },
        { DigimonName.Biyomon, (ADDR_230, 5) },
        { DigimonName.Palmon, (ADDR_230, 6) },
        { DigimonName.Monochromon, (ADDR_230, 7) },

        { DigimonName.Leomon, (ADDR_231, 0) },
        { DigimonName.Coelamon, (ADDR_231, 1) },
        { DigimonName.Kokatorimon, (ADDR_231, 2) },
        { DigimonName.Kuwagamon, (ADDR_231, 3) },
        { DigimonName.Mojyamon, (ADDR_231, 4) },
        { DigimonName.Nanimon, (ADDR_231, 5) },
        { DigimonName.Megadramon, (ADDR_231, 6) },
        { DigimonName.Piximon, (ADDR_231, 7) },

        { DigimonName.Digitamamon, (ADDR_232, 0) },
        { DigimonName.Penguinmon, (ADDR_232, 1) },
        { DigimonName.Ninjamon, (ADDR_232, 2) },
        { DigimonName.Phoenixmon, (ADDR_232, 3) },
        { DigimonName.HerculesKabuterimon, (ADDR_232, 4) },
        { DigimonName.MegaSeadramon, (ADDR_232, 5) },
        { DigimonName.Machinedramon, (ADDR_232, 6) },
        { DigimonName.Myotismon, (ADDR_232, 6) },
        { DigimonName.Panjyamon, (ADDR_232, 7) },
        { DigimonName.Weregarurumon, (ADDR_232, 7) },

        { DigimonName.Gigadramon, (ADDR_233, 0) },
        { DigimonName.MetalEtemon, (ADDR_233, 1) }
    };

    private bool HasBit(int address, int bit)
    {
        byte value = _mem.ReadByte(_ram.A(address));
        return (value & (1 << bit)) != 0;
    }
}