using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.GeneralConfig.UserControls;
using Generics.Configuration;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig;

public class GeneralConfigWindowViewModel : BaseViewModel
{
    private readonly GeneralConfigWindow _window;
    private readonly HomeConfigurationSection _homeConfigurationSection = new();
    private readonly MusicPlayerConfigurationSection _musicPlayerConfigurationSection = new();
    private readonly NarrationConfigurationSection _narrationConfigurationSection = new();
    private readonly EvolutionConfigurationSection _evolutionConfigurationSection = new();

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
    private EvolutionCalculatorMode _evolutionCalculatorMode;
    private bool _evolutionCalculatorModeOriginal;
    private bool _evolutionCalculatorModeVice;
    private bool _evolutionCalculatorModeViceMyotismon;
    private bool _evolutionCalculatorModeVicePanjyamon;

    private UserControl _currentSelectedSettingCategoryUserControl;

    public GeneralConfigWindowViewModel(GeneralConfigWindow window)
    {
        _window = window;

        SaveCommand = new CommandHandler(Save);

        CloseCommand = new CommandHandler(Close);

        ShowHomeConfigurationSectionCommand = new CommandHandler(ShowHomeConfigurationSection);

        ShowMusicPlayerConfigurationSectionCommand = new CommandHandler(ShowMusicPlayerConfigurationSection);

        ShowNarrationConfigurationSectionCommand = new CommandHandler(ShowNarrationConfigurationSection);

        ShowEvolutionConfigurationSectionCommand = new CommandHandler(ShowEvolutionConfigurationSection);

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

        SetEvolutionCalculatorModeOriginalCommand = new CommandHandler(() => SetEvolutionCalculatorMode(EvolutionCalculatorMode.Original));

        SetEvolutionCalculatorModeViceCommand = new CommandHandler(() => SetEvolutionCalculatorMode(EvolutionCalculatorMode.Vice));

        SetEvolutionCalculatorModeViceMyotismonCommand = new CommandHandler(() => SetEvolutionCalculatorMode(EvolutionCalculatorMode.ViceMyotismon));

        SetEvolutionCalculatorVicePanjyamonCommand = new CommandHandler(() => SetEvolutionCalculatorMode(EvolutionCalculatorMode.VicePanjyamon));

        _currentSelectedSettingCategoryUserControl = new HomeConfigurationSection();

        LoadConfig(UserConfigurationManager.UserConfiguration);
    }

    public bool HomeConfigurationSectionIsSelected => CurrentSelectedSettingCategoryUserControl.GetType() == typeof(HomeConfigurationSection);

    public bool MusicPlayerConfigurationSectionIsSelected => CurrentSelectedSettingCategoryUserControl.GetType() == typeof(MusicPlayerConfigurationSection);

    public bool NarrationConfigurationSectionIsSelected => CurrentSelectedSettingCategoryUserControl.GetType() == typeof(NarrationConfigurationSection);
    public bool EvolutionConfigurationSectionIsSelected => CurrentSelectedSettingCategoryUserControl.GetType() == typeof(EvolutionConfigurationSection);

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

            UserConfigurationManager.SetVolume(_volume);
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

    public bool EvolutionCalculatorModeIsOriginal
    {
        get => _evolutionCalculatorModeOriginal;
        private set => SetField(ref _evolutionCalculatorModeOriginal, value);
    }

    public bool EvolutionCalculatorModeIsVice
    {
        get => _evolutionCalculatorModeVice;
        private set => SetField(ref _evolutionCalculatorModeVice, value);
    }

    public bool EvolutionCalculatorModeIsViceMyotismon
    {
        get => _evolutionCalculatorModeViceMyotismon;
        private set => SetField(ref _evolutionCalculatorModeViceMyotismon, value);
    }

    public bool EvolutionCalculatorModeIsVicePanjyamon
    {
        get => _evolutionCalculatorModeVicePanjyamon;
        private set => SetField(ref _evolutionCalculatorModeVicePanjyamon, value);
    }

    public UserControl CurrentSelectedSettingCategoryUserControl
    {
        get => _currentSelectedSettingCategoryUserControl;
        private set
        {
            LoadConfig(UserConfigurationManager.UserConfiguration);

            SetField(ref _currentSelectedSettingCategoryUserControl, value);

            OnPropertyChanged(nameof(HomeConfigurationSectionIsSelected));
            OnPropertyChanged(nameof(MusicPlayerConfigurationSectionIsSelected));
            OnPropertyChanged(nameof(NarrationConfigurationSectionIsSelected));
        }
    }

    public ICommand SaveCommand { get; private set; }

    public ICommand CloseCommand { get; private set; }

    public ICommand ShowHomeConfigurationSectionCommand { get; private set; }

    public ICommand ShowMusicPlayerConfigurationSectionCommand { get; private set; }

    public ICommand ShowNarrationConfigurationSectionCommand { get; private set; }

    public ICommand ShowEvolutionConfigurationSectionCommand { get; private set; }

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

    public ICommand SetEvolutionCalculatorModeOriginalCommand { get; private set; }

    public ICommand SetEvolutionCalculatorModeViceCommand { get; private set; }

    public ICommand SetEvolutionCalculatorModeViceMyotismonCommand { get; private set; }

