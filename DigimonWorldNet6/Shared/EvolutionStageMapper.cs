using Generics.Configuration;
using Generics.Enums;
using Generics.Services;

namespace Generics;

public sealed class EvolutionStageMapper
{
    private readonly Dictionary<DigimonType, EvolutionStage> _originalEvolutionStageMappings;
    private readonly Dictionary<DigimonType, EvolutionStage> _viceEvolutionStageMappings;
    private readonly Dictionary<DigimonType, EvolutionStage> _viceMyotismonEvolutionStageMappings;
    private readonly Dictionary<DigimonType, EvolutionStage> _vicePanjyamonEvolutionStageMappings;

    private Dictionary<DigimonType, EvolutionStage> _evolutionStageMappings;

    public EvolutionStageMapper()
    {
        _originalEvolutionStageMappings = CreateOriginalEvolutionStageMappings();
        _viceEvolutionStageMappings = CreateViceEvolutionStageMappings();
        _viceMyotismonEvolutionStageMappings = CreateViceMyotismonEvolutionStageMappings();
        _vicePanjyamonEvolutionStageMappings = CreateVicePanjyamonEvolutionStageMappings();

        _evolutionStageMappings = _originalEvolutionStageMappings;

        UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(OnEvolutionCalculatorConfigChanged);
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

    private Dictionary<DigimonType, EvolutionStage> CreateOriginalEvolutionStageMappings() =>
        new()
        {
            [DigimonType.Agumon] = EvolutionStage.Rookie,
            [DigimonType.Airdramon] = EvolutionStage.Champion,
            [DigimonType.Andromon] = EvolutionStage.Ultimate,
            [DigimonType.Angemon] = EvolutionStage.Champion,
            [DigimonType.Bakemon] = EvolutionStage.Champion,
            [DigimonType.Betamon] = EvolutionStage.Rookie,
            [DigimonType.Birdramon] = EvolutionStage.Champion,
            [DigimonType.Biyomon] = EvolutionStage.Rookie,
            [DigimonType.Botamon] = EvolutionStage.Fresh,
            [DigimonType.Centarumon] = EvolutionStage.Champion,
            [DigimonType.Coelamon] = EvolutionStage.Champion,
            [DigimonType.Devimon] = EvolutionStage.Champion,
            [DigimonType.Digitamamon] = EvolutionStage.Ultimate,
            [DigimonType.Drimogemon] = EvolutionStage.Champion,
            [DigimonType.Elecmon] = EvolutionStage.Rookie,
            [DigimonType.Etemon] = EvolutionStage.Ultimate,
            [DigimonType.Frigimon] = EvolutionStage.Champion,
            [DigimonType.Gabumon] = EvolutionStage.Rookie,
            [DigimonType.Garurumon] = EvolutionStage.Champion,
            [DigimonType.Giromon] = EvolutionStage.Ultimate,
            [DigimonType.Greymon] = EvolutionStage.Champion,
            [DigimonType.HerculesKabuterimon] = EvolutionStage.Ultimate,
            [DigimonType.Kabuterimon] = EvolutionStage.Champion,
            [DigimonType.Kokatorimon] = EvolutionStage.Champion,
            [DigimonType.Koromon] = EvolutionStage.InTraining,
            [DigimonType.Kunemon] = EvolutionStage.Rookie,
            [DigimonType.Kuwagamon] = EvolutionStage.Champion,
            [DigimonType.Leomon] = EvolutionStage.Champion,
            [DigimonType.Mamemon] = EvolutionStage.Champion,
            [DigimonType.Megadramon] = EvolutionStage.Ultimate,
            [DigimonType.MegaSeadramon] = EvolutionStage.Ultimate,
            [DigimonType.Meramon] = EvolutionStage.Champion,
            [DigimonType.MetalGreymon] = EvolutionStage.Ultimate,
            [DigimonType.MetalMamemon] = EvolutionStage.Ultimate,
            [DigimonType.Mojyamon] = EvolutionStage.Champion,
            [DigimonType.Monochromon] = EvolutionStage.Champion,
            [DigimonType.Monzaemon] = EvolutionStage.Ultimate,
            [DigimonType.Nanimon] = EvolutionStage.Champion,
            [DigimonType.Ninjamon] = EvolutionStage.Champion,
            [DigimonType.Numemon] = EvolutionStage.Champion,
            [DigimonType.Ogremon] = EvolutionStage.Champion,
            [DigimonType.Palmon] = EvolutionStage.Rookie,
            [DigimonType.Patamon] = EvolutionStage.Rookie,
            [DigimonType.Penguinmon] = EvolutionStage.Rookie,
            [DigimonType.Phoenixmon] = EvolutionStage.Ultimate,
            [DigimonType.Piximon] = EvolutionStage.Champion,
            [DigimonType.Poyomon] = EvolutionStage.Fresh,
            [DigimonType.Punimon] = EvolutionStage.Fresh,
            [DigimonType.Seadramon] = EvolutionStage.Champion,
            [DigimonType.Shellmon] = EvolutionStage.Champion,
            [DigimonType.SkullGreymon] = EvolutionStage.Ultimate,
            [DigimonType.Sukamon] = EvolutionStage.Champion,
            [DigimonType.Tanemon] = EvolutionStage.InTraining,
            [DigimonType.Tokomon] = EvolutionStage.InTraining,
            [DigimonType.Tsunomon] = EvolutionStage.InTraining,
            [DigimonType.Tyrannomon] = EvolutionStage.Champion,
            [DigimonType.Unimon] = EvolutionStage.Champion,
            [DigimonType.Vademon] = EvolutionStage.Ultimate,
            [DigimonType.Vegiemon] = EvolutionStage.Champion,
            [DigimonType.Whamon] = EvolutionStage.Champion,
            [DigimonType.Yuramon] = EvolutionStage.Fresh
        };

    private Dictionary<DigimonType, EvolutionStage> CreateViceEvolutionStageMappings()
    {
        Dictionary<DigimonType, EvolutionStage> viceEvolutionStageMappings = new(_originalEvolutionStageMappings)
        {
            [DigimonType.Weregarurumon] = EvolutionStage.Ultimate,
            [DigimonType.Gigadramon] = EvolutionStage.Ultimate,
            [DigimonType.MetalEtemon] = EvolutionStage.Ultimate,
            [DigimonType.Machinedramon] = EvolutionStage.Ultimate
        };

        return viceEvolutionStageMappings;
    }

    private Dictionary<DigimonType, EvolutionStage> CreateViceMyotismonEvolutionStageMappings()
    {
        Dictionary<DigimonType, EvolutionStage> viceMyotismonEvolutionStageMappings = new(_viceEvolutionStageMappings)
        {
            [DigimonType.Myotismon] = EvolutionStage.Ultimate
        };

        viceMyotismonEvolutionStageMappings.Remove(DigimonType.Machinedramon);

        return viceMyotismonEvolutionStageMappings;
    }

    private Dictionary<DigimonType, EvolutionStage> CreateVicePanjyamonEvolutionStageMappings()
    {
        Dictionary<DigimonType, EvolutionStage> viceMyotismonEvolutionStageMappings = new(_viceEvolutionStageMappings)
        {
            [DigimonType.Panjyamon] = EvolutionStage.Champion
        };

        viceMyotismonEvolutionStageMappings.Remove(DigimonType.Weregarurumon);

        return viceMyotismonEvolutionStageMappings;
    }

    private void SetOriginalEvolutionStageMappings() => _evolutionStageMappings = _originalEvolutionStageMappings;

    private void SetViceEvolutionStageMappings() => _evolutionStageMappings = _viceEvolutionStageMappings;

    private void SetViceMyotismonEvolutionStageMappings() => _evolutionStageMappings = _viceMyotismonEvolutionStageMappings;

    private void SetVicePanjyamonEvolutionStageMappings() => _evolutionStageMappings = _vicePanjyamonEvolutionStageMappings;
}