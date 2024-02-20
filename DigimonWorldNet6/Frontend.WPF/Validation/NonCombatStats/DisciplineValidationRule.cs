using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace DigimonWorld.Frontend.WPF.Validation.NonCombatStats;

public sealed class DisciplineValidationRule : ValidationRule
{
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        if (value is not BindingExpression expression) 
            return ValidationResult.ValidResult;
            
        var sourceItem = expression.DataItem;

        if (sourceItem == null)
            return ValidationResult.ValidResult;
            
        var propertyName = expression.ParentBinding?.Path?.Path ?? throw new ArgumentNullException(nameof(expression.ParentBinding.Path.Path));
        var sourceValue = sourceItem.GetType().GetProperty(propertyName)?.GetValue(sourceItem, null);

        if (sourceValue == null)
            return ValidationResult.ValidResult;

        var stringValue = sourceValue.ToString();

        if (!int.TryParse(stringValue, out var intValue) || sourceValue.ToString()!.Length == 0 || intValue < -100 || intValue > 100)
            return new ValidationResult(false, "This textbox has to be empty or contain a number between -100 and 100.");

        return ValidationResult.ValidResult;
    }
}