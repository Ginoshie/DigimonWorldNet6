namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public sealed class InventoryItemOption(int id, string name)
{
    public int Id { get; } = id;

    public string Name { get; } = name;

    public override string ToString() => Name;
}
