using MemoryAccess.Core;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.Tamer;

public sealed class InventoryStats(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    public const int SLOT_COUNT = 30;

    private const int ITEM_TYPE_BASE_OFFSET = 0x0013D474;
    private const int ITEM_AMOUNT_BASE_OFFSET = 0x0013D492;
    private const int INVENTORY_SIZE_OFFSET = 0x0013D4CE;

    private readonly int[] _itemType = new int[SLOT_COUNT];
    private readonly int[] _itemAmount = new int[SLOT_COUNT];
    private int _inventorySize;

    private InventoryStats() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static InventoryStats Empty { get; } = new();

    public int InventorySize
    {
        get => _inventorySize;
        set
        {
            _inventorySize = value;
            mem.WriteByte(ram.A(INVENTORY_SIZE_OFFSET), (byte)value);
        }
    }

    public int GetItemType(int slot) => _itemType[slot];

    public void SetItemType(int slot, int value)
    {
        _itemType[slot] = value;
        mem.WriteByte(ram.A(ITEM_TYPE_BASE_OFFSET + slot), (byte)value);
    }

    public int GetItemAmount(int slot) => _itemAmount[slot];

    public void SetItemAmount(int slot, int value)
    {
        _itemAmount[slot] = value;
        mem.WriteByte(ram.A(ITEM_AMOUNT_BASE_OFFSET + slot), (byte)value);
    }

    protected override void OnUpdateData()
    {
        for (int slot = 0; slot < SLOT_COUNT; slot++)
        {
            _itemType[slot] = mem.ReadByte(ram.A(ITEM_TYPE_BASE_OFFSET + slot));
            _itemAmount[slot] = mem.ReadByte(ram.A(ITEM_AMOUNT_BASE_OFFSET + slot));
        }

        _inventorySize = mem.ReadByte(ram.A(INVENTORY_SIZE_OFFSET));

        EmulatorLinkEventHub.SignalInventorySynchronized();
    }
}
