using Shared.Enums;

namespace Shared.Configuration;

public class MusicPlayerConfig
{
    public int Volume { get; set; } = 50;

    public MuteMode MuteMode { get; set; } = MuteMode.Unmuted;

    public RepeatMode RepeatMode { get; set; } = RepeatMode.Single;

    public ShuffleMode ShuffleMode { get; set; } = ShuffleMode.Chronological;
    
    public MusicPlayerOnCloseAction OnCloseAction { get; set; }
}