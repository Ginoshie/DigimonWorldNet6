using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DigimonWorld.Frontend.WPF.Conversion;

public sealed class EnumToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(value);

        var stringValue = value.ToString();

        return new BitmapImage(new Uri($"/Images/{stringValue}.jpg", UriKind.Relative));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}