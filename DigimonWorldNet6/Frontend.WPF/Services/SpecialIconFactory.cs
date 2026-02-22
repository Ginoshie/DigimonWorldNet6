using DigimonWorld.Frontend.WPF.Models;
using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Services;

public static class SpecialIconFactory
{
    private const string BASE_PATH = "/Frontend.WPF;component/Images/Icons/Techniques/";

    public static SpecialIcon Create(Special specialIcon) =>
        new(specialIcon, IconPath(specialIcon));

    private static string IconPath(Special special) =>
        $"{BASE_PATH}{special.ToString().ToLower()}-icon-1-button.jpg";
}
