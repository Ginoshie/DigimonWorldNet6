using System;
using System.Windows;

namespace DigimonWorld.Frontend.WPF.Windows.BaseClasses;

public abstract class BaseWindow : Window
{
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        PositionWindow();
    }

    private void PositionWindow()
    {
        Left = Application.Current.MainWindow!.Left - ActualWidth - 10;
        Top = Application.Current.MainWindow.Top;
    }
}