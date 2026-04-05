using Shared.Enums;

namespace Shared.Configuration;

public class TamerVisionConfig
{
    public bool ShowEvo { get; set; }

    public EvoResultMask EvoResultMask { get; set; } = EvoResultMask.None;
}
