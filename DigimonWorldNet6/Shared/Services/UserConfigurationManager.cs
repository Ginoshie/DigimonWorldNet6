using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using Microsoft.VisualBasic.FileIO;
using Shared.Configuration;
using Shared.Enums;

namespace Shared.Services;

public static class UserConfigurationManager
{
    private static readonly UserConfiguration _userConfiguration;
    private static readonly string _userConfigFullPath = FileSystem.CombinePath(AppDomain.CurrentDomain.BaseDirectory, "userconfig.json");
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

    private static readonly BehaviorSubject<SpeakingSimulatorConfig> _currentSpeakingSimulatorConfigSubject;
    private static readonly BehaviorSubject<MusicPlayerConfig> _currentMusicPlayerConfigSubject;
    private static readonly BehaviorSubject<EvolutionCalculatorConfig> _currentEvolutionCalculatorConfigSubject;
    private static readonly BehaviorSubject<EmulatorLinkConfig> _currentEmulatorLinkConfigSubject;

    static UserConfigurationManager()
    {
        _userConfiguration = GetConfiguration();

        MusicPlayerConfig = _userConfiguration.MusicPlayerConfig;
        SpeakingSimulatorConfig = _userConfiguration.SpeakingSimulatorConfig;
        EvolutionCalculatorConfig = _userConfiguration.EvolutionCalculatorConfig;
        EmulatorLinkConfig = _userConfiguration.EmulatorLinkConfiguration;

        _currentSpeakingSimulatorConfigSubject = new BehaviorSubject<SpeakingSimulatorConfig>(SpeakingSimulatorConfig);
        CurrentSpeakingSimulatorConfig = _currentSpeakingSimulatorConfigSubject.AsObservable();

        _currentMusicPlayerConfigSubject = new BehaviorSubject<MusicPlayerConfig>(MusicPlayerConfig);
        CurrentMusicPlayerConfig = _currentMusicPlayerConfigSubject.AsObservable();

        _currentEvolutionCalculatorConfigSubject = new BehaviorSubject<EvolutionCalculatorConfig>(EvolutionCalculatorConfig);
        CurrentEvolutionCalculatorConfig = _currentEvolutionCalculatorConfigSubject.AsObservable();

        _currentEmulatorLinkConfigSubject = new BehaviorSubject<EmulatorLinkConfig>(EmulatorLinkConfig);
        CurrentEmulatorLinkConfig = _currentEmulatorLinkConfigSubject.AsObservable();
    }

    public static IObservable<SpeakingSimulatorConfig> CurrentSpeakingSimulatorConfig { get; }
    public static IObservable<MusicPlayerConfig> CurrentMusicPlayerConfig { get; }
    public static IObservable<EvolutionCalculatorConfig> CurrentEvolutionCalculatorConfig { get; }
    public static IObservable<EmulatorLinkConfig> CurrentEmulatorLinkConfig { get; }

    public static MusicPlayerConfig MusicPlayerConfig { get; }

    public static SpeakingSimulatorConfig SpeakingSimulatorConfig { get; }

    public static EvolutionCalculatorConfig EvolutionCalculatorConfig { get; }

    public static EmulatorLinkConfig EmulatorLinkConfig { get; }

    public static void SetNarratorMode(NarratorMode mode)
    {
        SpeakingSimulatorConfig.NarratorMode = mode;

        _currentSpeakingSimulatorConfigSubject.OnNext(SpeakingSimulatorConfig);
    }

    public static void SetVolume(int volume) => MusicPlayerConfig.Volume = volume;

    public static void SetMuteIsOn(MuteMode muteMode) => MusicPlayerConfig.MuteMode = muteMode;

    public static void SetShuffleModeIsOn(ShuffleMode shuffleMode) => MusicPlayerConfig.ShuffleMode = shuffleMode;

    public static void SetRepeatModeIsSingle(RepeatMode repeatMode) => MusicPlayerConfig.RepeatMode = repeatMode;
    public static void SetOnCloseAction(MusicPlayerOnCloseAction onCloseAction) => MusicPlayerConfig.OnCloseAction = onCloseAction;

    public static void SetEvolutionCalculatorMode(GameVariant mode)
    {
        EvolutionCalculatorConfig.GameVariant = mode;

        _currentEvolutionCalculatorConfigSubject.OnNext(EvolutionCalculatorConfig);
    }

    public static void SetEmulatorProcessName(string processName)
    {
        EmulatorLinkConfig.SelectedProcessName = processName;

        _currentEmulatorLinkConfigSubject.OnNext(EmulatorLinkConfig);
    }

    public static void SaveConfiguration()
    {
        try
        {
            string json = JsonSerializer.Serialize(_userConfiguration, _jsonSerializerOptions);
            File.WriteAllText(_userConfigFullPath, json);

            _currentSpeakingSimulatorConfigSubject.OnNext(SpeakingSimulatorConfig);
            _currentMusicPlayerConfigSubject.OnNext(MusicPlayerConfig);
            _currentEvolutionCalculatorConfigSubject.OnNext(EvolutionCalculatorConfig);
            _currentEmulatorLinkConfigSubject.OnNext(EmulatorLinkConfig);
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