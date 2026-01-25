using System.Collections.ObjectModel;
using DigimonWorld.Frontend.WPF.Models;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.Constants;

public static class EvolutionIcons
{
    public static ObservableCollection<DigimonIcon> FreshStageIcons { get; } =
    [
        new (DigimonName.Botamon, "/Frontend.WPF;component/Images/Icons/Digimon/botamon-icon.jpg"),
        new (DigimonName.Poyomon, "/Frontend.WPF;component/Images/Icons/Digimon/poyomon-icon.jpg"),
        new (DigimonName.Punimon, "/Frontend.WPF;component/Images/Icons/Digimon/punimon-icon.jpg"),
        new (DigimonName.Yuramon, "/Frontend.WPF;component/Images/Icons/Digimon/yuramon-icon.jpg")
    ];

    public static ObservableCollection<DigimonIcon> InTrainingStageIcons { get; } =
    [
        new (DigimonName.Koromon, "/Frontend.WPF;component/Images/Icons/Digimon/koromon-icon.jpg"),
        new (DigimonName.Tokomon, "/Frontend.WPF;component/Images/Icons/Digimon/tokomon-icon.jpg"),
        new (DigimonName.Tsunomon, "/Frontend.WPF;component/Images/Icons/Digimon/tsunomon-icon.jpg"),
        new (DigimonName.Tanemon, "/Frontend.WPF;component/Images/Icons/Digimon/tanemon-icon.jpg")
    ];

    public static ObservableCollection<DigimonIcon> RookieStageIcons { get; } =
    [
        new (DigimonName.Agumon, "/Frontend.WPF;component/Images/Icons/Digimon/agumon-icon.jpg"),
        new (DigimonName.Gabumon, "/Frontend.WPF;component/Images/Icons/Digimon/gabumon-icon.jpg"),
        new (DigimonName.Patamon, "/Frontend.WPF;component/Images/Icons/Digimon/patamon-icon.jpg"),
        new (DigimonName.Elecmon, "/Frontend.WPF;component/Images/Icons/Digimon/elecmon-icon.jpg"),
        new (DigimonName.Biyomon, "/Frontend.WPF;component/Images/Icons/Digimon/biyomon-icon.jpg"),
        new (DigimonName.Kunemon, "/Frontend.WPF;component/Images/Icons/Digimon/kunemon-icon.jpg"),
        new (DigimonName.Palmon, "/Frontend.WPF;component/Images/Icons/Digimon/palmon-icon.jpg"),
        new (DigimonName.Betamon, "/Frontend.WPF;component/Images/Icons/Digimon/betamon-icon.jpg"),
        new (DigimonName.Penguinmon, "/Frontend.WPF;component/Images/Icons/Digimon/penguinmon-icon.jpg")
    ];

