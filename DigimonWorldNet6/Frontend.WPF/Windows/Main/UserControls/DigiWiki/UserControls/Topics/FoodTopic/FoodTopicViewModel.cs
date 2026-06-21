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
            ShellmonDigiWikiNarratorText.FoodWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.FoodWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.FoodWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakMushroomOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FoodWiki.MUSHROOM_OVERWORLD, SpeakShellmonTextAction));
        SpeakMushroomFarmOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FoodWiki.MEAT_FARM_OVERWORLD, SpeakShellmonTextAction));
        OpenGuideFoodChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_FOOD_CHAPTER, UseShellExecute = true }));
        OpenGuideHungryChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_HUNGRY_CHAPTER, UseShellExecute = true }));
        OpenDataSheetCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.DIGIMON_RAISE_DATA_SHEET, UseShellExecute = true }));
    }

    public ICommand SpeakMushroomOverworldCommand { get; }
    public ICommand SpeakMushroomFarmOverworldCommand { get; }
    public ICommand OpenGuideFoodChapterCommand { get; }
    public ICommand OpenGuideHungryChapterCommand { get; }
    public ICommand OpenDataSheetCommand { get; }
}