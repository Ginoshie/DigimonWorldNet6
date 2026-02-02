using System.Collections.ObjectModel;
using System.Linq;
using DigimonWorld.Frontend.WPF.Models;
using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Services;

public static class DigimonIconFactory
{
    private const string BASE_PATH = "/Frontend.WPF;component/Images/Icons/Digimon/";

    public static ObservableCollection<DigimonIcon> Create(params DigimonName[] digimon) =>
        new(
            digimon.Select(d =>
                new DigimonIcon(d, IconPath(d)))
        );

    public static DigimonIcon Create(DigimonName digimon) =>
        new(digimon, IconPath(digimon));

    private static string IconPath(DigimonName name) =>
        $"{BASE_PATH}{name.ToString().ToLower()}-icon.jpg";
}
