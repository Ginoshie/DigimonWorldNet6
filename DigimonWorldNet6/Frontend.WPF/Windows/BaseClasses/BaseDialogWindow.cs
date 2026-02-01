using System;
using System.Windows;

namespace DigimonWorld.Frontend.WPF.Windows.BaseClasses;

public abstract class BaseDialogWindow : Window
{
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        PositionWindow();
    }

    private void PositionWindow()
    {
        UIElement? mainWindowUiElement = Application.Current.MainWindow?.FindName("OuterBorderWindow") as UIElement;

        if (mainWindowUiElement is not FrameworkElement mainWindowFrameworkElement)
        {
            return;
        }

        Point position = mainWindowFrameworkElement
            .TransformToAncestor(Application.Current.MainWindow!)
            .Transform(new Point(0, 0));

        double mainWindowCenterX = position.X + mainWindowFrameworkElement.ActualWidth / 2;
        double mainWindowCenterY = position.Y + mainWindowFrameworkElement.ActualHeight / 2;

        Left = Application.Current.MainWindow!.Left + mainWindowCenterX - ActualWidth / 2;
        Top = Application.Current.MainWindow.Top + mainWindowCenterY - ActualHeight / 2;
    }
}