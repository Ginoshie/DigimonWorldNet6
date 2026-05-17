using System;
using System.Windows.Controls;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Enums;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Home;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Navigation.Chapters.CombatChapter;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Navigation.Chapters.ConditionsChapter;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Navigation.Chapters.TamerMechanicsChapter;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Navigation.MenuNavigation;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.AgeTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.AreaPreferenceTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FinisherFormulaTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FinisherTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FlowerTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FoodTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.HungryTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.InjuredTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.NormalAttackTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.PoopTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.PoopyTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.PraisingTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.ScoldingTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SickTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SleepTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SleepyTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SpecialtyFactorTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.TirednessTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.TiredTopic;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.WeightTopic;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki;

public class DigiWikiViewModel : BaseViewModel, IDisposable
{
    private readonly SpeakingSimulator _speakingSimulator = new();

    private UserControl _currentSelectedDigiWikiContent;
    private UserControl _currentSelectedMenuNavigation;

    private void SpeakShellmonTextAsync(string shellmonText, Action<string> updateTextAction, SpeechDelay delayInMs)
    {
        _ = _speakingSimulator.SpeakAsync(
            shellmonText,
            updateTextAction,
            delayInMs);
    }

    private Action<string, Action<string>> SpeakShellmonTextLongDelayAction => (text, update) => SpeakShellmonTextAsync(text, update, SpeechDelay.Long);

    private Action<string, Action<string>> SpeakShellmonTextShortDelayAction => (text, update) => SpeakShellmonTextAsync(text, update, SpeechDelay.Short);

    private Action<string, Action<string>> SpeakShellmonTextNoDelayAction => (text, update) => SpeakShellmonTextAsync(text, update, SpeechDelay.None);


    public DigiWikiViewModel()
    {
        OpenCombatChapterCommand = new CommandHandler(OpenCombatChapter);

        OpenTamerMechanicChapterCommand = new CommandHandler(OpenTamerMechanicsChapter);

        OpenConditionsChapterCommand = new CommandHandler(OpenConditionsChapter);

        OpenChaptersCommand = new CommandHandler(OpenChapters);

        OpenPraisingTopicCommand = new CommandHandler(OpenPraisingTopic);

        OpenNormalAttackTopicCommand = new CommandHandler(OpenNormalAttackTopic);

        OpenFinisherTopicCommand = new CommandHandler(OpenFinisherTopic);

        OpenFinisherFormulaTopicCommand = new CommandHandler(OpenFinisherFormulaTopic);

        OpenSpecialtyFactorTopicCommand = new CommandHandler(OpenSpecialtyFactorTopic);

        OpenScoldingTopicCommand = new CommandHandler(OpenScoldingTopic);

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

    public ICommand OpenCombatChapterCommand { get; }
    public ICommand OpenTamerMechanicChapterCommand { get; }
    public ICommand OpenConditionsChapterCommand { get; }
    public ICommand OpenChaptersCommand { get; }
    public ICommand OpenPraisingTopicCommand { get; }
    public ICommand OpenNormalAttackTopicCommand { get; }
    public ICommand OpenFinisherTopicCommand { get; }
    public ICommand OpenFinisherFormulaTopicCommand { get; }
    public ICommand OpenSpecialtyFactorTopicCommand { get; }

    public string ActiveTopic
    {
        get;
        private set => SetField(ref field, value);
    } = string.Empty;

    public ICommand OpenScoldingTopicCommand { get; }
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

    private void OpenCombatChapter() => CurrentSelectedMenuNavigation = new CombatTopicsMenuNavigation();
    private void OpenTamerMechanicsChapter() { ActiveTopic = string.Empty; CurrentSelectedMenuNavigation = new TamerMechanicTopicsMenuNavigation(); }
    private void OpenConditionsChapter() { ActiveTopic = string.Empty; CurrentSelectedMenuNavigation = new ConditionTopicsMenuNavigation(); }
    private void OpenChapters() { ActiveTopic = string.Empty; CurrentSelectedMenuNavigation = new ChapterMenuNavigation(); }
    private void OpenPraisingTopic() { ActiveTopic = "Praising"; CurrentSelectedDigiWikiContent = new PraisingTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenNormalAttackTopic() { ActiveTopic = "NormalAttack"; CurrentSelectedDigiWikiContent = new NormalAttackTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenFinisherTopic() { ActiveTopic = "Finisher"; CurrentSelectedDigiWikiContent = new FinisherTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenFinisherFormulaTopic() { ActiveTopic = "FinisherFormula"; CurrentSelectedDigiWikiContent = new FinisherFormulaTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenSpecialtyFactorTopic() { ActiveTopic = "SpecialtyFactor"; CurrentSelectedDigiWikiContent = new SpecialtyFactorTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenScoldingTopic() { ActiveTopic = "Scolding"; CurrentSelectedDigiWikiContent = new ScoldingTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenFoodTopic() { ActiveTopic = "Food"; CurrentSelectedDigiWikiContent = new FoodTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenWeightTopic() { ActiveTopic = "Weight"; CurrentSelectedDigiWikiContent = new WeightTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenPoopTopic() { ActiveTopic = "Poop"; CurrentSelectedDigiWikiContent = new PoopTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenAgeTopic() { ActiveTopic = "Age"; CurrentSelectedDigiWikiContent = new AgeTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenTirednessTopic() { ActiveTopic = "Tiredness"; CurrentSelectedDigiWikiContent = new TirednessTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenSleepTopic() { ActiveTopic = "Sleep"; CurrentSelectedDigiWikiContent = new SleepTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenAreaPreferenceTopic() { ActiveTopic = "AreaPreference"; CurrentSelectedDigiWikiContent = new AreaPreferenceTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenHungryTopic() { ActiveTopic = "Hungry"; CurrentSelectedDigiWikiContent = new HungryTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenPoopyTopic() { ActiveTopic = "Poopy"; CurrentSelectedDigiWikiContent = new PoopyTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenTiredTopic() { ActiveTopic = "Tired"; CurrentSelectedDigiWikiContent = new TiredTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenSleepyTopic() { ActiveTopic = "Sleepy"; CurrentSelectedDigiWikiContent = new SleepyTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenFlowerTopic() { ActiveTopic = "Flower"; CurrentSelectedDigiWikiContent = new FlowerTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenInjuryTopic() { ActiveTopic = "Injured"; CurrentSelectedDigiWikiContent = new InjuredTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }
    private void OpenSickTopic() { ActiveTopic = "Sick"; CurrentSelectedDigiWikiContent = new SickTopicUserControl(SpeakShellmonTextShortDelayAction, SpeakShellmonTextNoDelayAction, InstantDisplay); }

    private void InstantDisplay()
    {
        _speakingSimulator.RequestInstantDisplay();
    }

    public void Dispose() => _speakingSimulator.Dispose();
}