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
            ShellmonDigiWikiNarratorText.WeightWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.WeightWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.WIKI_TEXT, SpeakShellmonTextAction);

        OpenGuideFoodChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_WEIGHT_CHAPTER, UseShellExecute = true }));
        OpenDigimonRaiseDataSheetCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.DIGIMON_RAISE_DATA_SHEET, UseShellExecute = true }));
        OpenFoodDataSheetCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.FOOD_DATA_SHEET, UseShellExecute = true }));
        
        SpeakWeightProfileScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.WEIGHT_SCREEN, SpeakShellmonTextAction));
        SpeakBigBerryCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.BIG_BERRY, SpeakShellmonTextAction));
        SpeakPricklyPearCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.PRICKLY_PEAR, SpeakShellmonTextAction));
        SpeakSteakCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.STEAK, SpeakShellmonTextAction));
        SpeakChainMelonCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.CHAIN_MELON, SpeakShellmonTextAction));
        SpeakMoldyMeatCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.MOLDY_MEAT, SpeakShellmonTextAction));
        SpeakDigicatfishCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.DIGICATFISH, SpeakShellmonTextAction));
        SpeakDigiseabassCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.DIGISEABASS, SpeakShellmonTextAction));
        SpeakBlackTroutCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.BLACK_TROUT, SpeakShellmonTextAction));
        SpeakGoldAcornCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.GOLD_ACORN, SpeakShellmonTextAction));
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