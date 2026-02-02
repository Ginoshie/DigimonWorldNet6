using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using DigimonWorld.Frontend.WPF.Models;
using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Conversion;

public class DigimonTypeContainsMultiConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is IList<DigimonName> historicEvolutions && values[1] is DigimonIcon digimonIcon)
        {
            return historicEvolutions.Contains(digimonIcon.DigimonName);
        }
        return false;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}