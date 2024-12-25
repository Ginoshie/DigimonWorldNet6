using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.Models;

public struct DigimonIcon
{
    public DigimonType DigimonType { get; }
    public string IconPath { get; }
    
    public DigimonIcon(DigimonType digimonType, string iconPath)
    {
        DigimonType = digimonType;
        IconPath = iconPath;
    }
}