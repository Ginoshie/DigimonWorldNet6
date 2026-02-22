using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Models;

public readonly struct SpecialIcon(Special special, string iconPath)
{
    public Special Special { get; } = special;
    public string IconPath { get; } = iconPath;
}