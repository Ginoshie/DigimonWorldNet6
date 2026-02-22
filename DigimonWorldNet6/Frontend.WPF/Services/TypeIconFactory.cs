using DigimonWorld.Frontend.WPF.Models;
using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Services;

public static class TypeIconFactory
{
    private const string BASE_PATH = "/Frontend.WPF;component/Images/Icons/Type/";

    public static TypeIcon Create(Type type) => new(type, IconPath(type));

    private static string IconPath(Type type) =>
        $"{BASE_PATH}{type.ToString().ToLower()}-type-icon-button.jpg";
}
