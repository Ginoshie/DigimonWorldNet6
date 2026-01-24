using System;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using Generics.Configuration;
using Generics.Enums;
using Microsoft.VisualBasic.FileIO;

namespace DigimonWorld.Frontend.WPF.Services;

public static class UserConfigurationManager
{
    private static readonly string UserConfigFullPath = FileSystem.CombinePath(AppDomain.CurrentDomain.BaseDirectory, "userconfig.json");
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { WriteIndented = true };

    private static readonly BehaviorSubject<SpeakingSimulatorConfig> CurrentSpeakingSimulatorConfigSubject;
    private static readonly BehaviorSubject<MusicPlayerConfig> CurrentMusicPlayerConfigSubject;
    private static readonly BehaviorSubject<EvolutionCalculatorConfig> CurrentEvolutionCalculatorConfigSubject;

    static UserConfigurationManager()
    {
        UserConfiguration = GetConfiguration();

        CurrentSpeakingSimulatorConfigSubject = new BehaviorSubject<SpeakingSimulatorConfig>(UserConfiguration.SpeakingSimulatorConfig);
        CurrentSpeakingSimulatorConfig = CurrentSpeakingSimulatorConfigSubject.AsObservable();

        CurrentMusicPlayerConfigSubject = new BehaviorSubject<MusicPlayerConfig>(UserConfiguration.MusicPlayerConfig);
        CurrentMusicPlayerConfig = CurrentMusicPlayerConfigSubject.AsObservable();

        CurrentEvolutionCalculatorConfigSubject = new BehaviorSubject<EvolutionCalculatorConfig>(UserConfiguration.EvolutionCalculatorConfig);
        CurrentEvolutionCalculatorConfig = CurrentEvolutionCalculatorConfigSubject.AsObservable();
    }

    public static IObservable<SpeakingSimulatorConfig> CurrentSpeakingSimulatorConfig { get; }
    public static IObservable<MusicPlayerConfig> CurrentMusicPlayerConfig { get; }
    public static IObservable<EvolutionCalculatorConfig> CurrentEvolutionCalculatorConfig { get; }

    public static UserConfiguration UserConfiguration { get; }

    public static MusicPlayerConfig MusicPlayerConfig => UserConfiguration.MusicPlayerConfig;

    public static SpeakingSimulatorConfig SpeakingSimulatorConfig => UserConfiguration.SpeakingSimulatorConfig;

    public static EvolutionCalculatorConfig EvolutionCalculatorConfig => UserConfiguration.EvolutionCalculatorConfig;

    public static void SetNarratorMode(NarratorMode mode) => UserConfiguration.SpeakingSimulatorConfig.NarratorMode = mode;

    public static void SetVolume(int volume) => UserConfiguration.MusicPlayerConfig.Volume = volume;

    public static void SetMuteIsOn(MuteMode muteMode) => UserConfiguration.MusicPlayerConfig.MuteMode = muteMode;

    public static void SetShuffleModeIsOn(ShuffleMode shuffleMode) => UserConfiguration.MusicPlayerConfig.ShuffleMode = shuffleMode;

    public static void SetRepeatModeIsSingle(RepeatMode repeatMode) => UserConfiguration.MusicPlayerConfig.RepeatMode = repeatMode;
    public static void SetOnCloseAction(MusicPlayerOnCloseAction onCloseAction) => UserConfiguration.MusicPlayerConfig.OnCloseAction = onCloseAction;
    public static void SetEvolutionCalculatorMode(EvolutionCalculatorMode mode) => UserConfiguration.EvolutionCalculatorConfig.EvolutionCalculatorMode = mode;

    public static void SaveConfiguration()
    {
        try
        {
            string json = JsonSerializer.Serialize(UserConfiguration, JsonSerializerOptions);
            File.WriteAllText(UserConfigFullPath, json);

            CurrentSpeakingSimulatorConfigSubject.OnNext(SpeakingSimulatorConfig);
            CurrentMusicPlayerConfigSubject.OnNext(MusicPlayerConfig);
            CurrentEvolutionCalculatorConfigSubject.OnNext(EvolutionCalculatorConfig);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save configuration: {ex.Message}");
        }
    }

    private static UserConfiguration GetConfiguration()
    {
        if (!File.Exists(UserConfigFullPath))
        {
            return new UserConfiguration();
        }

        try
        {
            string json = File.ReadAllText(UserConfigFullPath);
            return JsonSerializer.Deserialize<UserConfiguration>(json) ?? new UserConfiguration();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading configuration: {ex.Message}");

            return new UserConfiguration();
        }
    }
}