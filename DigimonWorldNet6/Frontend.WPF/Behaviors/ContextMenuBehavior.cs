using System.Windows;
using System.Windows.Controls;

namespace DigimonWorld.Frontend.WPF.Behaviors;

public static class ContextMenuBehavior
{
    public static readonly DependencyProperty IsDisabledProperty = DependencyProperty.RegisterAttached(
        "IsDisabled", typeof(bool), typeof(ContextMenuBehavior), new PropertyMetadata(false, OnIsDisabledChanged));

    public static bool GetIsDisabled(DependencyObject element)
        => (bool)element.GetValue(IsDisabledProperty);

    public static void SetIsDisabled(DependencyObject element, bool value)
        => element.SetValue(IsDisabledProperty, value);

    private static void OnIsDisabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement element)
        {
            return;
        }

        if ((bool)e.NewValue)
        {
            element.ContextMenu = new ContextMenu();
            element.AddHandler(FrameworkElement.ContextMenuOpeningEvent, new ContextMenuEventHandler(OnContextMenuOpening), true);
        }
        else
        {
            element.RemoveHandler(FrameworkElement.ContextMenuOpeningEvent, new ContextMenuEventHandler(OnContextMenuOpening));
            element.ClearValue(FrameworkElement.ContextMenuProperty);
        }
    }

    private static void OnContextMenuOpening(object sender, ContextMenuEventArgs e)
    {
        e.Handled = true;
    }
}
