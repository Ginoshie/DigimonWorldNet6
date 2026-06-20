using System;
using System.Globalization;
using System.Windows.Data;

namespace DigimonWorld.Frontend.WPF.Conversion;

public sealed class DigimonNameToIconPathConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(value);

        string? name = value.ToString();

        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return $"/Images/Icons/Digimon/{name.ToLowerInvariant()}-icon.png";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
