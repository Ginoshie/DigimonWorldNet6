using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Configuration;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig;

public class GeneralConfigWindowViewModel : BaseViewModel
{
    private readonly GeneralConfigWindow _window;

    private bool _narratorModeIsInstant;
    private bool _narratorModeIsSpeech;
    private int _volume;
    private bool _muteIsOn;
    private bool _muteIsOff;
    private bool _repeatModeIsSingle;
    private bool _repeatModeIsAll;
    private bool _shuffleModeIsOn;
    private bool _shuffleModeIsOff;
    private bool _pauseOnCloseWindow;
    private bool _doNothingOnCloseWindow;

    public GeneralConfigWindowViewModel(GeneralConfigWindow window)
    {
        _window = window;

        SaveCommand = new CommandHandler(Save);

        CancelCommand = new CommandHandler(Cancel);

        SetNarratorModeSpeechCommand = new CommandHandler(SetNarratorModeSpeech);

        SetNarratorModeInstantCommand = new CommandHandler(SetNarratorModeInstant);

        SetMuteOnCommand = new CommandHandler(SetMuteOn);

        SetMuteOffCommand = new CommandHandler(SetMuteOff);

        SetShuffleModeOnCommand = new CommandHandler(SetShuffleModeOn);

        SetShuffleModeOffCommand = new CommandHandler(SetShuffleModeOff);

        SetRepeatModeSingleCommand = new CommandHandler(SetRepeatModeSingle);

        SetRepeatModeAllCommand = new CommandHandler(SetRepeatModeAll);

        SetPauseOnCloseCommand = new CommandHandler(SetPauseOnClose);

        SetDoNothingOnCloseCommand = new CommandHandler(SetDoNothingOnClose);

        ApplyConfig(GeneralConfigurationManager.GeneralConfig);
    }

    public bool IsNarratorModeInstant
    {
        get => _narratorModeIsInstant;
        private set => SetField(ref _narratorModeIsInstant, value);
    }

    public bool IsNarratorModeSpeech
    {
        get => _narratorModeIsSpeech;
        private set => SetField(ref _narratorModeIsSpeech, value);
    }

    public int Volume
    {
        get => _volume;
        set
        {
            SetField(ref _volume, value);
            
            GeneralConfigurationManager.SetVolume(_volume);
        }
    }

    public bool MuteIsOn
    {
        get => _muteIsOn;
        private set => SetField(ref _muteIsOn, value);
    }

    public bool MuteIsOff
    {
        get => _muteIsOff;
        private set => SetField(ref _muteIsOff, value);
    }

    public bool ShuffleModeIsOn
    {
        get => _shuffleModeIsOn;
        private set => SetField(ref _shuffleModeIsOn, value);
    }

    public bool ShuffleModeIsOff
    {
        get => _shuffleModeIsOff;
        private set => SetField(ref _shuffleModeIsOff, value);
    }

    public bool RepeatModeIsSingle
    {
        get => _repeatModeIsSingle;
        private set => SetField(ref _repeatModeIsSingle, value);
    }

    public bool RepeatModeIsAll
    {
        get => _repeatModeIsAll;
        private set => SetField(ref _repeatModeIsAll, value);
    }

    public bool PauseOnCloseWindow
    {
        get => _pauseOnCloseWindow;
        private set => SetField(ref _pauseOnCloseWindow, value);
    }

    public bool DoNothingOnCloseWindow
    {
        get => _doNothingOnCloseWindow;
        private set => SetField(ref _doNothingOnCloseWindow, value);
    }

    public ICommand SaveCommand { get; private set; }

    public ICommand CancelCommand { get; private set; }

    public ICommand SetNarratorModeSpeechCommand { get; private set; }

    public ICommand SetNarratorModeInstantCommand { get; private set; }

    public ICommand SetMuteOnCommand { get; private set; }

    public ICommand SetMuteOffCommand { get; private set; }

    public ICommand SetShuffleModeOnCommand { get; private set; }

    public ICommand SetShuffleModeOffCommand { get; private set; }

    public ICommand SetRepeatModeSingleCommand { get; private set; }

    public ICommand SetRepeatModeAllCommand { get; private set; }

