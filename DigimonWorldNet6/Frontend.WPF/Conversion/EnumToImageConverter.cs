using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DigimonWorld.Frontend.WPF.Conversion;

public sealed class EnumToImageConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(value);

        string? stringValue = value.ToString();

        ArgumentException.ThrowIfNullOrWhiteSpace(stringValue);

        if (targetType != typeof(ImageSource))
            throw new ArgumentException($"Can only convert to {typeof(ImageSource)} but tried to convert to {targetType}", nameof(targetType));

        return new BitmapImage(new Uri($"/Images/Digimon/{stringValue}.jpg", UriKind.Relative));
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}