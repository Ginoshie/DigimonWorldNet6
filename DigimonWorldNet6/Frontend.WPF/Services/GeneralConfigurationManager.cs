using System;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using DigimonWorld.Frontend.WPF.Configuration;
using DigimonWorld.Frontend.WPF.Constants;

namespace DigimonWorld.Frontend.WPF.Services;

public static class GeneralConfigurationManager
{
    private static readonly string GeneralConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeneralConfiguration.json");
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { WriteIndented = true };

    private static readonly BehaviorSubject<SpeakingSimulatorConfig> CurrentSpeakingSimulatorConfigSubject;

    static GeneralConfigurationManager()
    {
        LoadConfiguration();

        CurrentSpeakingSimulatorConfigSubject = new BehaviorSubject<SpeakingSimulatorConfig>(GeneralConfig!.SpeakingSimulatorConfig);
        CurrentSpeakingSimulatorConfig = CurrentSpeakingSimulatorConfigSubject.AsObservable();
    }

    public static IObservable<SpeakingSimulatorConfig> CurrentSpeakingSimulatorConfig { get; }

    public static GeneralConfig GeneralConfig { get; private set; }

    public static MusicPlayerConfig MusicPlayerConfig => GeneralConfig.MusicPlayerConfig;

    public static SpeakingSimulatorConfig SpeakingSimulatorConfig => GeneralConfig.SpeakingSimulatorConfig;

    public static void SetNarratorMode(NarratorMode mode) => GeneralConfig.SpeakingSimulatorConfig.NarratorMode = mode;

    public static void SetVolume(int volume) => GeneralConfig.MusicPlayerConfig.Volume = volume;

    public static void SetMuteIsOn(MuteMode muteMode) => GeneralConfig.MusicPlayerConfig.MuteMode = muteMode;

    public static void SetShuffleModeIsOn(ShuffleMode shuffleMode) => GeneralConfig.MusicPlayerConfig.ShuffleMode = shuffleMode;

    public static void SetRepeatModeIsSingle(RepeatMode repeatMode) => GeneralConfig.MusicPlayerConfig.RepeatMode = repeatMode;
    public static void SetOnCloseAction(MusicPlayerOnCloseAction onCloseAction) => GeneralConfig.MusicPlayerConfig.OnCloseAction = onCloseAction;

    public static void SaveConfiguration()
    {
        try
        {
            string json = JsonSerializer.Serialize(GeneralConfig, JsonSerializerOptions);
            File.WriteAllText(GeneralConfigFilePath, json);

            CurrentSpeakingSimulatorConfigSubject.OnNext(GeneralConfig.SpeakingSimulatorConfig);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save configuration: {ex.Message}");
        }
    }

    public static void ResetConfiguration()
    {
        LoadConfiguration();

        CurrentSpeakingSimulatorConfigSubject.OnNext(GeneralConfig.SpeakingSimulatorConfig);
    }

    private static void LoadConfiguration()
    {
        if (!File.Exists(GeneralConfigFilePath))
        {
            GeneralConfig = new GeneralConfig();

            return;
        }

        try
        {
            string json = File.ReadAllText(GeneralConfigFilePath);
            GeneralConfig = JsonSerializer.Deserialize<GeneralConfig>(json) ?? new GeneralConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading configuration: {ex.Message}");

            GeneralConfig = new GeneralConfig();
        }
    }
}