using System;
using System.Reactive.Disposables;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig;

public class GeneralConfigWindowViewModel : BaseViewModel, IDisposable
{
    private readonly CompositeDisposable _compositeDisposable;
    private readonly GeneralConfigWindow _window;
    private bool _isNarratorModeInstant;
    private bool _isNarratorModeSpeech;

    public GeneralConfigWindowViewModel(GeneralConfigWindow window)
    {
        _window = window;

        SaveCommand = new CommandHandler(Save);

        CancelCommand = new CommandHandler(Cancel);

        SetNarratorModeSpeechCommand = new CommandHandler(SetNarratorModeSpeech);

        SetNarratorModeInstantCommand = new CommandHandler(SetNarratorModeInstant);

        _compositeDisposable = new CompositeDisposable(
            GeneralConfiguration.NarratorMode.Subscribe(OnNarratorModeChanged)
        );
    }

    public bool IsNarratorModeInstant
    {
        get => _isNarratorModeInstant;
        private set => SetField(ref _isNarratorModeInstant, value);
    }

    public bool IsNarratorModeSpeech
    {
        get => _isNarratorModeSpeech;
        private set => SetField(ref _isNarratorModeSpeech, value);
    }

    public ICommand SaveCommand { get; private set; }

    public ICommand CancelCommand { get; private set; }

    public ICommand SetNarratorModeSpeechCommand { get; private set; }

    public ICommand SetNarratorModeInstantCommand { get; private set; }

    private void Save()
    {
        _window.Close();
    }

    private void Cancel()
    {
        _window.Close();
    }

    private void SetNarratorModeSpeech()
    {
        GeneralConfiguration.SetNarratorMode(NarratorMode.Speech);
    }

    private void SetNarratorModeInstant()
    {
        GeneralConfiguration.SetNarratorMode(NarratorMode.Instant);
    }

    private void OnNarratorModeChanged(NarratorMode mode)
    {
        IsNarratorModeSpeech = mode == NarratorMode.Speech;
        IsNarratorModeInstant = mode == NarratorMode.Instant;
    }

    public void Dispose()
    {
        _compositeDisposable.Dispose();
    }
}