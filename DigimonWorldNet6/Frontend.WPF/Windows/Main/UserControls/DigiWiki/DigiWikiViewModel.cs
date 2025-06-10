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

    private UserControl _currentSelectedDigiWikiContent;

    private void SpeakShellmonText(string shellmonText, Action<string> updateTextAction, int delayInMs)
    {
        int initialDelay = UserConfigurationManager.SpeakingSimulatorConfig.NarratorMode == NarratorMode.Instant ? 0 : delayInMs;

        Task.Delay(initialDelay)
            .WaitAsync(CancellationToken.None)
            .ContinueWith(_ => _speakingSimulator.WriteTextAsSpeechAsync(shellmonText, updateTextAction));
    }

    private Action<string, Action<string>> SpeakShellmonTextActionLongDelay => (shellmonText, updateTextAction) => SpeakShellmonText(shellmonText, updateTextAction, 1500);

    private Action<string, Action<string>> SpeakShellmonTextWithDelayActionShortDelay => (shellmonText, updateTextAction) => SpeakShellmonText(shellmonText, updateTextAction, 600);

    public DigiWikiViewModel()
    {
        OpenFoodTopicCommand = new CommandHandler(OpenFoodTopic);

        _currentSelectedDigiWikiContent = new HomeTopic(SpeakShellmonTextActionLongDelay, InstantDisplay);
    }

    public ICommand OpenFoodTopicCommand { get; }

    public UserControl CurrentSelectedDigiWikiContent
    {
        get => _currentSelectedDigiWikiContent;
        private set => SetField(ref _currentSelectedDigiWikiContent, value);
    }

    private void OpenFoodTopic() => CurrentSelectedDigiWikiContent = new FoodTopicUserControl(SpeakShellmonTextWithDelayActionShortDelay, InstantDisplay);

    private void InstantDisplay() => _speakingSimulator.RequestInstantDisplay();
}