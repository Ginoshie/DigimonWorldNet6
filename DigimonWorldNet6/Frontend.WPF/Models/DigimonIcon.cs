using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Models;

public readonly struct DigimonIcon(DigimonName digimonName, string iconPath)
{
    public DigimonName DigimonName { get; } = digimonName;
    public string IconPath { get; } = iconPath;
}