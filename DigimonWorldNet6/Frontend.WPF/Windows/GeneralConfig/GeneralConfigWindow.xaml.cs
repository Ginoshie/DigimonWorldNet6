using System;
using System.Windows;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig;

public partial class GeneralConfigWindow
{
    public GeneralConfigWindow()
    {
        InitializeComponent();
        ContentRendered += GeneralConfigWindow_Loaded;
    }

    private void GeneralConfigWindow_Loaded(object? sender, EventArgs e)
        // Find the target element in the MainWindow
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