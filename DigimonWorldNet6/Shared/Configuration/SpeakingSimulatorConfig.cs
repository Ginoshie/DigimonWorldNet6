using Shared.Enums;

namespace Shared.Configuration;

public class SpeakingSimulatorConfig
{
    public NarratorMode NarratorMode { get; set; } = NarratorMode.Speech;
}