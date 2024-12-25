using System.Collections.ObjectModel;
using DigimonWorld.Frontend.WPF.Models;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.Constants;

public static class EvolutionIcons
{
    public static ObservableCollection<DigimonIcon> FreshStageIcons { get; } =
    [
        new (DigimonType.Botamon, "/Frontend.WPF;component/Images/Icons/Digimon/botamon-icon.jpg"),
        new (DigimonType.Poyomon, "/Frontend.WPF;component/Images/Icons/Digimon/poyomon-icon.jpg"),
        new (DigimonType.Punimon, "/Frontend.WPF;component/Images/Icons/Digimon/punimon-icon.jpg"),
        new (DigimonType.Yuramon, "/Frontend.WPF;component/Images/Icons/Digimon/yuramon-icon.jpg")
    ];

    public static ObservableCollection<DigimonIcon> InTrainingStageIcons { get; } =
    [
        new (DigimonType.Koromon, "/Frontend.WPF;component/Images/Icons/Digimon/koromon-icon.jpg"),
        new (DigimonType.Tokomon, "/Frontend.WPF;component/Images/Icons/Digimon/tokomon-icon.jpg"),
        new (DigimonType.Tsunomon, "/Frontend.WPF;component/Images/Icons/Digimon/tsunomon-icon.jpg"),
        new (DigimonType.Tanemon, "/Frontend.WPF;component/Images/Icons/Digimon/tanemon-icon.jpg")
    ];

    public static ObservableCollection<DigimonIcon> RookieStageIcons { get; } =
    [
        new (DigimonType.Agumon, "/Frontend.WPF;component/Images/Icons/Digimon/agumon-icon.jpg"),
        new (DigimonType.Gabumon, "/Frontend.WPF;component/Images/Icons/Digimon/gabumon-icon.jpg"),
        new (DigimonType.Patamon, "/Frontend.WPF;component/Images/Icons/Digimon/patamon-icon.jpg"),
        new (DigimonType.Elecmon, "/Frontend.WPF;component/Images/Icons/Digimon/elecmon-icon.jpg"),
        new (DigimonType.Biyomon, "/Frontend.WPF;component/Images/Icons/Digimon/biyomon-icon.jpg"),
        new (DigimonType.Kunemon, "/Frontend.WPF;component/Images/Icons/Digimon/kunemon-icon.jpg"),
        new (DigimonType.Palmon, "/Frontend.WPF;component/Images/Icons/Digimon/palmon-icon.jpg"),
        new (DigimonType.Betamon, "/Frontend.WPF;component/Images/Icons/Digimon/betamon-icon.jpg"),
        new (DigimonType.Penguinmon, "/Frontend.WPF;component/Images/Icons/Digimon/penguinmon-icon.jpg")
    ];

