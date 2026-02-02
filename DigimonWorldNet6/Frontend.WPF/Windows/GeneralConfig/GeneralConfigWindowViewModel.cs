using System;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.GeneralConfig.UserControls;
using Shared.Configuration;
using Shared.Enums;
using Shared.Services;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig;

public class GeneralConfigWindowViewModel : BaseViewModel
{
    private readonly GeneralConfigWindow _window;
    private readonly HomeConfigurationSection _homeConfigurationSection = new();
    private readonly MusicPlayerConfigurationSection _musicPlayerConfigurationSection = new();
    private readonly NarrationConfigurationSection _narrationConfigurationSection = new();
    private readonly EvolutionConfigurationSection _evolutionConfigurationSection = new();

    private GameVariant _gameVariant;
    private UserControl _currentSelectedSettingCategoryUserControl;

    public GeneralConfigWindowViewModel(GeneralConfigWindow window)
    {
        _window = window;

        SaveCommand = new CommandHandler(Save);
        CloseCommand = new CommandHandler(Close);

        ShowHomeConfigurationSectionCommand = new CommandHandler(() => CurrentSelectedSettingCategoryUserControl = _homeConfigurationSection);
        ShowMusicPlayerConfigurationSectionCommand = new CommandHandler(() => CurrentSelectedSettingCategoryUserControl = _musicPlayerConfigurationSection);
        ShowNarrationConfigurationSectionCommand = new CommandHandler(() => CurrentSelectedSettingCategoryUserControl = _narrationConfigurationSection);
        ShowEvolutionConfigurationSectionCommand = new CommandHandler(() => CurrentSelectedSettingCategoryUserControl = _evolutionConfigurationSection);

        SetNarratorModeSpeechCommand = new CommandHandler(() => SetNarratorMode(NarratorMode.Speech));
        SetNarratorModeInstantCommand = new CommandHandler(() => SetNarratorMode(NarratorMode.Instant));

        SetMuteOnCommand = new CommandHandler(() => SetMuteMode(MuteMode.Mute));
        SetMuteOffCommand = new CommandHandler(() => SetMuteMode(MuteMode.Unmuted));

        SetShuffleModeOnCommand = new CommandHandler(() => SetShuffleMode(ShuffleMode.Shuffle));
        SetShuffleModeOffCommand = new CommandHandler(() => SetShuffleMode(ShuffleMode.Chronological));

        SetRepeatModeSingleCommand = new CommandHandler(() => SetRepeatMode(RepeatMode.Single));
        SetRepeatModeAllCommand = new CommandHandler(() => SetRepeatMode(RepeatMode.All));

        SetPauseOnCloseCommand = new CommandHandler(() => SetOnCloseAction(MusicPlayerOnCloseAction.Pause));
        SetDoNothingOnCloseCommand = new CommandHandler(() => SetOnCloseAction(MusicPlayerOnCloseAction.Nothing));

        SetEvolutionCalculatorModeOriginalCommand = new CommandHandler(() => SetEvolutionCalculatorBaseMode(GameVariant.Original));
        SetEvolutionCalculatorModeViceCommand = new CommandHandler(() => SetEvolutionCalculatorBaseMode(GameVariant.Vice));
        SetEvolutionCalculatorModeViceMyotismonEnabledCommand = new CommandHandler(() => SetVicePatch(GameVariant.MyotismonPatch, true));
        SetEvolutionCalculatorModeViceMyotismonDisabledCommand = new CommandHandler(() => SetVicePatch(GameVariant.MyotismonPatch, false));
        SetEvolutionCalculatorModeVicePanjyamonEnabledCommand = new CommandHandler(() => SetVicePatch(GameVariant.PanjyamonPatch, true));
        SetEvolutionCalculatorModeVicePanjyamonDisabledCommand = new CommandHandler(() => SetVicePatch(GameVariant.PanjyamonPatch, false));

        _currentSelectedSettingCategoryUserControl = _homeConfigurationSection;

        LoadConfig(UserConfigurationManager.UserConfiguration);
    }

    #region UserControl Selection Properties

