namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public sealed class EvolveTargetOption(int value, string name)
{
    public int Value { get; } = value;

    public string Name { get; } = name;

    public override string ToString() => $"{Name} ({Value})";
}
