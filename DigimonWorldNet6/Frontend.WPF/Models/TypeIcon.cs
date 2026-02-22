using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Models;

public readonly struct TypeIcon(Type type, string iconPath)
{
    public Type Type { get; } = type;
    public string IconPath { get; } = iconPath;
}