using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DigimonWorld.Frontend.WPF.Conversion;

public class IsNullOrWhitespaceToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => string.IsNullOrWhiteSpace(value?.ToString()) ? Visibility.Hidden : Visibility.Visible;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}