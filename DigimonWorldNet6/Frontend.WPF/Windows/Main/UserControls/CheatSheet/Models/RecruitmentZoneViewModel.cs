using System.Collections.Generic;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public sealed class RecruitmentZoneViewModel(string name, IReadOnlyList<MemoryValueViewModel> recruitments)
{
    public string Name { get; } = name;

    public IReadOnlyList<MemoryValueViewModel> Recruitments { get; } = recruitments;
}
