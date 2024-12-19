using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.UserControls.FlaggedEvolutionPicker;

public class FlaggedEvolutionPickerViewModel : INotifyPropertyChanged
{
    private bool _componentIsOpen;

    public bool ComponentIsOpen
    {
        get => _componentIsOpen;
        set
        {
            if (_componentIsOpen == value) return;

            _componentIsOpen = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> FreshStageIcons { get; } =
    [
        "Images/Icons/Digimon/botamon-icon.jpg",
        "Images/Icons/Digimon/poyomon-icon.jpg",
        "Images/Icons/Digimon/punimon-icon.jpg",
        "Images/Icons/Digimon/yuramon-icon.jpg"
    ];

    public ObservableCollection<string> InTrainingStageIcons { get; } =
    [
        "Images/Icons/Digimon/koromon-icon.jpg",
        "Images/Icons/Digimon/tokomon-icon.jpg",
        "Images/Icons/Digimon/tsunomon-icon.jpg",
        "Images/Icons/Digimon/tanemon-icon.jpg"
    ];

    public ObservableCollection<string> RookieStageIcons { get; } =
    [
        "Images/Icons/Digimon/agumon-icon.jpg",
        "Images/Icons/Digimon/gabumon-icon.jpg",
        "Images/Icons/Digimon/patamon-icon.jpg",
        "Images/Icons/Digimon/elecmon-icon.jpg",
        "Images/Icons/Digimon/biyomon-icon.jpg",
        "Images/Icons/Digimon/kunemon-icon.jpg",
        "Images/Icons/Digimon/palmon-icon.jpg",
        "Images/Icons/Digimon/betamon-icon.jpg",
        "Images/Icons/Digimon/penguinmon-icon.jpg"
    ];

    public ObservableCollection<string> ChampionStageIcons { get; } =
    [
        "Images/Icons/Digimon/greymon-icon.jpg",
        "Images/Icons/Digimon/monochromon-icon.jpg",
        "Images/Icons/Digimon/ogremon-icon.jpg",
        "Images/Icons/Digimon/airdramon-icon.jpg",
        "Images/Icons/Digimon/kuwagamon-icon.jpg",
        "Images/Icons/Digimon/whamon-icon.jpg",
        "Images/Icons/Digimon/frigimon-icon.jpg",
        "Images/Icons/Digimon/nanimon-icon.jpg",
        "Images/Icons/Digimon/meramon-icon.jpg",
        "Images/Icons/Digimon/drimogemon-icon.jpg",
        "Images/Icons/Digimon/leomon-icon.jpg",
        "Images/Icons/Digimon/kokatorimon-icon.jpg",
        "Images/Icons/Digimon/vegiemon-icon.jpg",
        "Images/Icons/Digimon/shellmon-icon.jpg",
        "Images/Icons/Digimon/mojyamon-icon.jpg",
        "Images/Icons/Digimon/birdramon-icon.jpg",
        "Images/Icons/Digimon/tyrannomon-icon.jpg",
        "Images/Icons/Digimon/angemon-icon.jpg",
        "Images/Icons/Digimon/unimon-icon.jpg",
        "Images/Icons/Digimon/ninjamon-icon.jpg",
        "Images/Icons/Digimon/coelamon-icon.jpg",
        "Images/Icons/Digimon/numemon-icon.jpg",
        "Images/Icons/Digimon/centarumon-icon.jpg",
        "Images/Icons/Digimon/devimon-icon.jpg",
        "Images/Icons/Digimon/bakemon-icon.jpg",
        "Images/Icons/Digimon/kabuterimon-icon.jpg",
        "Images/Icons/Digimon/seadramon-icon.jpg",
        "Images/Icons/Digimon/garurumon-icon.jpg",
        "Images/Icons/Digimon/sukamon-icon.jpg"
    ];

    public ObservableCollection<string> UltimateStageIcons { get; } =
    [
        "Images/Icons/Digimon/metalgreymon-icon.jpg",
        "Images/Icons/Digimon/skullgreymon-icon.jpg",
        "Images/Icons/Digimon/giromon-icon.jpg",
        "Images/Icons/Digimon/h-kabuterimon-icon.jpg",
        "Images/Icons/Digimon/mamemon-icon.jpg",
        "Images/Icons/Digimon/megaseadramon-icon.jpg",
        "Images/Icons/Digimon/vademon-icon.jpg",
        "Images/Icons/Digimon/etemon-icon.jpg",
        "Images/Icons/Digimon/andromon-icon.jpg",
        "Images/Icons/Digimon/megadramon-icon.jpg",
        "Images/Icons/Digimon/phoenixmon-icon.jpg",
        "Images/Icons/Digimon/piximon-icon.jpg",
        "Images/Icons/Digimon/metalmamemon-icon.jpg",
        "Images/Icons/Digimon/monzaemon-icon.jpg",
        "Images/Icons/Digimon/digitamamon-icon.jpg",
    ];

    public IEnumerable<DigimonType> FlaggedDigimonTypes { get; set; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}