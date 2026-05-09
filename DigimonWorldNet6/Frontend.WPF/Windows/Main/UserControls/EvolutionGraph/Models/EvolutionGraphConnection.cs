using System.Windows.Media;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionGraph.Models;

public class EvolutionGraphConnection
{
    public EvolutionGraphConnection(string fromId, string toId, Brush stroke)
    {
        FromId = fromId;
        ToId = toId;
        Stroke = stroke;
    }

    public string FromId { get; }
    public string ToId { get; }
    public Brush Stroke { get; }
}
