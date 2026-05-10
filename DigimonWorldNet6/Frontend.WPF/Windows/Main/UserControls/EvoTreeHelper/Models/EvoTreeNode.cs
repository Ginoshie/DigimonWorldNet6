using System.Windows;
using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.Models;

public class EvoTreeNode : DependencyObject
{
    public static readonly DependencyProperty XProperty =
        DependencyProperty.Register(nameof(X), typeof(double), typeof(EvoTreeNode), new PropertyMetadata(0.0));

    public static readonly DependencyProperty YProperty =
        DependencyProperty.Register(nameof(Y), typeof(double), typeof(EvoTreeNode), new PropertyMetadata(0.0));

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(EvoTreeNode), new PropertyMetadata(false));

    public EvoTreeNode(double x, double y, string iconPath, DigimonName digimonName)
    {
        X = x;
        Y = y;
        IconPath = iconPath;
        DigimonName = digimonName;
    }

    public double X
    {
        get => (double)GetValue(XProperty);
        set => SetValue(XProperty, value);
    }

    public double Y
    {
        get => (double)GetValue(YProperty);
        set => SetValue(YProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public string IconPath { get; }
    public DigimonName DigimonName { get; }
}
