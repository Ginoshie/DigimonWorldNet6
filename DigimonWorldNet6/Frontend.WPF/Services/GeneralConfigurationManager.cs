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

    public static JukeboxConfig JukeboxConfig => GeneralConfig.JukeboxConfig;

    public static void SetNarratorMode(NarratorMode mode)
    {
        GeneralConfig.SpeakingSimulatorConfig.NarratorMode = mode;
    }

    public static void SetVolume(int volume)
    {
        // if (volume is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(volume), volume, "Volume must be between 0 and 100.");

        GeneralConfig.JukeboxConfig.Volume = volume;
    }

    public static void SetMuteIsOn(MuteMode muteMode)
    {
        GeneralConfig.JukeboxConfig.MuteMode = muteMode;
    }

    public static void SetRepeatModeIsSingle(RepeatMode repeatMode)
    {
        GeneralConfig.JukeboxConfig.RepeatMode = repeatMode;
    }

    public static void SetShuffleModeIsOn(ShuffleMode shuffleMode)
    {
        GeneralConfig.JukeboxConfig.ShuffleMode = shuffleMode;
    }

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