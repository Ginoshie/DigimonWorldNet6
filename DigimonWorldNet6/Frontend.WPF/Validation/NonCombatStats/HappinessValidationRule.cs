using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace DigimonWorld.Frontend.WPF.Validation.NonCombatStats;

public sealed class HappinessValidationRule : ValidationRule
{
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        if (value is not BindingExpression expression)
        {
            return ValidationResult.ValidResult;
        }

        object? sourceItem = expression.DataItem;

        if (sourceItem == null)
        {
            return ValidationResult.ValidResult;
        }

        string propertyName = expression.ParentBinding?.Path?.Path ?? throw new ArgumentNullException(nameof(expression.ParentBinding.Path.Path));
        object? sourceValue = sourceItem.GetType().GetProperty(propertyName)?.GetValue(sourceItem, null);

        if (sourceValue == null)
        {
            return ValidationResult.ValidResult;
        }

        string? stringValue = sourceValue.ToString();

        if(stringValue == string.Empty)
        {
            return ValidationResult.ValidResult;
        }

        if (!int.TryParse(stringValue, out int intValue) || sourceValue.ToString()!.Length == 0 || intValue < 0 || intValue > 100)
        {
            return new ValidationResult(false, "This textbox has to be empty or contain a positive number up to 100.");
        }

        return ValidationResult.ValidResult;
    }
}