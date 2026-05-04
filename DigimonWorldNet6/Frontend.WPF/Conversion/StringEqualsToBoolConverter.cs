using System;
using System.Globalization;
using System.Windows.Data;

namespace DigimonWorld.Frontend.WPF.Conversion;

public class StringEqualsToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is string str && parameter is string param && str == param;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
