using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.WeightTopic;

public class WeightTopicViewModel : TopicViewModelBase
{
    public WeightTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.WeightWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.WeightWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.WikiText, SpeakShellmonTextAction);

        OpenGuideFoodChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideWeightChapter, UseShellExecute = true }));
        OpenDigimonRaiseDataSheetCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.DigimonRaiseDataSheet, UseShellExecute = true }));
        OpenFoodDataSheetCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.FoodDataSheet, UseShellExecute = true }));
        
        SpeakWeightProfileScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.WeightScreen, SpeakShellmonTextAction));
        SpeakBigBerryCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.BigBerry, SpeakShellmonTextAction));
        SpeakPricklyPearCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.PricklyPear, SpeakShellmonTextAction));
        SpeakSteakCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.Steak, SpeakShellmonTextAction));
        SpeakChainMelonCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.ChainMelon, SpeakShellmonTextAction));
        SpeakMoldyMeatCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.MoldyMeat, SpeakShellmonTextAction));
        SpeakDigicatfishCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.Digicatfish, SpeakShellmonTextAction));
        SpeakDigiseabassCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.Digiseabass, SpeakShellmonTextAction));
        SpeakBlackTroutCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.BlackTrout, SpeakShellmonTextAction));
        SpeakGoldAcornCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.GoldAcorn, SpeakShellmonTextAction));
    }

    public ICommand OpenGuideFoodChapterCommand { get; }
    public ICommand OpenDigimonRaiseDataSheetCommand { get; }
    public ICommand OpenFoodDataSheetCommand { get; }
    public ICommand SpeakWeightProfileScreenCommand { get; }
    public ICommand SpeakBigBerryCommand { get; }
    public ICommand SpeakPricklyPearCommand { get; }
    public ICommand SpeakSteakCommand { get; }
    public ICommand SpeakChainMelonCommand { get; }
    public ICommand SpeakMoldyMeatCommand { get; }
    public ICommand SpeakDigicatfishCommand { get; }
    public ICommand SpeakDigiseabassCommand { get; }
    public ICommand SpeakBlackTroutCommand { get; }
    public ICommand SpeakGoldAcornCommand { get; }
}