    public static ObservableCollection<DigimonIcon> ChampionStageIcons { get; } =
    [
        new (DigimonName.Greymon, "/Frontend.WPF;component/Images/Icons/Digimon/greymon-icon.jpg"),
        new (DigimonName.Monochromon, "/Frontend.WPF;component/Images/Icons/Digimon/monochromon-icon.jpg"),
        new (DigimonName.Ogremon, "/Frontend.WPF;component/Images/Icons/Digimon/ogremon-icon.jpg"),
        new (DigimonName.Airdramon, "/Frontend.WPF;component/Images/Icons/Digimon/airdramon-icon.jpg"),
        new (DigimonName.Kuwagamon, "/Frontend.WPF;component/Images/Icons/Digimon/kuwagamon-icon.jpg"),
        new (DigimonName.Whamon, "/Frontend.WPF;component/Images/Icons/Digimon/whamon-icon.jpg"),
        new (DigimonName.Frigimon, "/Frontend.WPF;component/Images/Icons/Digimon/frigimon-icon.jpg"),
        new (DigimonName.Nanimon, "/Frontend.WPF;component/Images/Icons/Digimon/nanimon-icon.jpg"),
        new (DigimonName.Meramon, "/Frontend.WPF;component/Images/Icons/Digimon/meramon-icon.jpg"),
        new (DigimonName.Drimogemon, "/Frontend.WPF;component/Images/Icons/Digimon/drimogemon-icon.jpg"),
        new (DigimonName.Leomon, "/Frontend.WPF;component/Images/Icons/Digimon/leomon-icon.jpg"),
        new (DigimonName.Kokatorimon, "/Frontend.WPF;component/Images/Icons/Digimon/kokatorimon-icon.jpg"),
        new (DigimonName.Vegiemon, "/Frontend.WPF;component/Images/Icons/Digimon/vegiemon-icon.jpg"),
        new (DigimonName.Shellmon, "/Frontend.WPF;component/Images/Icons/Digimon/shellmon-icon.jpg"),
        new (DigimonName.Mojyamon, "/Frontend.WPF;component/Images/Icons/Digimon/mojyamon-icon.jpg"),
        new (DigimonName.Birdramon, "/Frontend.WPF;component/Images/Icons/Digimon/birdramon-icon.jpg"),
        new (DigimonName.Tyrannomon, "/Frontend.WPF;component/Images/Icons/Digimon/tyrannomon-icon.jpg"),
        new (DigimonName.Angemon, "/Frontend.WPF;component/Images/Icons/Digimon/angemon-icon.jpg"),
        new (DigimonName.Unimon, "/Frontend.WPF;component/Images/Icons/Digimon/unimon-icon.jpg"),
        new (DigimonName.Ninjamon, "/Frontend.WPF;component/Images/Icons/Digimon/ninjamon-icon.jpg"),
        new (DigimonName.Coelamon, "/Frontend.WPF;component/Images/Icons/Digimon/coelamon-icon.jpg"),
        new (DigimonName.Numemon, "/Frontend.WPF;component/Images/Icons/Digimon/numemon-icon.jpg"),
        new (DigimonName.Centarumon, "/Frontend.WPF;component/Images/Icons/Digimon/centarumon-icon.jpg"),
        new (DigimonName.Devimon, "/Frontend.WPF;component/Images/Icons/Digimon/devimon-icon.jpg"),
        new (DigimonName.Bakemon, "/Frontend.WPF;component/Images/Icons/Digimon/bakemon-icon.jpg"),
        new (DigimonName.Kabuterimon, "/Frontend.WPF;component/Images/Icons/Digimon/kabuterimon-icon.jpg"),
        new (DigimonName.Seadramon, "/Frontend.WPF;component/Images/Icons/Digimon/seadramon-icon.jpg"),
        new (DigimonName.Garurumon, "/Frontend.WPF;component/Images/Icons/Digimon/garurumon-icon.jpg"),
        new (DigimonName.Sukamon, "/Frontend.WPF;component/Images/Icons/Digimon/sukamon-icon.jpg")
    ];

    public static ObservableCollection<DigimonIcon> UltimateStageIcons { get; } =
    [
        new (DigimonName.MetalGreymon, "/Frontend.WPF;component/Images/Icons/Digimon/metalgreymon-icon.jpg"),
        new (DigimonName.SkullGreymon, "/Frontend.WPF;component/Images/Icons/Digimon/skullgreymon-icon.jpg"),
        new (DigimonName.Giromon, "/Frontend.WPF;component/Images/Icons/Digimon/giromon-icon.jpg"),
        new (DigimonName.HerculesKabuterimon, "/Frontend.WPF;component/Images/Icons/Digimon/h-kabuterimon-icon.jpg"),
        new (DigimonName.Mamemon, "/Frontend.WPF;component/Images/Icons/Digimon/mamemon-icon.jpg"),
        new (DigimonName.MegaSeadramon, "/Frontend.WPF;component/Images/Icons/Digimon/megaseadramon-icon.jpg"),
        new (DigimonName.Vademon, "/Frontend.WPF;component/Images/Icons/Digimon/vademon-icon.jpg"),
        new (DigimonName.Etemon, "/Frontend.WPF;component/Images/Icons/Digimon/etemon-icon.jpg"),
        new (DigimonName.Andromon, "/Frontend.WPF;component/Images/Icons/Digimon/andromon-icon.jpg"),
        new (DigimonName.Megadramon, "/Frontend.WPF;component/Images/Icons/Digimon/megadramon-icon.jpg"),
        new (DigimonName.Phoenixmon, "/Frontend.WPF;component/Images/Icons/Digimon/phoenixmon-icon.jpg"),
        new (DigimonName.Piximon, "/Frontend.WPF;component/Images/Icons/Digimon/piximon-icon.jpg"),
        new (DigimonName.MetalMamemon, "/Frontend.WPF;component/Images/Icons/Digimon/metalmamemon-icon.jpg"),
        new (DigimonName.Monzaemon, "/Frontend.WPF;component/Images/Icons/Digimon/monzaemon-icon.jpg"),
        new (DigimonName.Digitamamon, "/Frontend.WPF;component/Images/Icons/Digimon/digitamamon-icon.jpg")
    ];
}
