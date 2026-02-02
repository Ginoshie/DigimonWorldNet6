using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Models;

public readonly struct DigimonIcon
{
    public DigimonName DigimonName { get; }
    public string IconPath { get; }

    public DigimonIcon(DigimonName digimonName, string iconPath)
    {
        DigimonName = digimonName;
        IconPath = iconPath;
    }
}