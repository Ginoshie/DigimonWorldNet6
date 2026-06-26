using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Microsoft.Xaml.Behaviors;

namespace DigimonWorld.Frontend.WPF.Behaviors;

public sealed class LockOnRightClickBehavior : Behavior<FrameworkElement>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.AddHandler(UIElement.MouseRightButtonUpEvent, new MouseButtonEventHandler(OnRightButtonUp), true);
    }

    protected override void OnDetaching()
    {
        AssociatedObject.RemoveHandler(UIElement.MouseRightButtonUpEvent, new MouseButtonEventHandler(OnRightButtonUp));
        base.OnDetaching();
    }

    private void OnRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (FindInputControl(e.OriginalSource) is not { } control || ResolveLockable(control) is not { } lockable)
        {
            return;
        }

        lockable.IsLocked = !lockable.IsLocked;
        LockState.SetIsLocked(control, lockable.IsLocked);
    }

    private static FrameworkElement? FindInputControl(object? source)
    {
        DependencyObject? current = source as DependencyObject;
        while (current is not null)
        {
            if (current is TextBox or ToggleButton or ComboBox)
            {
                return (FrameworkElement)current;
            }

            current = current is Visual or Visual3D
                ? VisualTreeHelper.GetParent(current)
                : LogicalTreeHelper.GetParent(current);
        }

        return null;
    }

    private static ILockableValue? ResolveLockable(FrameworkElement control)
    {
        DependencyProperty? property = control switch
        {
            TextBox => TextBox.TextProperty,
            ToggleButton => ToggleButton.IsCheckedProperty,
            ComboBox => Selector.SelectedValueProperty,
            _ => null
        };

        if (property is null || control.GetBindingExpression(property) is not { } expression)
        {
            return null;
        }

        if (expression.ResolvedSource is ILockableValue lockable)
        {
            return lockable;
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

        return current as ILockableValue;
    }
}
