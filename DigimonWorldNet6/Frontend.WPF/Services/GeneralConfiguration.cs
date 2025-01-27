using System;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using DigimonWorld.Frontend.WPF.Configuration;
using DigimonWorld.Frontend.WPF.Constants;

namespace DigimonWorld.Frontend.WPF.Services;

public static class GeneralConfiguration
{
    private static readonly string SpeakingSimulatorConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeneralConfiguration.json");

    private static readonly BehaviorSubject<SpeakingSimulatorConfig> CurrentSpeakingSimulatorConfigSubject;

    private static SpeakingSimulatorConfig _speakingSimulatorConfig = null!;

    static GeneralConfiguration()
    {
        LoadConfiguration();

        CurrentSpeakingSimulatorConfigSubject = new BehaviorSubject<SpeakingSimulatorConfig>(_speakingSimulatorConfig);
        CurrentSpeakingSimulatorConfig = CurrentSpeakingSimulatorConfigSubject.AsObservable();
    }

    public static IObservable<SpeakingSimulatorConfig> CurrentSpeakingSimulatorConfig { get; }

    public static void SetNarratorMode(NarratorMode mode)
    {
        _speakingSimulatorConfig.NarratorMode = mode;
    }

    public static void SaveConfiguration()
    {
        try
        {
            string json = JsonSerializer.Serialize(_speakingSimulatorConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SpeakingSimulatorConfigFilePath, json);

            CurrentSpeakingSimulatorConfigSubject.OnNext(_speakingSimulatorConfig);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save configuration: {ex.Message}");
        }
    }

    public static void ResetConfiguration()
    {
        LoadConfiguration();
        
        CurrentSpeakingSimulatorConfigSubject.OnNext(_speakingSimulatorConfig);
    }

    private static void LoadConfiguration()
    {
        if (!File.Exists(SpeakingSimulatorConfigFilePath))
        {
            _speakingSimulatorConfig = new SpeakingSimulatorConfig();

            return;
        }

        try
        {
            string json = File.ReadAllText(SpeakingSimulatorConfigFilePath);
            _speakingSimulatorConfig = JsonSerializer.Deserialize<SpeakingSimulatorConfig>(json) ?? new SpeakingSimulatorConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading configuration: {ex.Message}");
            
            _speakingSimulatorConfig = new SpeakingSimulatorConfig();
        }
    }
}