using Shared.Enums;

namespace Shared.Extensions;

public static class ByteToSpecialExtensions
{
    public static Special ToSpecial(this byte specialAsByteValue) => Enum.Parse<Special>(specialAsByteValue.ToString());

}