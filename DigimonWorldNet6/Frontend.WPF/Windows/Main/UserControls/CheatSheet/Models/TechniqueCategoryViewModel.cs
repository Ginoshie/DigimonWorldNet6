using System.Collections.Generic;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public sealed class TechniqueCategoryViewModel(string name, IReadOnlyList<MemoryValueViewModel> techniques)
{
    public string Name { get; } = name;

    public string IconPath { get; } = $"/Images/Icons/Techniques/{name.ToLowerInvariant()}-icon-1.png";

    public IReadOnlyList<MemoryValueViewModel> Techniques { get; } = techniques;
}