    public bool HomeConfigurationSectionIsSelected => CurrentSelectedSettingCategoryUserControl.GetType() == typeof(HomeConfigurationSection);
    public bool MusicPlayerConfigurationSectionIsSelected => CurrentSelectedSettingCategoryUserControl.GetType() == typeof(MusicPlayerConfigurationSection);
    public bool NarrationConfigurationSectionIsSelected => CurrentSelectedSettingCategoryUserControl.GetType() == typeof(NarrationConfigurationSection);
    public bool EvolutionConfigurationSectionIsSelected => CurrentSelectedSettingCategoryUserControl.GetType() == typeof(EvolutionConfigurationSection);

    #endregion

    #region Audio & Narration Properties

    public bool IsNarratorModeInstant
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool IsNarratorModeSpeech
    {
        get;
        private set => SetField(ref field, value);
    }

    public int Volume
    {
        get;
        set
        {
            SetField(ref field, value);
            UserConfigurationManager.SetVolume(field);
        }
    }

    public bool MuteIsOn
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool MuteIsOff
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool ShuffleModeIsOn
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool ShuffleModeIsOff
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool RepeatModeIsSingle
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool RepeatModeIsAll
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool PauseOnCloseWindow
    {
        get;
        private set => SetField(ref field, value);
    }

    public bool DoNothingOnCloseWindow
    {
        get;
        private set => SetField(ref field, value);
    }

    #endregion

    #region Game Variant Properties

    public bool EvolutionCalculatorModeIsOriginal => _gameVariant.HasFlag(GameVariant.Original);
    public bool EvolutionCalculatorModeIsVice => _gameVariant.HasFlag(GameVariant.Vice);
    public bool ViceMyotismonEnabled => EvolutionCalculatorModeIsVice && _gameVariant.HasFlag(GameVariant.MyotismonPatch);
    public bool VicePanjyamonEnabled => EvolutionCalculatorModeIsVice && _gameVariant.HasFlag(GameVariant.PanjyamonPatch);
    public bool ViceMyotismonDisabled => !ViceMyotismonEnabled;
    public bool VicePanjyamonDisabled => !VicePanjyamonEnabled;
    public bool GameVariantContainsVice => _gameVariant.HasFlag(GameVariant.Vice);

    #endregion