    public ICommand SetPauseOnCloseCommand { get; private set; }

    public ICommand SetDoNothingOnCloseCommand { get; private set; }

    private void Save()
    {
        GeneralConfigurationManager.SaveConfiguration();

        _window.Close();
    }

    private void Cancel()
    {
        GeneralConfigurationManager.ResetConfiguration();

        _window.Close();
    }

    private void SetNarratorModeSpeech()
    {
        GeneralConfigurationManager.SetNarratorMode(NarratorMode.Speech);

        IsNarratorModeSpeech = true;
        IsNarratorModeInstant = false;
    }

    private void SetNarratorModeInstant()
    {
        GeneralConfigurationManager.SetNarratorMode(NarratorMode.Instant);

        IsNarratorModeSpeech = false;
        IsNarratorModeInstant = true;
    }

    private void SetMuteOn()
    {
        GeneralConfigurationManager.SetMuteIsOn(MuteMode.Mute);

        MuteIsOn = true;
        MuteIsOff = false;
    }

    private void SetMuteOff()
    {
        GeneralConfigurationManager.SetMuteIsOn(MuteMode.Unmuted);

        MuteIsOn = false;
        MuteIsOff = true;
    }

    private void SetRepeatModeSingle()
    {
        GeneralConfigurationManager.SetRepeatModeIsSingle(RepeatMode.Single);

        RepeatModeIsSingle = true;
        RepeatModeIsAll = false;
    }

    private void SetRepeatModeAll()
    {
        GeneralConfigurationManager.SetRepeatModeIsSingle(RepeatMode.All);

        RepeatModeIsSingle = false;
        RepeatModeIsAll = true;
    }

    private void SetPauseOnClose()
    {
        GeneralConfigurationManager.SetOnCloseAction(MusicPlayerOnCloseAction.Pause);

        PauseOnCloseWindow = true;
        DoNothingOnCloseWindow = false;
    }

    private void SetDoNothingOnClose()
    {
        GeneralConfigurationManager.SetOnCloseAction(MusicPlayerOnCloseAction.Nothing);

        PauseOnCloseWindow = false;
        DoNothingOnCloseWindow = true;
    }

    private void SetShuffleModeOn()
    {
        GeneralConfigurationManager.SetShuffleModeIsOn(ShuffleMode.Shuffle);

        ShuffleModeIsOn = true;
        ShuffleModeIsOff = false;
    }

    private void SetShuffleModeOff()
    {
        GeneralConfigurationManager.SetShuffleModeIsOn(ShuffleMode.Chronological);

        ShuffleModeIsOn = false;
        ShuffleModeIsOff = true;
    }

    private void ApplyConfig(Configuration.GeneralConfig generalConfig)
    {
        MusicPlayerConfig musicPlayerConfig = generalConfig.MusicPlayerConfig;
        
        Volume = musicPlayerConfig.Volume;
        
        MuteIsOn = musicPlayerConfig.MuteMode == MuteMode.Mute;
        MuteIsOff = musicPlayerConfig.MuteMode == MuteMode.Unmuted;

        ShuffleModeIsOn = musicPlayerConfig.ShuffleMode == ShuffleMode.Shuffle;
        ShuffleModeIsOff = musicPlayerConfig.ShuffleMode == ShuffleMode.Chronological;

        RepeatModeIsSingle = musicPlayerConfig.RepeatMode == RepeatMode.Single;
        RepeatModeIsAll = musicPlayerConfig.RepeatMode == RepeatMode.All;
        
        PauseOnCloseWindow = musicPlayerConfig.OnCloseAction == MusicPlayerOnCloseAction.Pause;
        _doNothingOnCloseWindow = musicPlayerConfig.OnCloseAction == MusicPlayerOnCloseAction.Nothing;
        
        SpeakingSimulatorConfig speakingSimulatorConfig = generalConfig.SpeakingSimulatorConfig;
        
        IsNarratorModeSpeech = speakingSimulatorConfig.NarratorMode == NarratorMode.Speech;
        IsNarratorModeInstant = speakingSimulatorConfig.NarratorMode == NarratorMode.Instant;
    }
}