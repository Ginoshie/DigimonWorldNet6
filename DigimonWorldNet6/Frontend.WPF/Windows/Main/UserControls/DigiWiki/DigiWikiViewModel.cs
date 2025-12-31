using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Home;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Navigation.Chapters.ConditionsChapter;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Navigation.Chapters.TamerMechanicsChapter;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Navigation.MenuNavigation;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.AgeTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.AreaPreferenceTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FlowerTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FoodTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.HungryTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.InjuryTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.PoopTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.PoopyTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SickTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SleepTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SleepyTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.TirednessTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.TiredTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.WeightTopic;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki;

public class DigiWikiViewModel : BaseViewModel
{
    private readonly SpeakingSimulator _speakingSimulator = new();

    private CancellationTokenSource? _cancellationTokenSource;
    private UserControl _currentSelectedDigiWikiContent;
    private UserControl _currentSelectedMenuNavigation;

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
        OpenTamerMechanicChapterCommand = new CommandHandler(OpenTamerMechanicsChapter);

        OpenConditionsChapterCommand = new CommandHandler(OpenConditionsChapter);

        OpenChaptersCommand = new CommandHandler(OpenChapters);

        OpenFoodTopicCommand = new CommandHandler(OpenFoodTopic);

        OpenWeightTopicCommand = new CommandHandler(OpenWeightTopic);

        OpenPoopTopicCommand = new CommandHandler(OpenPoopTopic);

        OpenAgeTopicCommand = new CommandHandler(OpenAgeTopic);

        OpenTirednessTopicCommand = new CommandHandler(OpenTirednessTopic);

        OpenSleepTopicCommand = new CommandHandler(OpenSleepTopic);

        OpenAreaPreferenceTopicCommand = new CommandHandler(OpenAreaPreferenceTopic);

        OpenHungryTopicCommand = new CommandHandler(OpenHungryTopic);

        OpenPoopyTopicCommand = new CommandHandler(OpenPoopyTopic);

        OpenTiredTopicCommand = new CommandHandler(OpenTiredTopic);
        
        OpenSleepyTopicCommand = new CommandHandler(OpenSleepyTopic);

        OpenFlowerTopicCommand = new CommandHandler(OpenFlowerTopic);

        OpenInjuryTopicCommand = new CommandHandler(OpenInjuryTopic);

        OpenSickTopicCommand = new CommandHandler(OpenSickTopic);

        _currentSelectedDigiWikiContent = new Home(SpeakShellmonTextLongDelayAction, InstantDisplay);

        _currentSelectedMenuNavigation = new ChapterMenuNavigation();
    }

    public ICommand OpenTamerMechanicChapterCommand { get; }
    public ICommand OpenConditionsChapterCommand { get; }
    public ICommand OpenChaptersCommand { get; }
    public ICommand OpenFoodTopicCommand { get; }
    public ICommand OpenWeightTopicCommand { get; }
    public ICommand OpenPoopTopicCommand { get; }
    public ICommand OpenAgeTopicCommand { get; }
    public ICommand OpenTirednessTopicCommand { get; }
    public ICommand OpenSleepTopicCommand { get; }
    public ICommand OpenAreaPreferenceTopicCommand { get; }
    public ICommand OpenHungryTopicCommand { get; }
    public ICommand OpenPoopyTopicCommand { get; }
    public ICommand OpenTiredTopicCommand { get; }
    public ICommand OpenSleepyTopicCommand { get; }
    public ICommand OpenFlowerTopicCommand { get; }
    public ICommand OpenInjuryTopicCommand { get; }
    public ICommand OpenSickTopicCommand { get; }

    public UserControl CurrentSelectedDigiWikiContent
    {
        get => _currentSelectedDigiWikiContent;
        private set => SetField(ref _currentSelectedDigiWikiContent, value);
    }

    public UserControl CurrentSelectedMenuNavigation
    {
        get => _currentSelectedMenuNavigation;
        private set => SetField(ref _currentSelectedMenuNavigation, value);
    }

    private void OpenTamerMechanicsChapter() => CurrentSelectedMenuNavigation = new TamerMechanicTopicsMenuNavigation();
    private void OpenConditionsChapter() => CurrentSelectedMenuNavigation = new ConditionTopicsMenuNavigation();
    private void OpenChapters() => CurrentSelectedMenuNavigation = new ChapterMenuNavigation();
    private void OpenFoodTopic() => CurrentSelectedDigiWikiContent = new FoodTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenWeightTopic() => CurrentSelectedDigiWikiContent = new WeightTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenPoopTopic() => CurrentSelectedDigiWikiContent = new PoopTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenAgeTopic() => CurrentSelectedDigiWikiContent = new AgeTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenTirednessTopic() => CurrentSelectedDigiWikiContent = new TirednessTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenSleepTopic() => CurrentSelectedDigiWikiContent = new SleepTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenAreaPreferenceTopic() => CurrentSelectedDigiWikiContent = new AreaPreferenceTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenHungryTopic() => CurrentSelectedDigiWikiContent = new HungryTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenPoopyTopic() => CurrentSelectedDigiWikiContent = new PoopyTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenTiredTopic() => CurrentSelectedDigiWikiContent = new TiredTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenSleepyTopic() => CurrentSelectedDigiWikiContent = new SleepyTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenFlowerTopic() => CurrentSelectedDigiWikiContent = new FlowerTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenInjuryTopic() => CurrentSelectedDigiWikiContent = new InjuryTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);
    private void OpenSickTopic() => CurrentSelectedDigiWikiContent = new SickTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay);

    private void InstantDisplay()
    {
        _cancellationTokenSource?.Cancel();

        _speakingSimulator.RequestInstantDisplay();
    }
}