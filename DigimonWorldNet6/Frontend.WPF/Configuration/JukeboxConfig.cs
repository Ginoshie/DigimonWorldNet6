using DigimonWorld.Frontend.WPF.Constants;

namespace DigimonWorld.Frontend.WPF.Configuration;

public class JukeboxConfig
{
    public int Volume { get; set; } = 50;

    public MuteMode MuteMode { get; set; } = MuteMode.Unmuted;

    public RepeatMode RepeatMode { get; set; } = RepeatMode.Single;

    public ShuffleMode ShuffleMode { get; set; } = ShuffleMode.Chronological;
}