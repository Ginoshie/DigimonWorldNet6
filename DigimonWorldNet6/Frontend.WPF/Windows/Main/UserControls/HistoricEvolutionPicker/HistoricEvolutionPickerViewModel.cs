using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows.Input;
using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Frontend.WPF.Models;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Shared.Configuration;
using Shared.Enums;
using Shared.Services;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.HistoricEvolutionPicker;

public class HistoricEvolutionPickerViewModel : BaseViewModel, IDisposable
{
    private readonly CompositeDisposable? _disposables;

    public HistoricEvolutionPickerViewModel()
    {
        HistoricEvolutionClickedCommand =
            new RelayCommand<DigimonIcon>(UpdateHistoricEvolution);

        _disposables = new CompositeDisposable
        {
            UserConfigurationManager.CurrentEvolutionCalculatorConfig
                .Subscribe(OnCurrentEvolutionCalculatorConfigChanged)
        };
    }

    public ICommand HistoricEvolutionClickedCommand { get; }

    public IList<DigimonName> HistoricEvolutions => Session.HistoricEvolutions;

    public ObservableCollection<DigimonIcon> FreshStageIcons { get; private set; } = DigimonIconFactory.Create(FreshStageDigimonNames);

    public ObservableCollection<DigimonIcon> InTrainingStageIcons { get; private set; } = DigimonIconFactory.Create(InTrainingStageDigimonNames);

    public ObservableCollection<DigimonIcon> RookieStageIcons { get; private set; } = DigimonIconFactory.Create(RookieStageDigimonNames);

    public ObservableCollection<DigimonIcon> ChampionStageIcons
    {
        get;
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    } = DigimonIconFactory.Create(ChampionStageDigimonNames);

    public ObservableCollection<DigimonIcon> UltimateStageIcons
    {
        get;
        private set
        {
            field = value;
            OnPropertyChanged();
        }
    } = DigimonIconFactory.Create(UltimateStageDigimonNames);

    private static readonly DigimonName[] FreshStageDigimonNames =
    [
        DigimonName.Botamon,
        DigimonName.Poyomon,
        DigimonName.Punimon,
        DigimonName.Yuramon
    ];

    private static readonly DigimonName[] InTrainingStageDigimonNames =
    [
        DigimonName.Koromon,
        DigimonName.Tokomon,
        DigimonName.Tsunomon,
        DigimonName.Tanemon
    ];

    private static readonly DigimonName[] RookieStageDigimonNames =
    [
        DigimonName.Agumon,
        DigimonName.Gabumon,
        DigimonName.Patamon,
        DigimonName.Elecmon,
        DigimonName.Biyomon,
        DigimonName.Kunemon,
        DigimonName.Palmon,
        DigimonName.Betamon,
        DigimonName.Penguinmon
    ];

    private static readonly DigimonName[] ChampionStageDigimonNames =
    [
        DigimonName.Greymon,
        DigimonName.Monochromon,
        DigimonName.Ogremon,
        DigimonName.Airdramon,
        DigimonName.Kuwagamon,
        DigimonName.Whamon,
        DigimonName.Frigimon,
        DigimonName.Nanimon,
        DigimonName.Meramon,
        DigimonName.Drimogemon,
        DigimonName.Leomon,
        DigimonName.Kokatorimon,
        DigimonName.Vegiemon,
        DigimonName.Shellmon,
        DigimonName.Mojyamon,
        DigimonName.Birdramon,
        DigimonName.Tyrannomon,
        DigimonName.Angemon,
        DigimonName.Unimon,
        DigimonName.Ninjamon,
        DigimonName.Coelamon,
        DigimonName.Numemon,
        DigimonName.Centarumon,
        DigimonName.Devimon,
        DigimonName.Bakemon,
        DigimonName.Kabuterimon,
        DigimonName.Seadramon,
        DigimonName.Garurumon,
        DigimonName.Sukamon
    ];

    private static readonly DigimonName[] UltimateStageDigimonNames =
    [
        DigimonName.MetalGreymon,
        DigimonName.SkullGreymon,
        DigimonName.Giromon,
        DigimonName.HerculesKabuterimon,
        DigimonName.MetalMamemon,
        DigimonName.MegaSeadramon,
        DigimonName.Vademon,
        DigimonName.Etemon,
        DigimonName.Andromon,
        DigimonName.Megadramon,
        DigimonName.Phoenixmon,
        DigimonName.Piximon,
        DigimonName.Mamemon,
        DigimonName.Monzaemon,
        DigimonName.Digitamamon
    ];

    private static readonly DigimonName[] ViceUltimateStageDigimonNames = UltimateStageDigimonNames.Concat([
        DigimonName.Weregarurumon,
        DigimonName.Gigadramon,
        DigimonName.MetalEtemon,
        DigimonName.Machinedramon
    ]).ToArray();

    private void UpdateHistoricEvolution(DigimonIcon digimonIcon)
    {
        if (!HistoricEvolutions.Remove(digimonIcon.DigimonName))
        {
            HistoricEvolutions.Add(digimonIcon.DigimonName);
        }

        OnPropertyChanged(nameof(HistoricEvolutions));
    }

    private void OnCurrentEvolutionCalculatorConfigChanged(
        EvolutionCalculatorConfig config)
    {
        GameVariant mode = config.GameVariant;

        if (mode.HasFlag(GameVariant.Original))
        {
            OnEvolutionCalculatorModeOriginal();
            return;
        }

        if (!mode.HasFlag(GameVariant.Vice))
        {
            return;
        }

        OnEvolutionCalculatorModeVice();

        if (mode.HasFlag(GameVariant.MyotismonPatch))
        {
            OnEvolutionCalculatorModeMyotismon();
        }

        if (mode.HasFlag(GameVariant.PanjyamonPatch))
        {
            OnEvolutionCalculatorModePanjyamon();
        }
    }


    private void OnEvolutionCalculatorModeOriginal()
    {
        ChampionStageIcons = DigimonIconFactory.Create(ChampionStageDigimonNames);

        UltimateStageIcons = DigimonIconFactory.Create(UltimateStageDigimonNames);
    }

    private void OnEvolutionCalculatorModeVice()
    {
        ChampionStageIcons = DigimonIconFactory.Create(ChampionStageDigimonNames);

        UltimateStageIcons = DigimonIconFactory.Create(ViceUltimateStageDigimonNames);
    }

    private void OnEvolutionCalculatorModeMyotismon()
    {
        DigimonName[] patchedUltimateDigimonNames = UltimateStageIcons
            .Where(digimonIcon => digimonIcon.DigimonName != DigimonName.Machinedramon)
            .Select(digimonIcon => digimonIcon.DigimonName)
            .Concat([DigimonName.Myotismon])
            .ToArray();

        UltimateStageIcons = DigimonIconFactory.Create(patchedUltimateDigimonNames);
    }

    private void OnEvolutionCalculatorModePanjyamon()
    {
        DigimonName[] patchedChampionDigimonNames = ChampionStageDigimonNames
            .Concat([DigimonName.Panjyamon])
            .ToArray();

        ChampionStageIcons = DigimonIconFactory.Create(patchedChampionDigimonNames);

        DigimonName[] patchedUltimateDigimonNames = UltimateStageIcons
            .Where(digimonIcon => digimonIcon.DigimonName != DigimonName.Weregarurumon)
            .Select(digimonIcon => digimonIcon.DigimonName)
            .ToArray();

        UltimateStageIcons = DigimonIconFactory.Create(patchedUltimateDigimonNames);
    }

    public void Dispose() => _disposables?.Dispose();
}