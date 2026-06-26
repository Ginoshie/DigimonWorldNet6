using System.Windows;

namespace DigimonWorld.Frontend.WPF.Behaviors;

public static class LockState
{
    public static readonly DependencyProperty IsLockedProperty = DependencyProperty.RegisterAttached(
        "IsLocked", typeof(bool), typeof(LockState), new PropertyMetadata(false));

    public static bool GetIsLocked(DependencyObject element)
        => (bool)element.GetValue(IsLockedProperty);

    public static void SetIsLocked(DependencyObject element, bool value)
        => element.SetValue(IsLockedProperty, value);
}
