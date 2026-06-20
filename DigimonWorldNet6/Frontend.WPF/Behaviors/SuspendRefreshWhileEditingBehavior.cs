using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Microsoft.Xaml.Behaviors;

namespace DigimonWorld.Frontend.WPF.Behaviors;

public sealed class SuspendRefreshWhileEditingBehavior : Behavior<FrameworkElement>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.AddHandler(UIElement.GotFocusEvent, new RoutedEventHandler(OnGotFocus), true);
        AssociatedObject.AddHandler(UIElement.LostFocusEvent, new RoutedEventHandler(OnLostFocus), true);
    }

    protected override void OnDetaching()
    {
        AssociatedObject.RemoveHandler(UIElement.GotFocusEvent, new RoutedEventHandler(OnGotFocus));
        AssociatedObject.RemoveHandler(UIElement.LostFocusEvent, new RoutedEventHandler(OnLostFocus));
        base.OnDetaching();
    }

    private void OnGotFocus(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is TextBox textBox && ResolveEditable(textBox) is { } editable)
        {
            editable.IsEditing = true;
        }
    }

    private void OnLostFocus(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is not TextBox textBox || ResolveEditable(textBox) is not { } editable)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            editable.IsEditing = false;

            if (textBox.GetBindingExpression(TextBox.TextProperty) is { } expression)
            {
                System.Windows.Controls.Validation.ClearInvalid(expression);
                expression.UpdateTarget();
            }
        }
        else if (!System.Windows.Controls.Validation.GetHasError(textBox))
        {
            editable.IsEditing = false;
        }
    }

    private static IEditableValue? ResolveEditable(TextBox textBox)
    {
        if (textBox.GetBindingExpression(TextBox.TextProperty) is not { } expression)
        {
            return null;
        }

        if (expression.ResolvedSource is IEditableValue editable)
        {
            return editable;
        }

        if (expression.ResolvedSource is not { } source || expression.ParentBinding.Path?.Path is not { } path)
        {
            return null;
        }

        string[] segments = path.Split('.');
        object? current = source;

        for (int i = 0; i < segments.Length - 1; i++)
        {
            current = current?.GetType().GetProperty(segments[i])?.GetValue(current);
        }

        return current as IEditableValue;
    }
}
