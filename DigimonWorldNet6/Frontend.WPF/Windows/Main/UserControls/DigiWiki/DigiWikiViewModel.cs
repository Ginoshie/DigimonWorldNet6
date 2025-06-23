using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.FoodTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.HomeTopic;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki;

public class DigiWikiViewModel : BaseViewModel
{
    private readonly SpeakingSimulator _speakingSimulator = new();

    private CancellationTokenSource? _cancellationTokenSource;
    private UserControl _currentSelectedDigiWikiContent;

    private void SpeakShellmonText(string shellmonText, Action<string> updateTextAction, int delayInMs)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = _cancellationTokenSource.Token;

        int initialDelay = UserConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant ? 0 : delayInMs;

        Task.Delay(initialDelay, token)
            .WaitAsync(token)
            .ContinueWith(_ =>
            {
                Task writeTextAsSpeechTask = _speakingSimulator.WriteTextAsSpeechAsync(shellmonText, updateTextAction);

                return writeTextAsSpeechTask;
            });
    }

    private Action<string, Action<string>> SpeakShellmonTextLongDelayAction => (shellmonText, updateTextAction) => SpeakShellmonText(shellmonText, updateTextAction, 1500);

    private Action<string, Action<string>> SpeakShellmonTextShortDelayAction => (shellmonText, updateTextAction) => SpeakShellmonText(shellmonText, updateTextAction, 600);
    private Action<string, Action<string>> SpeakShellmonTextNoDelayAction => (shellmonText, updateTextAction) => SpeakShellmonText(shellmonText, updateTextAction, 0);

    public DigiWikiViewModel()
    {
        OpenFoodTopicCommand = new CommandHandler(OpenFoodTopic);

        _currentSelectedDigiWikiContent = new Home(SpeakShellmonTextLongDelayAction, InstantDisplay);
    }

    public ICommand OpenFoodTopicCommand { get; }

    public UserControl CurrentSelectedDigiWikiContent
    {
        get => _currentSelectedDigiWikiContent;
        private set => SetField(ref _currentSelectedDigiWikiContent, value);
    }

    private void OpenFoodTopic() => CurrentSelectedDigiWikiContent = new FoodTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);

    private void InstantDisplay()
    {
        _cancellationTokenSource?.Cancel();

        _speakingSimulator.RequestInstantDisplay();
    }
}