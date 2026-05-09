using System.Windows;
using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionGraph.Models;

public class EvolutionGraphNode : DependencyObject
{
    public static readonly DependencyProperty XProperty =
        DependencyProperty.Register(nameof(X), typeof(double), typeof(EvolutionGraphNode), new PropertyMetadata(0.0));

    public static readonly DependencyProperty YProperty =
        DependencyProperty.Register(nameof(Y), typeof(double), typeof(EvolutionGraphNode), new PropertyMetadata(0.0));

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(EvolutionGraphNode), new PropertyMetadata(false));

    public EvolutionGraphNode(string id, double x, double y, string iconPath, DigimonName digimonName, bool isCenter, EvolutionCriteriaDisplay? criteria = null)
    {
        Id = id;
        X = x;
        Y = y;
        IconPath = iconPath;
        DigimonName = digimonName;
        IsCenter = isCenter;
        Criteria = criteria;
    }

    public string Id { get; }

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
    public bool IsCenter { get; }
    public EvolutionCriteriaDisplay? Criteria { get; }
}