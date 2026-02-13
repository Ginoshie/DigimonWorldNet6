namespace Shared.Configuration;

public class UserConfiguration
{
    public SpeakingSimulatorConfig SpeakingSimulatorConfig { get; init; } = new();

    public MusicPlayerConfig MusicPlayerConfig { get; init; } = new();

    public EvolutionCalculatorConfig EvolutionCalculatorConfig { get; init; } = new();

    public EmulatorLinkConfig EmulatorLinkConfiguration { get; init; } = new();
}