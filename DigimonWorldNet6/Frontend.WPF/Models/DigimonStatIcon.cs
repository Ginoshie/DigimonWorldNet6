using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Models;

public class DigimonStatIcon
{
    public DigimonStatName DigimonStatName { get; }
    public string IconPath { get; }

    public DigimonStatIcon(DigimonStatName digimonStatName, string iconPath)
    {
        DigimonStatName = digimonStatName;
        IconPath = iconPath;
    }
}