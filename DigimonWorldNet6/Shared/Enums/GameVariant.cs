namespace Shared.Enums;

[Flags]
public enum GameVariant
{
    Original = 1 << 0,
    Vice = 1 << 1,

    MyotismonPatch = 1 << 2,
    PanjyamonPatch = 1 << 3
}