    #region Current UserControl

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
            OnPropertyChanged(nameof(EvolutionConfigurationSectionIsSelected));
        }
    }

    #endregion

    #region Commands

    public ICommand SaveCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand ShowHomeConfigurationSectionCommand { get; }
    public ICommand ShowMusicPlayerConfigurationSectionCommand { get; }
    public ICommand ShowNarrationConfigurationSectionCommand { get; }
    public ICommand ShowEvolutionConfigurationSectionCommand { get; }
    public ICommand SetNarratorModeSpeechCommand { get; }
    public ICommand SetNarratorModeInstantCommand { get; }
    public ICommand SetMuteOnCommand { get; }
    public ICommand SetMuteOffCommand { get; }
    public ICommand SetShuffleModeOnCommand { get; }
    public ICommand SetShuffleModeOffCommand { get; }
    public ICommand SetRepeatModeSingleCommand { get; }
    public ICommand SetRepeatModeAllCommand { get; }
    public ICommand SetPauseOnCloseCommand { get; }
    public ICommand SetDoNothingOnCloseCommand { get; }
    public ICommand SetEvolutionCalculatorModeOriginalCommand { get; }
    public ICommand SetEvolutionCalculatorModeViceCommand { get; }
    public ICommand SetEvolutionCalculatorModeViceMyotismonEnabledCommand { get; }
    public ICommand SetEvolutionCalculatorModeViceMyotismonDisabledCommand { get; }
    public ICommand SetEvolutionCalculatorModeVicePanjyamonEnabledCommand { get; }
    public ICommand SetEvolutionCalculatorModeVicePanjyamonDisabledCommand { get; }

    #endregion

    #region Methods

    private void Save() => SaveConfiguration();
    private void Close() => _window.Close();

    private void SetNarratorMode(NarratorMode mode)
    {
        IsNarratorModeSpeech = mode == NarratorMode.Speech;
        IsNarratorModeInstant = mode == NarratorMode.Instant;
        
        UserConfigurationManager.SetNarratorMode(mode);
    }

    private void SetMuteMode(MuteMode mode)
    {
        MuteIsOn = mode == MuteMode.Mute;
        MuteIsOff = mode == MuteMode.Unmuted;
    }

    private void SetShuffleMode(ShuffleMode mode)
    {
        ShuffleModeIsOn = mode == ShuffleMode.Shuffle;
        ShuffleModeIsOff = mode == ShuffleMode.Chronological;
    }

    private void SetRepeatMode(RepeatMode mode)
    {
        RepeatModeIsSingle = mode == RepeatMode.Single;
        RepeatModeIsAll = mode == RepeatMode.All;
    }

    private void SetOnCloseAction(MusicPlayerOnCloseAction action)
    {
        PauseOnCloseWindow = action == MusicPlayerOnCloseAction.Pause;
        DoNothingOnCloseWindow = action == MusicPlayerOnCloseAction.Nothing;
    }

    private void SetEvolutionCalculatorBaseMode(GameVariant baseMode)
    {
        if (baseMode != GameVariant.Original && baseMode != GameVariant.Vice)
        {
            throw new ArgumentException("Only Original or Vice are valid base modes.");
        }

        _gameVariant &= ~(GameVariant.Original | GameVariant.Vice);
        _gameVariant |= baseMode;

        if (baseMode != GameVariant.Vice)
        {
            _gameVariant &= ~(GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch);
        }

        RaiseEvolutionCalculatorProperties();
        ApplyEvolutionCalculatorMode();
    }

    private void SetVicePatch(GameVariant gameVariant, bool enabled)
    {
        if (!EvolutionCalculatorModeIsVice)
        {
            return;
        }

        if (enabled)
        {
            _gameVariant |= gameVariant;
        }
        else
        {
            _gameVariant &= ~gameVariant;
        }

        RaiseEvolutionCalculatorProperties();
        ApplyEvolutionCalculatorMode();
    }

    private void ApplyEvolutionCalculatorMode()
    {
        UserConfigurationManager.SetEvolutionCalculatorMode(_gameVariant);
    }

    private void RaiseEvolutionCalculatorProperties()
    {
        OnPropertyChanged(nameof(EvolutionCalculatorModeIsOriginal));
        OnPropertyChanged(nameof(EvolutionCalculatorModeIsVice));
        OnPropertyChanged(nameof(ViceMyotismonEnabled));
        OnPropertyChanged(nameof(VicePanjyamonEnabled));
        OnPropertyChanged(nameof(ViceMyotismonDisabled));
        OnPropertyChanged(nameof(VicePanjyamonDisabled));
        OnPropertyChanged(nameof(GameVariantContainsVice));
    }

    private void LoadConfig(UserConfiguration config)
    {
        MusicPlayerConfig music = config.MusicPlayerConfig;
        Volume = music.Volume;
        SetMuteMode(music.MuteMode);
        SetShuffleMode(music.ShuffleMode);
        SetRepeatMode(music.RepeatMode);
        SetOnCloseAction(music.OnCloseAction);

        SpeakingSimulatorConfig sim = config.SpeakingSimulatorConfig;
        SetNarratorMode(sim.NarratorMode);

        _gameVariant = config.EvolutionCalculatorConfig.GameVariant;
        RaiseEvolutionCalculatorProperties();
    }

    private void SaveConfiguration()
    {
        UserConfigurationManager.SetNarratorMode(IsNarratorModeSpeech ? NarratorMode.Speech : NarratorMode.Instant);
        UserConfigurationManager.SetMuteIsOn(MuteIsOn ? MuteMode.Mute : MuteMode.Unmuted);
        UserConfigurationManager.SetShuffleModeIsOn(ShuffleModeIsOn ? ShuffleMode.Shuffle : ShuffleMode.Chronological);
        UserConfigurationManager.SetRepeatModeIsSingle(RepeatModeIsSingle ? RepeatMode.Single : RepeatMode.All);
        UserConfigurationManager.SetOnCloseAction(PauseOnCloseWindow ? MusicPlayerOnCloseAction.Pause : MusicPlayerOnCloseAction.Nothing);
        UserConfigurationManager.SetEvolutionCalculatorMode(_gameVariant);
        UserConfigurationManager.SaveConfiguration();
    }

    #endregion
}