using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FoodTopic;

public class FoodTopicViewModel : TopicViewModelBase
{
    public FoodTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.FoodWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.FoodWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.FoodWiki.WikiText, SpeakShellmonTextAction);

        SpeakMushroomOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FoodWiki.MushroomOverworld, SpeakShellmonTextAction));
        SpeakMushroomFarmOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FoodWiki.MeatFarmOverworld, SpeakShellmonTextAction));
        OpenGuideFoodChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideFoodChapter, UseShellExecute = true }));
        OpenGuideHungryChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideHungryChapter, UseShellExecute = true }));
        OpenDataSheetCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.DigimonRaiseDataSheet, UseShellExecute = true }));
    }

    public ICommand SpeakMushroomOverworldCommand { get; }
    public ICommand SpeakMushroomFarmOverworldCommand { get; }
    public ICommand OpenGuideFoodChapterCommand { get; }
    public ICommand OpenGuideHungryChapterCommand { get; }
    public ICommand OpenDataSheetCommand { get; }
}