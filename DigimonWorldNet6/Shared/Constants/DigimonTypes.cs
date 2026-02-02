using Shared.Extensions;
using Shared.Enums;

namespace Shared.Constants;

public static class DigimonTypes
{
    public static DigimonType Agumon { get; } = new(DigimonName.Agumon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Airdramon { get; } = new(DigimonName.Airdramon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Andromon { get; } = new(DigimonName.Andromon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Angemon { get; } = new(DigimonName.Angemon, GameVariant.Original);
    public static DigimonType AngemonVice { get; } = new(DigimonName.Angemon, GameVariant.Vice);

    public static DigimonType Bakemon { get; } = new(DigimonName.Bakemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Betamon { get; } = new(DigimonName.Betamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Birdramon { get; } = new(DigimonName.Birdramon, GameVariant.Original);
    public static DigimonType BirdramonVice { get; } = new(DigimonName.Birdramon, GameVariant.Vice);

    public static DigimonType Biyomon { get; } = new(DigimonName.Biyomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Botamon { get; } = new(DigimonName.Botamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Centarumon { get; } = new(DigimonName.Centarumon, GameVariant.Original);
    public static DigimonType CentarumonVice { get; } = new(DigimonName.Centarumon, GameVariant.Vice);

    public static DigimonType Coelamon { get; } = new(DigimonName.Coelamon, GameVariant.Original);
    public static DigimonType CoelamonVice { get; } = new(DigimonName.Coelamon, GameVariant.Vice);

    public static DigimonType Devimon { get; } = new(DigimonName.Devimon, GameVariant.Original | GameVariant.Vice, GameVariant.MyotismonPatch);
    public static DigimonType DevimonMyotismon { get; } = new(DigimonName.Devimon, GameVariant.Vice | GameVariant.MyotismonPatch);

    public static DigimonType Digitamamon { get; } = new(DigimonName.Digitamamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Drimogemon { get; } = new(DigimonName.Drimogemon, GameVariant.Original);
    public static DigimonType DrimogemonVice { get; } = new(DigimonName.Drimogemon, GameVariant.Vice);

    public static DigimonType Elecmon { get; } = new(DigimonName.Elecmon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Etemon { get; } = new(DigimonName.Etemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Frigimon { get; } = new(DigimonName.Frigimon, GameVariant.Original);
    public static DigimonType FrigimonVice { get; } = new(DigimonName.Frigimon, GameVariant.Vice);

    public static DigimonType Gabumon { get; } = new(DigimonName.Gabumon, GameVariant.Original | GameVariant.Vice, GameVariant.PanjyamonPatch);
    public static DigimonType GabumonPanjyamon { get; } = new(DigimonName.Gabumon, GameVariant.Vice | GameVariant.PanjyamonPatch);

    public static DigimonType Garurumon { get; } = new(DigimonName.Garurumon, GameVariant.Original);
    public static DigimonType GarurumonVice { get; } = new(DigimonName.Garurumon, GameVariant.Vice);

    public static DigimonType Gigadramon { get; } = new(DigimonName.Gigadramon, GameVariant.Vice);

    public static DigimonType Giromon { get; } = new(DigimonName.Giromon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Greymon { get; } = new(DigimonName.Greymon, GameVariant.Original);
    public static DigimonType GreymonVice { get; } = new(DigimonName.Greymon, GameVariant.Vice);

    public static DigimonType HerculesKabuterimon { get; } = new(DigimonName.HerculesKabuterimon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Kabuterimon { get; } = new(DigimonName.Kabuterimon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Kokatorimon { get; } = new(DigimonName.Kokatorimon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Koromon { get; } = new(DigimonName.Koromon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Kunemon { get; } = new(DigimonName.Kunemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Kuwagamon { get; } = new(DigimonName.Kuwagamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Leomon { get; } = new(DigimonName.Leomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Machinedramon { get; } = new(DigimonName.Machinedramon, GameVariant.Vice, GameVariant.MyotismonPatch);

    public static DigimonType Mamemon { get; } = new(DigimonName.Mamemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Megadramon { get; } = new(DigimonName.Megadramon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType MegaSeadramon { get; } = new(DigimonName.MegaSeadramon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Meramon { get; } = new(DigimonName.Meramon, GameVariant.Original);
    public static DigimonType MeramonVice { get; } = new(DigimonName.Meramon, GameVariant.Vice);

    public static DigimonType MetalEtemon { get; } = new(DigimonName.MetalEtemon, GameVariant.Vice);

    public static DigimonType MetalGreymon { get; } = new(DigimonName.MetalGreymon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType MetalMamemon { get; } = new(DigimonName.MetalMamemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Mojyamon { get; } = new(DigimonName.Mojyamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Monochromon { get; } = new(DigimonName.Monochromon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Monzaemon { get; } = new(DigimonName.Monzaemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Myotismon { get; } = new(DigimonName.Myotismon, GameVariant.Vice | GameVariant.MyotismonPatch);

    public static DigimonType Nanimon { get; } = new(DigimonName.Nanimon, GameVariant.Original | GameVariant.Vice, GameVariant.MyotismonPatch);
    public static DigimonType NanimonMyotismon { get; } = new(DigimonName.Nanimon, GameVariant.Vice | GameVariant.MyotismonPatch);

    public static DigimonType Ninjamon { get; } = new(DigimonName.Ninjamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Numemon { get; } = new(DigimonName.Numemon, GameVariant.Original);
    public static DigimonType NumemonVice { get; } = new(DigimonName.Numemon, GameVariant.Vice);

    public static DigimonType Ogremon { get; } = new(DigimonName.Ogremon, GameVariant.Original);
    public static DigimonType OgremonVice { get; } = new(DigimonName.Ogremon, GameVariant.Vice);

    public static DigimonType Palmon { get; } = new(DigimonName.Palmon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType PanjamonPanjamonAndMyotismon { get; } = new(DigimonName.Panjyamon, GameVariant.Vice | GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch);
    public static DigimonType PanjyamonPanjyamon { get; } = new(DigimonName.Panjyamon, GameVariant.Vice | GameVariant.PanjyamonPatch, GameVariant.MyotismonPatch);

    public static DigimonType Patamon { get; } = new(DigimonName.Patamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Penguinmon { get; } = new(DigimonName.Penguinmon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Phoenixmon { get; } = new(DigimonName.Phoenixmon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Piximon { get; } = new(DigimonName.Piximon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Poyomon { get; } = new(DigimonName.Poyomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Punimon { get; } = new(DigimonName.Punimon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Seadramon { get; } = new(DigimonName.Seadramon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Shellmon { get; } = new(DigimonName.Shellmon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType SkullGreymon { get; } = new(DigimonName.SkullGreymon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Sukamon { get; } = new(DigimonName.Sukamon, GameVariant.Original);
    public static DigimonType SukamonVice { get; } = new(DigimonName.Sukamon, GameVariant.Vice);

    public static DigimonType Tanemon { get; } = new(DigimonName.Tanemon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Tokomon { get; } = new(DigimonName.Tokomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Tsunomon { get; } = new(DigimonName.Tsunomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Tyrannomon { get; } = new(DigimonName.Tyrannomon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Unimon { get; } = new(DigimonName.Unimon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Vademon { get; } = new(DigimonName.Vademon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Vegiemon { get; } = new(DigimonName.Vegiemon, GameVariant.Original);
    public static DigimonType VegiemonMyotismon { get; } = new(DigimonName.Vegiemon, GameVariant.Vice | GameVariant.MyotismonPatch);
    public static DigimonType VegiemonVice { get; } = new(DigimonName.Vegiemon, GameVariant.Vice, GameVariant.MyotismonPatch);

    public static DigimonType Whamon { get; } = new(DigimonName.Whamon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Yuramon { get; } = new(DigimonName.Yuramon, GameVariant.Original | GameVariant.Vice);

    public static DigimonType Weregarurumon { get; } = new(DigimonName.Weregarurumon, GameVariant.Vice, GameVariant.PanjyamonPatch);

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
        PanjamonPanjamonAndMyotismon,
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
}