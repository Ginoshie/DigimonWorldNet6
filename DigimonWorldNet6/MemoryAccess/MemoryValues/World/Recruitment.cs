using MemoryAccess.Core;
using Shared.Services.Events;

namespace MemoryAccess.MemoryValues.World;

public class Recruitment(ProcessMemory mem, PsxRam ram) : MemoryValueSyncBase
{
    private const int RECRUITMENT_BASE_OFFSET = 0x001BDFE6;

    private readonly byte[] _data = new byte[8];

    private Recruitment() : this(ProcessMemory.Empty, PsxRam.Empty)
    {
    }

    public static Recruitment Empty { get; } = new();

    public bool Agumon
    {
        get => HasBit(0, 3);
        set => SetBit(0, 3, value);
    }

    public bool Betamon
    {
        get => HasBit(0, 4);
        set => SetBit(0, 4, value);
    }

    public bool Greymon
    {
        get => HasBit(0, 5);
        set => SetBit(0, 5, value);
    }

    public bool Devimon
    {
        get => HasBit(0, 6);
        set => SetBit(0, 6, value);
    }

    public bool Airdramon
    {
        get => HasBit(0, 7);
        set => SetBit(0, 7, value);
    }

    public bool Tyrannomon
    {
        get => HasBit(1, 0);
        set => SetBit(1, 0, value);
    }

    public bool Meramon
    {
        get => HasBit(1, 1);
        set => SetBit(1, 1, value);
    }

    public bool Seadramon
    {
        get => HasBit(1, 2);
        set => SetBit(1, 2, value);
    }

    public bool Numemon
    {
        get => HasBit(1, 3);
        set => SetBit(1, 3, value);
    }

    public bool MetalGreymon
    {
        get => HasBit(1, 4);
        set => SetBit(1, 4, value);
    }

    public bool Mamemon
    {
        get => HasBit(1, 5);
        set => SetBit(1, 5, value);
    }

    public bool Monzaemon
    {
        get => HasBit(1, 6);
        set => SetBit(1, 6, value);
    }

    public bool Gabumon
    {
        get => HasBit(2, 1);
        set => SetBit(2, 1, value);
    }

    public bool Elecmon
    {
        get => HasBit(2, 2);
        set => SetBit(2, 2, value);
    }

    public bool Kabuterimon
    {
        get => HasBit(2, 3);
        set => SetBit(2, 3, value);
    }

    public bool Angemon
    {
        get => HasBit(2, 4);
        set => SetBit(2, 4, value);
    }

    public bool Birdramon
    {
        get => HasBit(2, 5);
        set => SetBit(2, 5, value);
    }

    public bool Garurumon
    {
        get => HasBit(2, 6);
        set => SetBit(2, 6, value);
    }

    public bool Frigimon
    {
        get => HasBit(2, 7);
        set => SetBit(2, 7, value);
    }

    public bool Whamon
    {
        get => HasBit(3, 0);
        set => SetBit(3, 0, value);
    }

    public bool Vegiemon
    {
        get => HasBit(3, 1);
        set => SetBit(3, 1, value);
    }

    public bool SkullGreymon
    {
        get => HasBit(3, 2);
        set => SetBit(3, 2, value);
    }

    public bool MetalMamemon
    {
        get => HasBit(3, 3);
        set => SetBit(3, 3, value);
    }

    public bool Vademon
    {
        get => HasBit(3, 4);
        set => SetBit(3, 4, value);
    }

    public bool Patamon
    {
        get => HasBit(3, 7);
        set => SetBit(3, 7, value);
    }

    public bool Kunemon
    {
        get => HasBit(4, 0);
        set => SetBit(4, 0, value);
    }

    public bool Unimon
    {
        get => HasBit(4, 1);
        set => SetBit(4, 1, value);
    }

    public bool Ogremon
    {
        get => HasBit(4, 2);
        set => SetBit(4, 2, value);
    }

    public bool Shellmon
    {
        get => HasBit(4, 3);
        set => SetBit(4, 3, value);
    }

    public bool Centarumon
    {
        get => HasBit(4, 4);
        set => SetBit(4, 4, value);
    }

    public bool Bakemon
    {
        get => HasBit(4, 5);
        set => SetBit(4, 5, value);
    }

    public bool Drimogemon
    {
        get => HasBit(4, 6);
        set => SetBit(4, 6, value);
    }

    public bool Sukamon
    {
        get => HasBit(4, 7);
        set => SetBit(4, 7, value);
    }

    public bool Andromon
    {
        get => HasBit(5, 0);
        set => SetBit(5, 0, value);
    }

    public bool Giromon
    {
        get => HasBit(5, 1);
        set => SetBit(5, 1, value);
    }

    public bool Etemon
    {
        get => HasBit(5, 2);
        set => SetBit(5, 2, value);
    }

    public bool Biyomon
    {
        get => HasBit(5, 5);
        set => SetBit(5, 5, value);
    }

    public bool Palmon
    {
        get => HasBit(5, 6);
        set => SetBit(5, 6, value);
    }

    public bool Monochromon
    {
        get => HasBit(5, 7);
        set => SetBit(5, 7, value);
    }

    public bool Leomon
    {
        get => HasBit(6, 0);
        set => SetBit(6, 0, value);
    }

    public bool Coelamon
    {
        get => HasBit(6, 1);
        set => SetBit(6, 1, value);
    }

    public bool Kokatorimon
    {
        get => HasBit(6, 2);
        set => SetBit(6, 2, value);
    }

    public bool Kuwagamon
    {
        get => HasBit(6, 3);
        set => SetBit(6, 3, value);
    }

    public bool Mojyamon
    {
        get => HasBit(6, 4);
        set => SetBit(6, 4, value);
    }

    public bool Nanimon
    {
        get => HasBit(6, 5);
        set => SetBit(6, 5, value);
    }

    public bool Megadramon
    {
        get => HasBit(6, 6);
        set => SetBit(6, 6, value);
    }

    public bool Piximon
    {
        get => HasBit(6, 7);
        set => SetBit(6, 7, value);
    }

    public bool Digitamamon
    {
        get => HasBit(7, 0);
        set => SetBit(7, 0, value);
    }

    public bool Penguinmon
    {
        get => HasBit(7, 1);
        set => SetBit(7, 1, value);
    }

    public bool Ninjamon
    {
        get => HasBit(7, 2);
        set => SetBit(7, 2, value);
    }

    protected override void OnUpdateData()
    {
        byte[] data = mem.ReadBytes(ram.A(RECRUITMENT_BASE_OFFSET), _data.Length);
        Array.Copy(data, _data, _data.Length);

        EmulatorLinkEventHub.SignalRecruitmentSynchronized();
    }

    private bool HasBit(int index, int bit)
        => (_data[index] & (1 << bit)) != 0;

    private void SetBit(int index, int bit, bool value)
    {
        _data[index] = (byte)(value ? _data[index] | (1 << bit) : _data[index] & ~(1 << bit));
        mem.WriteByte(ram.A(RECRUITMENT_BASE_OFFSET + index), _data[index]);
    }
}