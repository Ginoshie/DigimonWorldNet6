using System.Windows.Media;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.Models;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper;

/// <summary>
/// A resolved connection that holds direct references to source and target nodes,
/// enabling data binding to their X/Y properties for dynamic connector updates.
/// </summary>
public class ResolvedConnection(EvoTreeNode source, EvoTreeNode target, Brush stroke, double sourceYOffset = 0, int stubTier = 1)
{
    public EvoTreeNode Source { get; } = source;
    public EvoTreeNode Target { get; } = target;
    public Brush Stroke { get; } = stroke;
    public Brush DarkStroke { get; } = CreateDarkVariant(stroke);
    public double SourceYOffset { get; } = sourceYOffset;
    public int StubTier { get; } = stubTier;

    private static Brush CreateDarkVariant(Brush brush)
    {
        if (brush is SolidColorBrush scb)
        {
            Color c = scb.Color;
            return new SolidColorBrush(Color.FromRgb(
                (byte)(c.R * 0.45),
                (byte)(c.G * 0.45),
                (byte)(c.B * 0.45)));
        }
        return brush;
    }
}