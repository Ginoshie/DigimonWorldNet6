using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Microsoft.Xaml.Behaviors;

namespace DigimonWorld.Frontend.WPF.Behaviors;

public sealed class CommitOnBackgroundClickBehavior : Behavior<FrameworkElement>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Focusable = true;
        KeyboardNavigation.SetIsTabStop(AssociatedObject, false);
        AssociatedObject.AddHandler(UIElement.PreviewMouseDownEvent, new MouseButtonEventHandler(OnPreviewMouseDown), true);
    }

    protected override void OnDetaching()
    {
        AssociatedObject.RemoveHandler(UIElement.PreviewMouseDownEvent, new MouseButtonEventHandler(OnPreviewMouseDown));
        base.OnDetaching();
    }

    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (Keyboard.FocusedElement is not TextBox || ClickedInsideTextBox(e.OriginalSource))
        {
            return;
        }

        AssociatedObject.Focus();
    }

    private static bool ClickedInsideTextBox(object? source)
    {
        DependencyObject? current = source as DependencyObject;
        while (current is not null)
        {
            if (current is TextBox)
            {
                return true;
            }

            current = current is Visual or Visual3D
                ? VisualTreeHelper.GetParent(current)
                : LogicalTreeHelper.GetParent(current);
        }

        return false;
    }
}
