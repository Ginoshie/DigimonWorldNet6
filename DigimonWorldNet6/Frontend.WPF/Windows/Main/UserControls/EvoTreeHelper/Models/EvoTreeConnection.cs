using System.Windows.Media;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.Models;

public class EvoTreeConnection
{
    public EvoTreeConnection(string fromId, string toId, Brush stroke)
    {
        FromId = fromId;
        ToId = toId;
        Stroke = stroke;
    }

    public string FromId { get; }
    public string ToId { get; }
    public Brush Stroke { get; }
}
