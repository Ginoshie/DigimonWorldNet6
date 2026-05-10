using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.Models;

/// <summary>
/// Represents a special evolution that is available through non-standard means.
/// </summary>
public sealed class SpecialEvolutionInfo(DigimonName target, string description, string iconPath)
{
    public DigimonName Target { get; } = target;
    public string Name { get; } = target.ToString();
    public string Description { get; } = description;
    public string IconPath { get; } = iconPath;
}
