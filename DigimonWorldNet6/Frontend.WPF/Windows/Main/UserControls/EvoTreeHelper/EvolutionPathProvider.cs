using System.Collections.Generic;
using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper;

/// <summary>
/// Provides evolution path data: for a given DigimonName, returns which Digimon it can evolve into.
/// This is a simplified view of the evolution tree for graph display purposes.
/// </summary>
public static class EvolutionPathProvider
{
    private static readonly Dictionary<DigimonName, DigimonName[]> _evolvesTo = new()
    {
        // Fresh → InTraining
        [DigimonName.Botamon] = [DigimonName.Koromon],
        [DigimonName.Poyomon] = [DigimonName.Tokomon],
        [DigimonName.Punimon] = [DigimonName.Tsunomon],
        [DigimonName.Yuramon] = [DigimonName.Tanemon],

        // InTraining → Rookie
        [DigimonName.Koromon] = [DigimonName.Agumon, DigimonName.Gabumon],
        [DigimonName.Tanemon] = [DigimonName.Palmon, DigimonName.Betamon],
        [DigimonName.Tokomon] = [DigimonName.Patamon, DigimonName.Biyomon],
        [DigimonName.Tsunomon] = [DigimonName.Elecmon, DigimonName.Penguinmon],

        // Rookie → Champion
        [DigimonName.Agumon] = [DigimonName.Greymon, DigimonName.Meramon, DigimonName.Birdramon, DigimonName.Centarumon, DigimonName.Monochromon, DigimonName.Tyrannomon],
        [DigimonName.Betamon] = [DigimonName.Seadramon, DigimonName.Whamon, DigimonName.Shellmon, DigimonName.Coelamon],
        [DigimonName.Biyomon] = [DigimonName.Birdramon, DigimonName.Airdramon, DigimonName.Kokatorimon, DigimonName.Unimon, DigimonName.Kabuterimon],
        [DigimonName.Elecmon] = [DigimonName.Leomon, DigimonName.Angemon, DigimonName.Bakemon, DigimonName.Kokatorimon],
        [DigimonName.Gabumon] = [DigimonName.Centarumon, DigimonName.Monochromon, DigimonName.Drimogemon, DigimonName.Tyrannomon, DigimonName.Ogremon, DigimonName.Garurumon],
        [DigimonName.Kunemon] = [DigimonName.Bakemon, DigimonName.Kabuterimon, DigimonName.Kuwagamon, DigimonName.Vegiemon],
        [DigimonName.Palmon] = [DigimonName.Kuwagamon, DigimonName.Vegiemon, DigimonName.Ninjamon, DigimonName.Whamon, DigimonName.Coelamon],
        [DigimonName.Patamon] = [DigimonName.Drimogemon, DigimonName.Tyrannomon, DigimonName.Ogremon, DigimonName.Leomon, DigimonName.Angemon, DigimonName.Unimon],
        [DigimonName.Penguinmon] = [DigimonName.Whamon, DigimonName.Shellmon, DigimonName.Garurumon, DigimonName.Frigimon, DigimonName.Mojyamon],

        // Champion → Ultimate
        [DigimonName.Airdramon] = [DigimonName.Megadramon, DigimonName.Phoenixmon],
        [DigimonName.Angemon] = [DigimonName.Andromon, DigimonName.Phoenixmon],
        [DigimonName.Bakemon] = [DigimonName.SkullGreymon, DigimonName.Giromon],
        [DigimonName.Birdramon] = [DigimonName.Phoenixmon],
        [DigimonName.Centarumon] = [DigimonName.Andromon, DigimonName.Giromon],
        [DigimonName.Coelamon] = [DigimonName.MegaSeadramon],
        [DigimonName.Devimon] = [DigimonName.SkullGreymon, DigimonName.Megadramon],
        [DigimonName.Drimogemon] = [DigimonName.MetalGreymon],
        [DigimonName.Frigimon] = [DigimonName.MetalMamemon, DigimonName.Mamemon],
        [DigimonName.Garurumon] = [DigimonName.SkullGreymon, DigimonName.MegaSeadramon],
        [DigimonName.Greymon] = [DigimonName.MetalGreymon, DigimonName.SkullGreymon],
        [DigimonName.Kabuterimon] = [DigimonName.HerculesKabuterimon, DigimonName.MetalMamemon],
        [DigimonName.Kokatorimon] = [DigimonName.Phoenixmon, DigimonName.Piximon],
        [DigimonName.Kuwagamon] = [DigimonName.HerculesKabuterimon, DigimonName.Piximon],
        [DigimonName.Leomon] = [DigimonName.Andromon, DigimonName.Mamemon],
        [DigimonName.Meramon] = [DigimonName.MetalGreymon, DigimonName.Andromon],
        [DigimonName.Mojyamon] = [DigimonName.SkullGreymon, DigimonName.Mamemon],
        [DigimonName.Monochromon] = [DigimonName.MetalGreymon, DigimonName.MetalMamemon],
        [DigimonName.Nanimon] = [DigimonName.Digitamamon],
        [DigimonName.Ninjamon] = [DigimonName.Piximon, DigimonName.MetalMamemon, DigimonName.Mamemon],
        [DigimonName.Numemon] = [DigimonName.Monzaemon],
        [DigimonName.Ogremon] = [DigimonName.Andromon, DigimonName.Giromon],
        [DigimonName.Seadramon] = [DigimonName.Megadramon, DigimonName.MegaSeadramon],
        [DigimonName.Shellmon] = [DigimonName.HerculesKabuterimon, DigimonName.MegaSeadramon],
        [DigimonName.Sukamon] = [DigimonName.Etemon],
        [DigimonName.Tyrannomon] = [DigimonName.MetalGreymon, DigimonName.Megadramon],
        [DigimonName.Unimon] = [DigimonName.Giromon, DigimonName.Phoenixmon],
        [DigimonName.Vegiemon] = [DigimonName.Piximon],
        [DigimonName.Whamon] = [DigimonName.MegaSeadramon, DigimonName.Mamemon],
    };

    private static readonly Dictionary<DigimonName, DigimonName[]> _evolvesFrom;

    static EvolutionPathProvider()
    {
        _evolvesFrom = new Dictionary<DigimonName, DigimonName[]>();
        Dictionary<DigimonName, List<DigimonName>> reverse = new();

        foreach (KeyValuePair<DigimonName, DigimonName[]> kvp in _evolvesTo)
        {
            foreach (DigimonName target in kvp.Value)
            {
                if (!reverse.ContainsKey(target))
                {
                    reverse[target] = [];
                }
                reverse[target].Add(kvp.Key);
            }
        }

        foreach (KeyValuePair<DigimonName, List<DigimonName>> kvp in reverse)
        {
            _evolvesFrom[kvp.Key] = kvp.Value.ToArray();
        }
    }

    public static DigimonName[] GetEvolutions(DigimonName digimon)
    {
        return _evolvesTo.TryGetValue(digimon, out DigimonName[]? targets)
            ? targets
            : [];
    }

    public static DigimonName[] GetPrecursors(DigimonName digimon)
    {
        return _evolvesFrom.TryGetValue(digimon, out DigimonName[]? sources)
            ? sources
            : [];
    }
}
