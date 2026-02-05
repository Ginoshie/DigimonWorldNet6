using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DigimonWorld.Frontend.WPF.Conversion;

public class TrueToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value is true ? Visibility.Visible : Visibility.Hidden;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => value is Visibility.Visible;
}