using Generics.Configuration;
using Generics.Enums;
using Generics.Services;

namespace Generics;

public sealed class EvolutionStageMapper
{
    private readonly Dictionary<DigimonName, EvolutionStage> _originalEvolutionStageMappings;
    private readonly Dictionary<DigimonName, EvolutionStage> _viceEvolutionStageMappings;
    private readonly Dictionary<DigimonName, EvolutionStage> _viceMyotismonEvolutionStageMappings;
    private readonly Dictionary<DigimonName, EvolutionStage> _vicePanjyamonEvolutionStageMappings;

    private Dictionary<DigimonName, EvolutionStage> _evolutionStageMappings;

    public EvolutionStageMapper()
    {
        _originalEvolutionStageMappings = CreateOriginalEvolutionStageMappings();
        _viceEvolutionStageMappings = CreateViceEvolutionStageMappings();
        _viceMyotismonEvolutionStageMappings = CreateViceMyotismonEvolutionStageMappings();
        _vicePanjyamonEvolutionStageMappings = CreateVicePanjyamonEvolutionStageMappings();

        _evolutionStageMappings = _originalEvolutionStageMappings;

        UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(OnEvolutionCalculatorConfigChanged);
    }

    public EvolutionStage this[DigimonName evolutionResult]
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

    private void OnEvolutionCalculatorConfigChanged(EvolutionCalculatorConfig evolutionCalculatorConfig)
    {
        switch (evolutionCalculatorConfig.EvolutionCalculatorMode)
        {
            case EvolutionCalculatorMode.Original:
                SetOriginalEvolutionStageMappings();
                break;
            case EvolutionCalculatorMode.Vice:
                SetViceEvolutionStageMappings();
                break;
            case EvolutionCalculatorMode.ViceMyotismon:
                SetViceMyotismonEvolutionStageMappings();
                break;
            case EvolutionCalculatorMode.VicePanjyamon:
                SetVicePanjyamonEvolutionStageMappings();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(evolutionCalculatorConfig.EvolutionCalculatorMode));
        }
    }

    private Dictionary<DigimonName, EvolutionStage> CreateOriginalEvolutionStageMappings() =>
        new()
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
            [DigimonName.Mamemon] = EvolutionStage.Champion,
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
            [DigimonName.Piximon] = EvolutionStage.Champion,
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
            [DigimonName.Yuramon] = EvolutionStage.Fresh
        };

    private Dictionary<DigimonName, EvolutionStage> CreateViceEvolutionStageMappings()
    {
        Dictionary<DigimonName, EvolutionStage> viceEvolutionStageMappings = new(_originalEvolutionStageMappings)
        {
            [DigimonName.Weregarurumon] = EvolutionStage.Ultimate,
            [DigimonName.Gigadramon] = EvolutionStage.Ultimate,
            [DigimonName.MetalEtemon] = EvolutionStage.Ultimate,
            [DigimonName.Machinedramon] = EvolutionStage.Ultimate
        };

        return viceEvolutionStageMappings;
    }

    private Dictionary<DigimonName, EvolutionStage> CreateViceMyotismonEvolutionStageMappings()
    {
        Dictionary<DigimonName, EvolutionStage> viceMyotismonEvolutionStageMappings = new(_viceEvolutionStageMappings)
        {
            [DigimonName.Myotismon] = EvolutionStage.Ultimate
        };

        viceMyotismonEvolutionStageMappings.Remove(DigimonName.Machinedramon);

        return viceMyotismonEvolutionStageMappings;
    }

    private Dictionary<DigimonName, EvolutionStage> CreateVicePanjyamonEvolutionStageMappings()
    {
        Dictionary<DigimonName, EvolutionStage> viceMyotismonEvolutionStageMappings = new(_viceEvolutionStageMappings)
        {
            [DigimonName.Panjyamon] = EvolutionStage.Champion
        };

        viceMyotismonEvolutionStageMappings.Remove(DigimonName.Weregarurumon);

        return viceMyotismonEvolutionStageMappings;
    }

    private void SetOriginalEvolutionStageMappings() => _evolutionStageMappings = _originalEvolutionStageMappings;

    private void SetViceEvolutionStageMappings() => _evolutionStageMappings = _viceEvolutionStageMappings;

    private void SetViceMyotismonEvolutionStageMappings() => _evolutionStageMappings = _viceMyotismonEvolutionStageMappings;

    private void SetVicePanjyamonEvolutionStageMappings() => _evolutionStageMappings = _vicePanjyamonEvolutionStageMappings;
}