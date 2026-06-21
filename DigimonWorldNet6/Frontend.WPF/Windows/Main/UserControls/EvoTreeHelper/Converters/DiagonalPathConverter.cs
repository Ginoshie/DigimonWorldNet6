using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.Converters;

/// <summary>
/// Converts source (X,Y) and target (X,Y) node positions into a path with
/// a horizontal segment, a diagonal segment, and another horizontal segment.
/// Values: sourceX, sourceY, targetX, targetY.
/// Parameter: node size (double) used to calculate edge midpoints.
/// </summary>
public class DiagonalPathConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 4)
        {
            return DependencyProperty.UnsetValue;
        }

        for (int i = 0; i < 4; i++)
        {
            if (values[i] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        double sourceX = System.Convert.ToDouble(values[0]);
        double sourceY = System.Convert.ToDouble(values[1]);
        double targetX = System.Convert.ToDouble(values[2]);
        double targetY = System.Convert.ToDouble(values[3]);

        double nodeSize = parameter is string s ? double.Parse(s, CultureInfo.InvariantCulture) : 48.0;
        double halfNode = nodeSize / 2.0;
        double sourceYOffset = values.Length > 4 && values[4] != DependencyProperty.UnsetValue
            ? System.Convert.ToDouble(values[4])
            : 0.0;

        int stubTier = values.Length > 5 && values[5] != DependencyProperty.UnsetValue
            ? System.Convert.ToInt32(values[5])
            : 1;

        double startY = sourceY + halfNode + sourceYOffset;
        double endY = targetY + halfNode;

        double startX = sourceX + nodeSize;

        double totalHorizontal = Math.Abs(targetX - startX);

        // Tier 1 (innermost): longest stub (52%), Tier 2: medium (26%), Tier 3 (outermost): immediate diagonal
        double sourceStubFraction = stubTier switch
        {
            1 => 0.52,
            2 => 0.26,
            _ => 0.0
        };
        
        // All target stubs at 16%
        double sourceStub = totalHorizontal * sourceStubFraction;
        double targetStub = totalHorizontal * 0.16;

        double diagonalStartX = startX + sourceStub;
        double diagonalEndX = targetX - targetStub;

        // Path: start from center of source button → horizontal stub → diagonal → horizontal stub
        // Starting from center ensures lines are hidden behind the button
        double centerX = sourceX + halfNode;
        PathFigure figure = new() { StartPoint = new Point(centerX, startY) };
        figure.Segments.Add(new LineSegment(new Point(diagonalStartX, startY), true));
        figure.Segments.Add(new LineSegment(new Point(diagonalEndX, endY), true));
        figure.Segments.Add(new LineSegment(new Point(targetX, endY), true));

        PathGeometry geometry = new();
        geometry.Figures.Add(figure);

        return geometry;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}