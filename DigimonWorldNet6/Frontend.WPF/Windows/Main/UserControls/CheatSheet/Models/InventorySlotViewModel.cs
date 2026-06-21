using System;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

public sealed class InventorySlotViewModel
{
    public InventorySlotViewModel(int slotNumber, Func<int> getType, Action<int> setType, Func<int> getAmount, Action<int> setAmount)
    {
        SlotNumber = slotNumber;
        ItemType = new NumericMemoryValueViewModel($"Slot {slotNumber} Type", getType, setType);
        ItemAmount = new NumericMemoryValueViewModel($"Slot {slotNumber} Amount", getAmount, setAmount);
    }

    public int SlotNumber { get; }

    public NumericMemoryValueViewModel ItemType { get; }

    public NumericMemoryValueViewModel ItemAmount { get; }
}
