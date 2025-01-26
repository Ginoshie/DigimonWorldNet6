using System;
using System.Windows;

namespace DigimonWorld.Frontend.WPF.Windows;

public class BaseDialogWindow : Window
{
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        PositionWindow();
    }

    private void PositionWindow()
    {
        UIElement? targetElement = Application.Current.MainWindow?.FindName("OuterBorderWindow") as UIElement;

        if (targetElement is not FrameworkElement frameworkElement) return; 
        
        Point position = frameworkElement.TransformToAncestor(Application.Current.MainWindow!)
            .Transform(new Point(0, 0));

        double centerX = position.X + frameworkElement.ActualWidth / 2;
        double centerY = position.Y + frameworkElement.ActualHeight / 2;

        Left = Application.Current.MainWindow!.Left + centerX - ActualWidth / 2;
        Top = Application.Current.MainWindow.Top + centerY - ActualHeight / 2;
    }
    
}