    public ICommand SetEvolutionCalculatorVicePanjyamonCommand { get; private set; }

    private void Save() => SaveConfiguration();

    private void Close() => _window.Close();

    private void ShowHomeConfigurationSection() => CurrentSelectedSettingCategoryUserControl = _homeConfigurationSection;

    private void ShowMusicPlayerConfigurationSection() => CurrentSelectedSettingCategoryUserControl = _musicPlayerConfigurationSection;

    private void ShowNarrationConfigurationSection() => CurrentSelectedSettingCategoryUserControl = _narrationConfigurationSection;

    private void ShowEvolutionConfigurationSection() => CurrentSelectedSettingCategoryUserControl = _evolutionConfigurationSection;

    private void SetNarratorModeSpeech()
    {
        IsNarratorModeSpeech = true;
        IsNarratorModeInstant = false;
    }

    private void SetNarratorModeInstant()
    {
        IsNarratorModeSpeech = false;
        IsNarratorModeInstant = true;
    }

    private void SetMuteOn()
    {
        MuteIsOn = true;
        MuteIsOff = false;
    }

    private void SetMuteOff()
    {
        MuteIsOn = false;
        MuteIsOff = true;
    }

    private void SetShuffleModeOn()
    {
        ShuffleModeIsOn = true;
        ShuffleModeIsOff = false;
    }

    private void SetShuffleModeOff()
    {
        ShuffleModeIsOn = false;
        ShuffleModeIsOff = true;
    }

    private void SetRepeatModeSingle()
    {
        RepeatModeIsSingle = true;
        RepeatModeIsAll = false;
    }

    private void SetRepeatModeAll()
    {
        RepeatModeIsSingle = false;
        RepeatModeIsAll = true;
    }

    private void SetPauseOnClose()
    {
        PauseOnCloseWindow = true;
        DoNothingOnCloseWindow = false;
    }

    private void SetDoNothingOnClose()
    {
        PauseOnCloseWindow = false;
        DoNothingOnCloseWindow = true;
    }

    private void SetEvolutionCalculatorMode(EvolutionCalculatorMode mode)
    {
        _evolutionCalculatorMode = mode;

        EvolutionCalculatorModeIsOriginal = mode == EvolutionCalculatorMode.Original;
        EvolutionCalculatorModeIsVice = mode == EvolutionCalculatorMode.Vice;
        EvolutionCalculatorModeIsViceMyotismon = mode == EvolutionCalculatorMode.ViceMyotismon;
        EvolutionCalculatorModeIsVicePanjyamon = mode == EvolutionCalculatorMode.VicePanjyamon;
    }

    private void LoadConfig(UserConfiguration userConfiguration)
    {
        MusicPlayerConfig musicPlayerConfig = userConfiguration.MusicPlayerConfig;

        Volume = musicPlayerConfig.Volume;

        MuteIsOn = musicPlayerConfig.MuteMode == MuteMode.Mute;
        MuteIsOff = musicPlayerConfig.MuteMode == MuteMode.Unmuted;

        ShuffleModeIsOn = musicPlayerConfig.ShuffleMode == ShuffleMode.Shuffle;
        ShuffleModeIsOff = musicPlayerConfig.ShuffleMode == ShuffleMode.Chronological;

        RepeatModeIsSingle = musicPlayerConfig.RepeatMode == RepeatMode.Single;
        RepeatModeIsAll = musicPlayerConfig.RepeatMode == RepeatMode.All;

        PauseOnCloseWindow = musicPlayerConfig.OnCloseAction == MusicPlayerOnCloseAction.Pause;
        _doNothingOnCloseWindow = musicPlayerConfig.OnCloseAction == MusicPlayerOnCloseAction.Nothing;

        SpeakingSimulatorConfig speakingSimulatorConfig = userConfiguration.SpeakingSimulatorConfig;

        IsNarratorModeSpeech = speakingSimulatorConfig.NarratorMode == NarratorMode.Speech;
        IsNarratorModeInstant = speakingSimulatorConfig.NarratorMode == NarratorMode.Instant;
        
        SetEvolutionCalculatorMode(userConfiguration.EvolutionCalculatorConfig.EvolutionCalculatorMode);
    }

    private void SaveConfiguration()
    {
        UserConfigurationManager.SetNarratorMode(IsNarratorModeSpeech ? NarratorMode.Speech : NarratorMode.Instant);
        UserConfigurationManager.SetMuteIsOn(MuteIsOn ? MuteMode.Mute : MuteMode.Unmuted);
        UserConfigurationManager.SetShuffleModeIsOn(ShuffleModeIsOn ? ShuffleMode.Shuffle : ShuffleMode.Chronological);
        UserConfigurationManager.SetRepeatModeIsSingle(RepeatModeIsSingle ? RepeatMode.Single : RepeatMode.All);
        UserConfigurationManager.SetOnCloseAction(PauseOnCloseWindow ? MusicPlayerOnCloseAction.Pause : MusicPlayerOnCloseAction.Nothing);
        UserConfigurationManager.SetEvolutionCalculatorMode(_evolutionCalculatorMode);

        UserConfigurationManager.SaveConfiguration();
    }
}