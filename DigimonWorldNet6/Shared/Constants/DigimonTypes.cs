using System.Collections.ObjectModel;
using Generics.Enums;

namespace Generics.Constants;

public static class DigimonTypes
{
    public static DigimonType[] AllDigimonTypes { get; } =
    [
        new(DigimonName.Agumon, EvolutionCalculatorMode.Original),
        new(DigimonName.Airdramon, EvolutionCalculatorMode.Original),
        new(DigimonName.Andromon, EvolutionCalculatorMode.Original),
        new(DigimonName.Angemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Bakemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Betamon, EvolutionCalculatorMode.Original),
        new(DigimonName.Birdramon, EvolutionCalculatorMode.Original),
        new(DigimonName.Biyomon, EvolutionCalculatorMode.Original),
        new(DigimonName.Botamon, EvolutionCalculatorMode.Original),
        new(DigimonName.Centarumon, EvolutionCalculatorMode.Original),
        new(DigimonName.Coelamon, EvolutionCalculatorMode.Original),
        new(DigimonName.Devimon, EvolutionCalculatorMode.Original),
        new(DigimonName.Digitamamon, EvolutionCalculatorMode.Original),
        new(DigimonName.Drimogemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Elecmon, EvolutionCalculatorMode.Original),
        new(DigimonName.Etemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Frigimon, EvolutionCalculatorMode.Original),
        new(DigimonName.Gabumon, EvolutionCalculatorMode.Original),
        new(DigimonName.Garurumon, EvolutionCalculatorMode.Original),
        new(DigimonName.Giromon, EvolutionCalculatorMode.Original),
        new(DigimonName.Greymon, EvolutionCalculatorMode.Original),
        new(DigimonName.HerculesKabuterimon, EvolutionCalculatorMode.Original),
        new(DigimonName.Kabuterimon, EvolutionCalculatorMode.Original),
        new(DigimonName.Kokatorimon, EvolutionCalculatorMode.Original),
        new(DigimonName.Koromon, EvolutionCalculatorMode.Original),
        new(DigimonName.Kunemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Kuwagamon, EvolutionCalculatorMode.Original),
        new(DigimonName.Leomon, EvolutionCalculatorMode.Original),
        new(DigimonName.Mamemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Megadramon, EvolutionCalculatorMode.Original),
        new(DigimonName.MegaSeadramon, EvolutionCalculatorMode.Original),
        new(DigimonName.Meramon, EvolutionCalculatorMode.Original),
        new(DigimonName.MetalGreymon, EvolutionCalculatorMode.Original),
        new(DigimonName.MetalMamemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Mojyamon, EvolutionCalculatorMode.Original),
        new(DigimonName.Monochromon, EvolutionCalculatorMode.Original),
        new(DigimonName.Monzaemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Nanimon, EvolutionCalculatorMode.Original),
        new(DigimonName.Ninjamon, EvolutionCalculatorMode.Original),
        new(DigimonName.Numemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Ogremon, EvolutionCalculatorMode.Original),
        new(DigimonName.Palmon, EvolutionCalculatorMode.Original),
        new(DigimonName.Patamon, EvolutionCalculatorMode.Original),
        new(DigimonName.Penguinmon, EvolutionCalculatorMode.Original),
        new(DigimonName.Phoenixmon, EvolutionCalculatorMode.Original),
        new(DigimonName.Piximon, EvolutionCalculatorMode.Original),
        new(DigimonName.Poyomon, EvolutionCalculatorMode.Original),
        new(DigimonName.Punimon, EvolutionCalculatorMode.Original),
        new(DigimonName.Seadramon, EvolutionCalculatorMode.Original),
        new(DigimonName.Shellmon, EvolutionCalculatorMode.Original),
        new(DigimonName.SkullGreymon, EvolutionCalculatorMode.Original),
        new(DigimonName.Sukamon, EvolutionCalculatorMode.Original),
        new(DigimonName.Tanemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Tokomon, EvolutionCalculatorMode.Original),
        new(DigimonName.Tsunomon, EvolutionCalculatorMode.Original),
        new(DigimonName.Tyrannomon, EvolutionCalculatorMode.Original),
        new(DigimonName.Unimon, EvolutionCalculatorMode.Original),
        new(DigimonName.Vademon, EvolutionCalculatorMode.Original),
        new(DigimonName.Vegiemon, EvolutionCalculatorMode.Original),
        new(DigimonName.Whamon, EvolutionCalculatorMode.Original),
        new(DigimonName.Yuramon, EvolutionCalculatorMode.Original),
        new(DigimonName.Weregarurumon, EvolutionCalculatorMode.Vice),
        new(DigimonName.Gigadramon, EvolutionCalculatorMode.Vice),
        new(DigimonName.MetalEtemon, EvolutionCalculatorMode.Vice),
        new(DigimonName.Machinedramon, EvolutionCalculatorMode.Vice),
        new(DigimonName.Myotismon, EvolutionCalculatorMode.ViceMyotismon),
        new(DigimonName.Panjyamon, EvolutionCalculatorMode.VicePanjyamon)
    ];

    private static readonly ReadOnlyDictionary<EvolutionCalculatorMode, IReadOnlyList<DigimonType>> DigimonTypeOfCalculatorMode =
        new(
            new Dictionary<EvolutionCalculatorMode, IReadOnlyList<DigimonType>>
            {
                [EvolutionCalculatorMode.Original] = AllDigimonTypes
                    .Where(t => t.EvolutionCalculatorMode == EvolutionCalculatorMode.Original)
                    .ToArray(),

                [EvolutionCalculatorMode.Vice] = AllDigimonTypes
                    .Where(t => t.EvolutionCalculatorMode is EvolutionCalculatorMode.Original or EvolutionCalculatorMode.Vice)
                    .ToArray(),

                [EvolutionCalculatorMode.ViceMyotismon] = AllDigimonTypes
                    .Where(t => t.EvolutionCalculatorMode is EvolutionCalculatorMode.Original or EvolutionCalculatorMode.ViceMyotismon)
                    .ToArray(),

                [EvolutionCalculatorMode.VicePanjyamon] = AllDigimonTypes
                    .Where(t => t.EvolutionCalculatorMode is EvolutionCalculatorMode.Original or EvolutionCalculatorMode.VicePanjyamon)
                    .ToArray()
            });

    public static IEnumerable<DigimonName> Get(EvolutionCalculatorMode mode)
        => DigimonTypeOfCalculatorMode[mode].Select(d => d.Digimon);
}
