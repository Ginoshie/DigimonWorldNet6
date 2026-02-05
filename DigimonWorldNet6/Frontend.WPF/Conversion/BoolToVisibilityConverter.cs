using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DigimonWorld.Frontend.WPF.Conversion;

public class BoolToVisibilityConverter : IValueConverter
{
    public bool CollapseWhenFalse { get; set; } = true;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is true)
            return Visibility.Visible;

        return CollapseWhenFalse
            ? Visibility.Collapsed
            : Visibility.Hidden;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is Visibility.Visible;
    }
}