    public static ObservableCollection<DigimonIcon> ChampionStageIcons { get; } =
    [
        new (DigimonType.Greymon, "/Frontend.WPF;component/Images/Icons/Digimon/greymon-icon.jpg"),
        new (DigimonType.Monochromon, "/Frontend.WPF;component/Images/Icons/Digimon/monochromon-icon.jpg"),
        new (DigimonType.Ogremon, "/Frontend.WPF;component/Images/Icons/Digimon/ogremon-icon.jpg"),
        new (DigimonType.Airdramon, "/Frontend.WPF;component/Images/Icons/Digimon/airdramon-icon.jpg"),
        new (DigimonType.Kuwagamon, "/Frontend.WPF;component/Images/Icons/Digimon/kuwagamon-icon.jpg"),
        new (DigimonType.Whamon, "/Frontend.WPF;component/Images/Icons/Digimon/whamon-icon.jpg"),
        new (DigimonType.Frigimon, "/Frontend.WPF;component/Images/Icons/Digimon/frigimon-icon.jpg"),
        new (DigimonType.Nanimon, "/Frontend.WPF;component/Images/Icons/Digimon/nanimon-icon.jpg"),
        new (DigimonType.Meramon, "/Frontend.WPF;component/Images/Icons/Digimon/meramon-icon.jpg"),
        new (DigimonType.Drimogemon, "/Frontend.WPF;component/Images/Icons/Digimon/drimogemon-icon.jpg"),
        new (DigimonType.Leomon, "/Frontend.WPF;component/Images/Icons/Digimon/leomon-icon.jpg"),
        new (DigimonType.Kokatorimon, "/Frontend.WPF;component/Images/Icons/Digimon/kokatorimon-icon.jpg"),
        new (DigimonType.Vegiemon, "/Frontend.WPF;component/Images/Icons/Digimon/vegiemon-icon.jpg"),
        new (DigimonType.Shellmon, "/Frontend.WPF;component/Images/Icons/Digimon/shellmon-icon.jpg"),
        new (DigimonType.Mojyamon, "/Frontend.WPF;component/Images/Icons/Digimon/mojyamon-icon.jpg"),
        new (DigimonType.Birdramon, "/Frontend.WPF;component/Images/Icons/Digimon/birdramon-icon.jpg"),
        new (DigimonType.Tyrannomon, "/Frontend.WPF;component/Images/Icons/Digimon/tyrannomon-icon.jpg"),
        new (DigimonType.Angemon, "/Frontend.WPF;component/Images/Icons/Digimon/angemon-icon.jpg"),
        new (DigimonType.Unimon, "/Frontend.WPF;component/Images/Icons/Digimon/unimon-icon.jpg"),
        new (DigimonType.Ninjamon, "/Frontend.WPF;component/Images/Icons/Digimon/ninjamon-icon.jpg"),
        new (DigimonType.Coelamon, "/Frontend.WPF;component/Images/Icons/Digimon/coelamon-icon.jpg"),
        new (DigimonType.Numemon, "/Frontend.WPF;component/Images/Icons/Digimon/numemon-icon.jpg"),
        new (DigimonType.Centarumon, "/Frontend.WPF;component/Images/Icons/Digimon/centarumon-icon.jpg"),
        new (DigimonType.Devimon, "/Frontend.WPF;component/Images/Icons/Digimon/devimon-icon.jpg"),
        new (DigimonType.Bakemon, "/Frontend.WPF;component/Images/Icons/Digimon/bakemon-icon.jpg"),
        new (DigimonType.Kabuterimon, "/Frontend.WPF;component/Images/Icons/Digimon/kabuterimon-icon.jpg"),
        new (DigimonType.Seadramon, "/Frontend.WPF;component/Images/Icons/Digimon/seadramon-icon.jpg"),
        new (DigimonType.Garurumon, "/Frontend.WPF;component/Images/Icons/Digimon/garurumon-icon.jpg"),
        new (DigimonType.Sukamon, "/Frontend.WPF;component/Images/Icons/Digimon/sukamon-icon.jpg")
    ];

    public static ObservableCollection<DigimonIcon> UltimateStageIcons { get; } =
    [
        new (DigimonType.MetalGreymon, "/Frontend.WPF;component/Images/Icons/Digimon/metalgreymon-icon.jpg"),
        new (DigimonType.SkullGreymon, "/Frontend.WPF;component/Images/Icons/Digimon/skullgreymon-icon.jpg"),
        new (DigimonType.Giromon, "/Frontend.WPF;component/Images/Icons/Digimon/giromon-icon.jpg"),
        new (DigimonType.HerculesKabuterimon, "/Frontend.WPF;component/Images/Icons/Digimon/h-kabuterimon-icon.jpg"),
        new (DigimonType.Mamemon, "/Frontend.WPF;component/Images/Icons/Digimon/mamemon-icon.jpg"),
        new (DigimonType.MegaSeadramon, "/Frontend.WPF;component/Images/Icons/Digimon/megaseadramon-icon.jpg"),
        new (DigimonType.Vademon, "/Frontend.WPF;component/Images/Icons/Digimon/vademon-icon.jpg"),
        new (DigimonType.Etemon, "/Frontend.WPF;component/Images/Icons/Digimon/etemon-icon.jpg"),
        new (DigimonType.Andromon, "/Frontend.WPF;component/Images/Icons/Digimon/andromon-icon.jpg"),
        new (DigimonType.Megadramon, "/Frontend.WPF;component/Images/Icons/Digimon/megadramon-icon.jpg"),
        new (DigimonType.Phoenixmon, "/Frontend.WPF;component/Images/Icons/Digimon/phoenixmon-icon.jpg"),
        new (DigimonType.Piximon, "/Frontend.WPF;component/Images/Icons/Digimon/piximon-icon.jpg"),
        new (DigimonType.MetalMamemon, "/Frontend.WPF;component/Images/Icons/Digimon/metalmamemon-icon.jpg"),
        new (DigimonType.Monzaemon, "/Frontend.WPF;component/Images/Icons/Digimon/monzaemon-icon.jpg"),
        new (DigimonType.Digitamamon, "/Frontend.WPF;component/Images/Icons/Digimon/digitamamon-icon.jpg")
    ];
}
