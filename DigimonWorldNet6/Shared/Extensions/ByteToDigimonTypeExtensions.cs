using Type = Shared.Enums.Type;

namespace Shared.Extensions;

public static class ByteToDigimonTypeExtensions
{
    public static Type ToDigimonType(this byte typeAsByteValue) => Enum.Parse<Type>(typeAsByteValue.ToString());
}