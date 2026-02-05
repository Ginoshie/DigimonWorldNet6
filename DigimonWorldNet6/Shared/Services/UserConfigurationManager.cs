using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using Microsoft.VisualBasic.FileIO;
using Shared.Configuration;
using Shared.Enums;

namespace Shared.Services;

public static class UserConfigurationManager
{
    private static readonly string _userConfigFullPath = FileSystem.CombinePath(AppDomain.CurrentDomain.BaseDirectory, "userconfig.json");
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    private static readonly BehaviorSubject<SpeakingSimulatorConfig> _currentSpeakingSimulatorConfigSubject;
    private static readonly BehaviorSubject<MusicPlayerConfig> _currentMusicPlayerConfigSubject;
    private static readonly BehaviorSubject<EvolutionCalculatorConfig> _currentEvolutionCalculatorConfigSubject;

    static UserConfigurationManager()
    {
        UserConfiguration = GetConfiguration();

        _currentSpeakingSimulatorConfigSubject = new BehaviorSubject<SpeakingSimulatorConfig>(UserConfiguration.SpeakingSimulatorConfig);
        CurrentSpeakingSimulatorConfig = _currentSpeakingSimulatorConfigSubject.AsObservable();

        _currentMusicPlayerConfigSubject = new BehaviorSubject<MusicPlayerConfig>(UserConfiguration.MusicPlayerConfig);
        CurrentMusicPlayerConfig = _currentMusicPlayerConfigSubject.AsObservable();

        _currentEvolutionCalculatorConfigSubject = new BehaviorSubject<EvolutionCalculatorConfig>(UserConfiguration.EvolutionCalculatorConfig);
        CurrentEvolutionCalculatorConfig = _currentEvolutionCalculatorConfigSubject.AsObservable();
    }

    public static IObservable<SpeakingSimulatorConfig> CurrentSpeakingSimulatorConfig { get; }
    public static IObservable<MusicPlayerConfig> CurrentMusicPlayerConfig { get; }
    public static IObservable<EvolutionCalculatorConfig> CurrentEvolutionCalculatorConfig { get; }

    public static UserConfiguration UserConfiguration { get; }

    public static MusicPlayerConfig MusicPlayerConfig => UserConfiguration.MusicPlayerConfig;

    public static SpeakingSimulatorConfig SpeakingSimulatorConfig => UserConfiguration.SpeakingSimulatorConfig;

    public static EvolutionCalculatorConfig EvolutionCalculatorConfig => UserConfiguration.EvolutionCalculatorConfig;

    public static void SetNarratorMode(NarratorMode mode)
    {
        UserConfiguration.SpeakingSimulatorConfig.NarratorMode = mode;
        
        _currentSpeakingSimulatorConfigSubject.OnNext(SpeakingSimulatorConfig);
    }

    public static void SetVolume(int volume) => UserConfiguration.MusicPlayerConfig.Volume = volume;

    public static void SetMuteIsOn(MuteMode muteMode) => UserConfiguration.MusicPlayerConfig.MuteMode = muteMode;

    public static void SetShuffleModeIsOn(ShuffleMode shuffleMode) => UserConfiguration.MusicPlayerConfig.ShuffleMode = shuffleMode;

    public static void SetRepeatModeIsSingle(RepeatMode repeatMode) => UserConfiguration.MusicPlayerConfig.RepeatMode = repeatMode;
    public static void SetOnCloseAction(MusicPlayerOnCloseAction onCloseAction) => UserConfiguration.MusicPlayerConfig.OnCloseAction = onCloseAction;
    public static void SetEvolutionCalculatorMode(GameVariant mode)
    {
        UserConfiguration.EvolutionCalculatorConfig.GameVariant = mode;
        
        _currentEvolutionCalculatorConfigSubject.OnNext(EvolutionCalculatorConfig);
    }

    public static void SaveConfiguration()
    {
        try
        {
            string json = JsonSerializer.Serialize(UserConfiguration, _jsonSerializerOptions);
            File.WriteAllText(_userConfigFullPath, json);

            _currentSpeakingSimulatorConfigSubject.OnNext(SpeakingSimulatorConfig);
            _currentMusicPlayerConfigSubject.OnNext(MusicPlayerConfig);
            _currentEvolutionCalculatorConfigSubject.OnNext(EvolutionCalculatorConfig);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save configuration: {ex.Message}");
        }
    }

    private static UserConfiguration GetConfiguration()
    {
        if (!File.Exists(_userConfigFullPath))
        {
            return new UserConfiguration();
        }

        try
        {
            string json = File.ReadAllText(_userConfigFullPath);
            return JsonSerializer.Deserialize<UserConfiguration>(json) ?? new UserConfiguration();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading configuration: {ex.Message}");

            return new UserConfiguration();
        }
    }
}