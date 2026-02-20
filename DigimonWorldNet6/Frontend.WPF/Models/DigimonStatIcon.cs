using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Models;

public class DigimonStatIcon(DigimonStatName digimonStatName, string iconPath)
{
    public DigimonStatName DigimonStatName { get; } = digimonStatName;
    public string IconPath { get; } = iconPath;
}