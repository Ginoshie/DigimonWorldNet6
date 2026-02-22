using Shared.Extensions;
using Shared.Enums;

namespace Shared.Constants;

public static class DigimonTypes
{
    public static Digimon Agumon { get; } = new(3, DigimonName.Agumon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Airdramon { get; } = new(7, DigimonName.Airdramon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Andromon { get; } = new(40, DigimonName.Andromon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Angemon { get; } = new(20, DigimonName.Angemon, GameVariant.Original);
    public static Digimon AngemonVice { get; } = new(20, DigimonName.Angemon, GameVariant.Vice);

    public static Digimon Bakemon { get; } = new(37, DigimonName.Bakemon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Betamon { get; } = new(4, DigimonName.Betamon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Birdramon { get; } = new(21, DigimonName.Birdramon, GameVariant.Original);
    public static Digimon BirdramonVice { get; } = new(21, DigimonName.Birdramon, GameVariant.Vice);

    public static Digimon Biyomon { get; } = new(45, DigimonName.Biyomon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Botamon { get; } = new(1, DigimonName.Botamon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Centarumon { get; } = new(36, DigimonName.Centarumon, GameVariant.Original);
    public static Digimon CentarumonVice { get; } = new(36, DigimonName.Centarumon, GameVariant.Vice);

    public static Digimon Coelamon { get; } = new(49, DigimonName.Coelamon, GameVariant.Original);
    public static Digimon CoelamonVice { get; } = new(49, DigimonName.Coelamon, GameVariant.Vice);

    public static Digimon Devimon { get; } = new(6, DigimonName.Devimon, GameVariant.Original | GameVariant.Vice, GameVariant.MyotismonPatch);
    public static Digimon DevimonMyotismon { get; } = new(6, DigimonName.Devimon, GameVariant.Vice | GameVariant.MyotismonPatch);

    public static Digimon Digitamamon { get; } = new(56, DigimonName.Digitamamon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Drimogemon { get; } = new(38, DigimonName.Drimogemon, GameVariant.Original);
    public static Digimon DrimogemonVice { get; } = new(38, DigimonName.Drimogemon, GameVariant.Vice);

    public static Digimon Elecmon { get; } = new(18, DigimonName.Elecmon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Etemon { get; } = new(42, DigimonName.Etemon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Frigimon { get; } = new(23, DigimonName.Frigimon, GameVariant.Original);
    public static Digimon FrigimonVice { get; } = new(23, DigimonName.Frigimon, GameVariant.Vice);

    public static Digimon Gabumon { get; } = new(17, DigimonName.Gabumon, GameVariant.Original | GameVariant.Vice, GameVariant.PanjyamonPatch);
    public static Digimon GabumonPanjyamon { get; } = new(17, DigimonName.Gabumon, GameVariant.Vice | GameVariant.PanjyamonPatch);

    public static Digimon Garurumon { get; } = new(22, DigimonName.Garurumon, GameVariant.Original);
    public static Digimon GarurumonVice { get; } = new(22, DigimonName.Garurumon, GameVariant.Vice);

    public static Digimon Gigadramon { get; } = new(64, DigimonName.Gigadramon, GameVariant.Vice);

    public static Digimon Giromon { get; } = new(41, DigimonName.Giromon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Greymon { get; } = new(5, DigimonName.Greymon, GameVariant.Original);
    public static Digimon GreymonVice { get; } = new(5, DigimonName.Greymon, GameVariant.Vice);

    public static Digimon HerculesKabuterimon { get; } = new(60, DigimonName.HerculesKabuterimon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Kabuterimon { get; } = new(19, DigimonName.Kabuterimon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Kokatorimon { get; } = new(50, DigimonName.Kokatorimon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Koromon { get; } = new(2, DigimonName.Koromon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Kunemon { get; } = new(32, DigimonName.Kunemon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Kuwagamon { get; } = new(51, DigimonName.Kuwagamon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Leomon { get; } = new(48, DigimonName.Leomon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Machinedramon { get; } = new(62, DigimonName.Machinedramon, GameVariant.Vice, GameVariant.MyotismonPatch);

    public static Digimon Mamemon { get; } = new(13, DigimonName.Mamemon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Megadramon { get; } = new(54, DigimonName.Megadramon, GameVariant.Original | GameVariant.Vice);

    public static Digimon MegaSeadramon { get; } = new(61, DigimonName.MegaSeadramon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Meramon { get; } = new(9, DigimonName.Meramon, GameVariant.Original);
    public static Digimon MeramonVice { get; } = new(9, DigimonName.Meramon, GameVariant.Vice);

    public static Digimon MetalEtemon { get; } = new(65, DigimonName.MetalEtemon, GameVariant.Vice);

    public static Digimon MetalGreymon { get; } = new(12, DigimonName.MetalGreymon, GameVariant.Original | GameVariant.Vice);

    public static Digimon MetalMamemon { get; } = new(27, DigimonName.MetalMamemon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Mojyamon { get; } = new(52, DigimonName.Mojyamon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Monochromon { get; } = new(47, DigimonName.Monochromon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Monzaemon { get; } = new(14, DigimonName.Monzaemon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Myotismon { get; } = new(63, DigimonName.Myotismon, GameVariant.Vice | GameVariant.MyotismonPatch);

    public static Digimon Nanimon { get; } = new(53, DigimonName.Nanimon, GameVariant.Original | GameVariant.Vice, GameVariant.MyotismonPatch);
    public static Digimon NanimonMyotismon { get; } = new(53, DigimonName.Nanimon, GameVariant.Vice | GameVariant.MyotismonPatch);

    public static Digimon Ninjamon { get; } = new(58, DigimonName.Ninjamon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Numemon { get; } = new(11, DigimonName.Numemon, GameVariant.Original);
    public static Digimon NumemonVice { get; } = new(11, DigimonName.Numemon, GameVariant.Vice);

    public static Digimon Ogremon { get; } = new(34, DigimonName.Ogremon, GameVariant.Original);
    public static Digimon OgremonVice { get; } = new(34, DigimonName.Ogremon, GameVariant.Vice);

    public static Digimon Palmon { get; } = new(46, DigimonName.Palmon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Patamon { get; } = new(31, DigimonName.Patamon, GameVariant.Original | GameVariant.Vice);
    
    public static Digimon PanjyamonPanjyamonAndMyotismon { get; } = new(63, DigimonName.Panjyamon, GameVariant.Vice | GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch);
    public static Digimon PanjyamonPanjyamon { get; } = new(63, DigimonName.Panjyamon, GameVariant.Vice | GameVariant.PanjyamonPatch, GameVariant.MyotismonPatch);

    public static Digimon Penguinmon { get; } = new(57, DigimonName.Penguinmon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Phoenixmon { get; } = new(59, DigimonName.Phoenixmon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Piximon { get; } = new(55, DigimonName.Piximon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Poyomon { get; } = new(29, DigimonName.Poyomon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Punimon { get; } = new(15, DigimonName.Punimon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Seadramon { get; } = new(10, DigimonName.Seadramon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Shellmon { get; } = new(35, DigimonName.Shellmon, GameVariant.Original | GameVariant.Vice);

    public static Digimon SkullGreymon { get; } = new(26, DigimonName.SkullGreymon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Sukamon { get; } = new(39, DigimonName.Sukamon, GameVariant.Original);
    public static Digimon SukamonVice { get; } = new(39, DigimonName.Sukamon, GameVariant.Vice);

    public static Digimon Tanemon { get; } = new(44, DigimonName.Tanemon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Tokomon { get; } = new(30, DigimonName.Tokomon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Tsunomon { get; } = new(16, DigimonName.Tsunomon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Tyrannomon { get; } = new(8, DigimonName.Tyrannomon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Unimon { get; } = new(33, DigimonName.Unimon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Vademon { get; } = new(28, DigimonName.Vademon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Vegiemon { get; } = new(25, DigimonName.Vegiemon, GameVariant.Original, GameVariant.Vice);
    public static Digimon VegiemonVice { get; } = new(25, DigimonName.Vegiemon, GameVariant.Vice, GameVariant.Original | GameVariant.MyotismonPatch);
    public static Digimon VegiemonMyotismon { get; } = new(25, DigimonName.Vegiemon, GameVariant.Vice | GameVariant.MyotismonPatch);

    public static Digimon Whamon { get; } = new(24, DigimonName.Whamon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Yuramon { get; } = new(43, DigimonName.Yuramon, GameVariant.Original | GameVariant.Vice);

    public static Digimon Weregarurumon { get; } = new(63, DigimonName.Weregarurumon, GameVariant.Vice, GameVariant.Original | GameVariant.PanjyamonPatch);

    public static IReadOnlyList<Digimon> AllDigimonTypes { get; } =
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
            .Select(d => d.DigimonName);

    public static Digimon Get(int byteValue, GameVariant mode)
        => AllDigimonTypes
            .First(d => d.ByteValue == byteValue && d.IncludeGameVariantFlags.IsAvailableIn(d.ExcludeGameVariantFlags, mode));
}