using System.Globalization;
using System.Windows.Controls;

namespace DigimonWorld.Frontend.WPF.Validation;

public sealed class NumericRangeValidationRule : ValidationRule
{
    public long Minimum { get; set; }

    public long Maximum { get; set; }

    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        string? stringValue = value?.ToString();

        if (string.IsNullOrWhiteSpace(stringValue))
        {
            return ValidationResult.ValidResult;
        }

        if (!long.TryParse(stringValue, NumberStyles.Integer, cultureInfo, out long longValue) || longValue < Minimum || longValue > Maximum)
        {
            return new ValidationResult(false, $"Enter a whole number between {Minimum} and {Maximum}.");
        }

        return ValidationResult.ValidResult;
    }
}
