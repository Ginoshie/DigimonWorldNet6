using Generics.Enums;

namespace Generics;

public sealed class EvolutionStageMapper
{
    private readonly Dictionary<DigimonType, EvolutionStage> _evolutionStageMappings = new();

    public EvolutionStageMapper()
    {
        _evolutionStageMappings[DigimonType.Agumon] = EvolutionStage.Rookie;
        _evolutionStageMappings[DigimonType.Airdramon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Andromon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Angemon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Bakemon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Betamon] = EvolutionStage.Rookie;
        _evolutionStageMappings[DigimonType.Birdramon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Biyomon] = EvolutionStage.Rookie;
        _evolutionStageMappings[DigimonType.Botamon] = EvolutionStage.Fresh;
        _evolutionStageMappings[DigimonType.Centarumon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Coelamon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Devimon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Digitamamon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Drimogemon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Elecmon] = EvolutionStage.Rookie;
        _evolutionStageMappings[DigimonType.Etemon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Frigimon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Gabumon] = EvolutionStage.Rookie;
        _evolutionStageMappings[DigimonType.Garurumon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Giromon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Greymon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.HerculesKabuterimon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Kabuterimon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Kokatorimon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Koromon] = EvolutionStage.InTraining;
        _evolutionStageMappings[DigimonType.Kunemon] = EvolutionStage.Rookie;
        _evolutionStageMappings[DigimonType.Kuwagamon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Leomon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Mamemon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Megadramon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.MegaSeadramon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Meramon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.MetalGreymon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.MetalMamemon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Mojyamon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Monochromon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Monzaemon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Nanimon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Ninjamon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Numemon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Ogremon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Palmon] = EvolutionStage.Rookie;
        _evolutionStageMappings[DigimonType.Patamon] = EvolutionStage.Rookie;
        _evolutionStageMappings[DigimonType.Penguinmon] = EvolutionStage.Rookie;
        _evolutionStageMappings[DigimonType.Phoenixmon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Piximon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Poyomon] = EvolutionStage.Fresh;
        _evolutionStageMappings[DigimonType.Punimon] = EvolutionStage.Fresh;
        _evolutionStageMappings[DigimonType.Seadramon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Shellmon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.SkullGreymon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Sukamon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Tanemon] = EvolutionStage.InTraining;
        _evolutionStageMappings[DigimonType.Tokomon] = EvolutionStage.InTraining;
        _evolutionStageMappings[DigimonType.Tsunomon] = EvolutionStage.InTraining;
        _evolutionStageMappings[DigimonType.Tyrannomon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Unimon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Vademon] = EvolutionStage.Ultimate;
        _evolutionStageMappings[DigimonType.Vegiemon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Whamon] = EvolutionStage.Champion;
        _evolutionStageMappings[DigimonType.Yuramon] = EvolutionStage.Fresh;
    }

    public EvolutionStage this[DigimonType evolutionResult]
    {
        get
        {
            if (_evolutionStageMappings.TryGetValue(evolutionResult, out EvolutionStage evolutionStage))
            {
                return evolutionStage;
            }

            throw new KeyNotFoundException($"Evolution stage mapping for {evolutionResult} was not found in {nameof(EvolutionStageMapper)}");
        }
    }
}