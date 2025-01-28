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

    private static readonly BehaviorSubject<SpeakingSimulatorConfig> CurrentSpeakingSimulatorConfigSubject;
    private static readonly BehaviorSubject<JukeboxConfig> CurrentJukeboxConfigSubject;

    private static GeneralConfig _generalConfig = null!;

    static GeneralConfigurationManager()
    {
        LoadConfiguration();

        CurrentSpeakingSimulatorConfigSubject = new BehaviorSubject<SpeakingSimulatorConfig>(_generalConfig.SpeakingSimulatorConfig);
        CurrentSpeakingSimulatorConfig = CurrentSpeakingSimulatorConfigSubject.AsObservable();

        CurrentJukeboxConfigSubject = new BehaviorSubject<JukeboxConfig>(_generalConfig.JukeboxConfig);
        CurrentJukeboxConfig = CurrentJukeboxConfigSubject.AsObservable();
    }

    public static IObservable<SpeakingSimulatorConfig> CurrentSpeakingSimulatorConfig { get; }
    
    public static IObservable<JukeboxConfig> CurrentJukeboxConfig { get; }

    public static void SetNarratorMode(NarratorMode mode)
    {
        _generalConfig.SpeakingSimulatorConfig.NarratorMode = mode;
    }

    public static void SetMuteIsOn(bool muteIsOn)
    {
        _generalConfig.JukeboxConfig.MuteIsOn = muteIsOn;
    }

    public static void SetRepeatModeIsSingle(bool repeatModeIsSingle)
    {
        _generalConfig.JukeboxConfig.RepeatModeIsSingle = repeatModeIsSingle;
    }

    public static void SetShuffleModeIsOn(bool shuffleModeIsOn)
    {
        _generalConfig.JukeboxConfig.ShuffleModeIsOn = shuffleModeIsOn;
    }

    public static void SaveConfiguration()
    {
        try
        {
            string json = JsonSerializer.Serialize(_generalConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GeneralConfigFilePath, json);

            CurrentSpeakingSimulatorConfigSubject.OnNext(_generalConfig.SpeakingSimulatorConfig);
            CurrentJukeboxConfigSubject.OnNext(_generalConfig.JukeboxConfig);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save configuration: {ex.Message}");
        }
    }

    public static void ResetConfiguration()
    {
        LoadConfiguration();
        
        CurrentSpeakingSimulatorConfigSubject.OnNext(_generalConfig.SpeakingSimulatorConfig);
    }

    private static void LoadConfiguration()
    {
        if (!File.Exists(GeneralConfigFilePath))
        {
            _generalConfig = new GeneralConfig();

            return;
        }

        try
        {
            string json = File.ReadAllText(GeneralConfigFilePath);
            _generalConfig = JsonSerializer.Deserialize<GeneralConfig>(json) ?? new GeneralConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading configuration: {ex.Message}");
            
            _generalConfig = new GeneralConfig();
        }
    }
}