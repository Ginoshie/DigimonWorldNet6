using Shared.Enums;

namespace Shared;

public static class EvolutionStageMapper
{
    private static readonly Dictionary<DigimonName, EvolutionStage> EvolutionStageMappings = new()
    {
        [DigimonName.Agumon] = EvolutionStage.Rookie,
        [DigimonName.Airdramon] = EvolutionStage.Champion,
        [DigimonName.Andromon] = EvolutionStage.Ultimate,
        [DigimonName.Angemon] = EvolutionStage.Champion,
        [DigimonName.Bakemon] = EvolutionStage.Champion,
        [DigimonName.Betamon] = EvolutionStage.Rookie,
        [DigimonName.Birdramon] = EvolutionStage.Champion,
        [DigimonName.Biyomon] = EvolutionStage.Rookie,
        [DigimonName.Botamon] = EvolutionStage.Fresh,
        [DigimonName.Centarumon] = EvolutionStage.Champion,
        [DigimonName.Coelamon] = EvolutionStage.Champion,
        [DigimonName.Devimon] = EvolutionStage.Champion,
        [DigimonName.Digitamamon] = EvolutionStage.Ultimate,
        [DigimonName.Drimogemon] = EvolutionStage.Champion,
        [DigimonName.Elecmon] = EvolutionStage.Rookie,
        [DigimonName.Etemon] = EvolutionStage.Ultimate,
        [DigimonName.Frigimon] = EvolutionStage.Champion,
        [DigimonName.Gabumon] = EvolutionStage.Rookie,
        [DigimonName.Garurumon] = EvolutionStage.Champion,
        [DigimonName.Giromon] = EvolutionStage.Ultimate,
        [DigimonName.Greymon] = EvolutionStage.Champion,
        [DigimonName.HerculesKabuterimon] = EvolutionStage.Ultimate,
        [DigimonName.Kabuterimon] = EvolutionStage.Champion,
        [DigimonName.Kokatorimon] = EvolutionStage.Champion,
        [DigimonName.Koromon] = EvolutionStage.InTraining,
        [DigimonName.Kunemon] = EvolutionStage.Rookie,
        [DigimonName.Kuwagamon] = EvolutionStage.Champion,
        [DigimonName.Leomon] = EvolutionStage.Champion,
        [DigimonName.Mamemon] = EvolutionStage.Ultimate,
        [DigimonName.Megadramon] = EvolutionStage.Ultimate,
        [DigimonName.MegaSeadramon] = EvolutionStage.Ultimate,
        [DigimonName.Meramon] = EvolutionStage.Champion,
        [DigimonName.MetalGreymon] = EvolutionStage.Ultimate,
        [DigimonName.MetalMamemon] = EvolutionStage.Ultimate,
        [DigimonName.Mojyamon] = EvolutionStage.Champion,
        [DigimonName.Monochromon] = EvolutionStage.Champion,
        [DigimonName.Monzaemon] = EvolutionStage.Ultimate,
        [DigimonName.Nanimon] = EvolutionStage.Champion,
        [DigimonName.Ninjamon] = EvolutionStage.Champion,
        [DigimonName.Numemon] = EvolutionStage.Champion,
        [DigimonName.Ogremon] = EvolutionStage.Champion,
        [DigimonName.Palmon] = EvolutionStage.Rookie,
        [DigimonName.Patamon] = EvolutionStage.Rookie,
        [DigimonName.Penguinmon] = EvolutionStage.Rookie,
        [DigimonName.Phoenixmon] = EvolutionStage.Ultimate,
        [DigimonName.Piximon] = EvolutionStage.Ultimate,
        [DigimonName.Poyomon] = EvolutionStage.Fresh,
        [DigimonName.Punimon] = EvolutionStage.Fresh,
        [DigimonName.Seadramon] = EvolutionStage.Champion,
        [DigimonName.Shellmon] = EvolutionStage.Champion,
        [DigimonName.SkullGreymon] = EvolutionStage.Ultimate,
        [DigimonName.Sukamon] = EvolutionStage.Champion,
        [DigimonName.Tanemon] = EvolutionStage.InTraining,
        [DigimonName.Tokomon] = EvolutionStage.InTraining,
        [DigimonName.Tsunomon] = EvolutionStage.InTraining,
        [DigimonName.Tyrannomon] = EvolutionStage.Champion,
        [DigimonName.Unimon] = EvolutionStage.Champion,
        [DigimonName.Vademon] = EvolutionStage.Ultimate,
        [DigimonName.Vegiemon] = EvolutionStage.Champion,
        [DigimonName.Whamon] = EvolutionStage.Champion,
        [DigimonName.Yuramon] = EvolutionStage.Fresh,
        [DigimonName.Weregarurumon] = EvolutionStage.Ultimate,
        [DigimonName.Gigadramon] = EvolutionStage.Ultimate,
        [DigimonName.MetalEtemon] = EvolutionStage.Ultimate,
        [DigimonName.Machinedramon] = EvolutionStage.Ultimate,
        [DigimonName.Myotismon] = EvolutionStage.Ultimate,
        [DigimonName.Panjyamon] = EvolutionStage.Champion
    };

    public static EvolutionStage Get(DigimonName digimonName)
    {
        if (EvolutionStageMappings.TryGetValue(digimonName, out EvolutionStage evolutionStage))
        {
            return evolutionStage;
        }

        throw new KeyNotFoundException($"Evolution stage mapping for {digimonName} was not found in {nameof(EvolutionStageMapper)}");
    }
}