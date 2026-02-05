using System;
using System.Globalization;
using System.Windows.Data;

namespace DigimonWorld.Frontend.WPF.Conversion;

public class TooltipTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string statName = parameter?.ToString() ?? "";
        return $"Synchronize {statName} from the emulator";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}