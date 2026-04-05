using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.TamerVision;

public static class TamerVisionSettings
{
    public static bool ShowEvo { get; set; }
    public static EvoResultMask EvoResultMask { get; set; } = EvoResultMask.None;
}