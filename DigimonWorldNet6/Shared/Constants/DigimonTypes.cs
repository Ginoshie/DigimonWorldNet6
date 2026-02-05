using Shared.Extensions;
using Shared.Enums;

namespace Shared.Constants;

public static class DigimonTypes
{
    public static DigimonType Agumon { get; } = new(3, DigimonName.Agumon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Airdramon { get; } = new(7, DigimonName.Airdramon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Andromon { get; } = new(40, DigimonName.Andromon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Angemon { get; } = new(20, DigimonName.Angemon, GameVariant.Original);
    public static DigimonType AngemonVice { get; } = new(20, DigimonName.Angemon, GameVariant.Vice);

    public static DigimonType Bakemon { get; } = new(37, DigimonName.Bakemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Betamon { get; } = new(4, DigimonName.Betamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Birdramon { get; } = new(21, DigimonName.Birdramon, GameVariant.Original);
    public static DigimonType BirdramonVice { get; } = new(21, DigimonName.Birdramon, GameVariant.Vice);

    public static DigimonType Biyomon { get; } = new(45, DigimonName.Biyomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Botamon { get; } = new(1, DigimonName.Botamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Centarumon { get; } = new(36, DigimonName.Centarumon, GameVariant.Original);
    public static DigimonType CentarumonVice { get; } = new(36, DigimonName.Centarumon, GameVariant.Vice);

    public static DigimonType Coelamon { get; } = new(49, DigimonName.Coelamon, GameVariant.Original);
    public static DigimonType CoelamonVice { get; } = new(49, DigimonName.Coelamon, GameVariant.Vice);

    public static DigimonType Devimon { get; } = new(6, DigimonName.Devimon, GameVariant.Original | GameVariant.Vice);
    public static DigimonType DevimonMyotismon { get; } = new(6, DigimonName.Devimon, GameVariant.Vice | GameVariant.MyotismonPatch);

    public static DigimonType Digitamamon { get; } = new(56, DigimonName.Digitamamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Drimogemon { get; } = new(38, DigimonName.Drimogemon, GameVariant.Original);
    public static DigimonType DrimogemonVice { get; } = new(38, DigimonName.Drimogemon, GameVariant.Vice);

    public static DigimonType Elecmon { get; } = new(18, DigimonName.Elecmon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Etemon { get; } = new(42, DigimonName.Etemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Frigimon { get; } = new(23, DigimonName.Frigimon, GameVariant.Original);
    public static DigimonType FrigimonVice { get; } = new(23, DigimonName.Frigimon, GameVariant.Vice);

    public static DigimonType Gabumon { get; } = new(17, DigimonName.Gabumon, GameVariant.Original | GameVariant.Vice);
    public static DigimonType GabumonPanjyamon { get; } = new(17, DigimonName.Gabumon, GameVariant.Vice);

    public static DigimonType Garurumon { get; } = new(22, DigimonName.Garurumon, GameVariant.Original);
    public static DigimonType GarurumonVice { get; } = new(22, DigimonName.Garurumon, GameVariant.Vice);

    public static DigimonType Gigadramon { get; } = new(64, DigimonName.Gigadramon, GameVariant.Vice);

    public static DigimonType Giromon { get; } = new(41, DigimonName.Giromon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Greymon { get; } = new(5, DigimonName.Greymon, GameVariant.Original);
    public static DigimonType GreymonVice { get; } = new(5, DigimonName.Greymon, GameVariant.Vice);

    public static DigimonType HerculesKabuterimon { get; } = new(60, DigimonName.HerculesKabuterimon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Kabuterimon { get; } = new(19, DigimonName.Kabuterimon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Kokatorimon { get; } = new(50, DigimonName.Kokatorimon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Koromon { get; } = new(2, DigimonName.Koromon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Kunemon { get; } = new(32, DigimonName.Kunemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Kuwagamon { get; } = new(51, DigimonName.Kuwagamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Leomon { get; } = new(48, DigimonName.Leomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Machinedramon { get; } = new(62, DigimonName.Machinedramon, GameVariant.Vice);

    public static DigimonType Mamemon { get; } = new(13, DigimonName.Mamemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Megadramon { get; } = new(54, DigimonName.Megadramon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType MegaSeadramon { get; } = new(61, DigimonName.MegaSeadramon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Meramon { get; } = new(9, DigimonName.Meramon, GameVariant.Original);
    public static DigimonType MeramonVice { get; } = new(9, DigimonName.Meramon, GameVariant.Vice);

    public static DigimonType MetalEtemon { get; } = new(65, DigimonName.MetalEtemon, GameVariant.Vice);

    public static DigimonType MetalGreymon { get; } = new(12, DigimonName.MetalGreymon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType MetalMamemon { get; } = new(27, DigimonName.MetalMamemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Mojyamon { get; } = new(52, DigimonName.Mojyamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Monochromon { get; } = new(47, DigimonName.Monochromon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Monzaemon { get; } = new(14, DigimonName.Monzaemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Myotismon { get; } = new(63, DigimonName.Myotismon, GameVariant.Vice);

    public static DigimonType Nanimon { get; } = new(53, DigimonName.Nanimon, GameVariant.Original | GameVariant.Vice);
    public static DigimonType NanimonMyotismon { get; } = new(53, DigimonName.Nanimon, GameVariant.Vice);

    public static DigimonType Ninjamon { get; } = new(58, DigimonName.Ninjamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Numemon { get; } = new(11, DigimonName.Numemon, GameVariant.Original);
    public static DigimonType NumemonVice { get; } = new(11, DigimonName.Numemon, GameVariant.Vice);

    public static DigimonType Ogremon { get; } = new(34, DigimonName.Ogremon, GameVariant.Original);
    public static DigimonType OgremonVice { get; } = new(34, DigimonName.Ogremon, GameVariant.Vice);

    public static DigimonType Palmon { get; } = new(46, DigimonName.Palmon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Patamon { get; } = new(31, DigimonName.Patamon, GameVariant.Original | GameVariant.Vice);
    
    public static DigimonType PanjyamonPanjyamonAndMyotismon { get; } = new(63, DigimonName.Panjyamon, GameVariant.Vice | GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch);
    public static DigimonType PanjyamonPanjyamon { get; } = new(63, DigimonName.Panjyamon, GameVariant.Vice | GameVariant.PanjyamonPatch, GameVariant.MyotismonPatch);

    public static DigimonType Penguinmon { get; } = new(57, DigimonName.Penguinmon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Phoenixmon { get; } = new(59, DigimonName.Phoenixmon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Piximon { get; } = new(55, DigimonName.Piximon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Poyomon { get; } = new(29, DigimonName.Poyomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Punimon { get; } = new(15, DigimonName.Punimon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Seadramon { get; } = new(10, DigimonName.Seadramon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Shellmon { get; } = new(35, DigimonName.Shellmon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType SkullGreymon { get; } = new(26, DigimonName.SkullGreymon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Sukamon { get; } = new(39, DigimonName.Sukamon, GameVariant.Original);
    public static DigimonType SukamonVice { get; } = new(39, DigimonName.Sukamon, GameVariant.Vice);

    public static DigimonType Tanemon { get; } = new(44, DigimonName.Tanemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Tokomon { get; } = new(30, DigimonName.Tokomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Tsunomon { get; } = new(16, DigimonName.Tsunomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Tyrannomon { get; } = new(8, DigimonName.Tyrannomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Unimon { get; } = new(33, DigimonName.Unimon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Vademon { get; } = new(28, DigimonName.Vademon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Vegiemon { get; } = new(25, DigimonName.Vegiemon, GameVariant.Original);
    public static DigimonType VegiemonVice { get; } = new(25, DigimonName.Vegiemon, GameVariant.Vice);
    public static DigimonType VegiemonMyotismon { get; } = new(25, DigimonName.Vegiemon, GameVariant.Vice | GameVariant.MyotismonPatch);

    public static DigimonType Whamon { get; } = new(24, DigimonName.Whamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Yuramon { get; } = new(43, DigimonName.Yuramon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Weregarurumon { get; } = new(63, DigimonName.Weregarurumon, GameVariant.Vice);

    public static IReadOnlyList<DigimonType> AllDigimonTypes { get; } =
    [
        Agumon,
        Airdramon,
        Andromon,
        Angemon,
        AngemonVice,
        Bakemon,
        Betamon,
        Birdramon,
        BirdramonVice,
        Biyomon,
        Botamon,
        Centarumon,
        CentarumonVice,
        Coelamon,
        CoelamonVice,
        Devimon,
        DevimonMyotismon,
        Digitamamon,
        Drimogemon,
        DrimogemonVice,
        Elecmon,
        Etemon,
        Frigimon,
        FrigimonVice,
        Gabumon,
        GabumonPanjyamon,
        Garurumon,
        GarurumonVice,
        Gigadramon,
        Giromon,
        Greymon,
        GreymonVice,
        HerculesKabuterimon,
        Kabuterimon,
        Kokatorimon,
        Koromon,
        Kunemon,
        Kuwagamon,
        Leomon,
        Machinedramon,
        Mamemon,
        Megadramon,
        MegaSeadramon,
        Meramon,
        MeramonVice,
        MetalEtemon,
        MetalGreymon,
        MetalMamemon,
        Mojyamon,
        Monochromon,
        Monzaemon,
        Myotismon,
        Nanimon,
        NanimonMyotismon,
        Ninjamon,
        Numemon,
        NumemonVice,
        Ogremon,
        OgremonVice,
        Palmon,
        PanjyamonPanjyamonAndMyotismon,
        PanjyamonPanjyamon,
        Patamon,
        Penguinmon,
        Phoenixmon,
        Piximon,
        Poyomon,
        Punimon,
        Seadramon,
        Shellmon,
        SkullGreymon,
        Sukamon,
        SukamonVice,
        Tanemon,
        Tokomon,
        Tsunomon,
        Tyrannomon,
        Unimon,
        Vademon,
        Vegiemon,
        VegiemonMyotismon,
        VegiemonVice,
        Weregarurumon,
        Whamon,
        Yuramon
    ];

    public static IEnumerable<DigimonName> Get(GameVariant mode)
        => AllDigimonTypes
            .Where(d => d.IncludeGameVariantFlags.IsAvailableIn(d.ExcludeGameVariantFlags, mode))
            .Select(d => d.Digimon);

    public static DigimonType Get(int byteValue, GameVariant mode)
        => AllDigimonTypes
            .First(d => d.ByteValue == byteValue && d.IncludeGameVariantFlags.IsAvailableIn(d.ExcludeGameVariantFlags, mode));
}