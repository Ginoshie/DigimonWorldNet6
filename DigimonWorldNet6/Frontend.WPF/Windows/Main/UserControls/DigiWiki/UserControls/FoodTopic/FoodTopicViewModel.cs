using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.FoodTopic;

public class FoodTopicViewModel : TopicViewModelBase
{
    public FoodTopicViewModel(Action<string, Action<string>> speakShellmonTextShortDelayAction, Action<string, Action<string>> speakShellmonTextNoDelayAction, Action instantDisplay) : base(instantDisplay)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.FoodWikiText, SpeakShellmonTextAction);

        SpeakFoodWikiCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FoodWikiText, SpeakShellmonTextAction));
        SpeakShellFactCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FoodWikiShellFacts, SpeakShellmonTextAction));
        SpeakHungerConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FoodWikiHungerConditionScreen, SpeakShellmonTextAction));
        SpeakHungerConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FoodWikiHungerConditionOverworld, SpeakShellmonTextAction));
        OpenGuideFoodChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuidFoodChapter, UseShellExecute = true }));
        OpenGuideHungryChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideHungryChapter, UseShellExecute = true }));
        OpenDataSheetCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.DataSheet, UseShellExecute = true }));
    }

    public ICommand SpeakFoodWikiCommand { get; }
    public ICommand SpeakShellFactCommand { get; }
    public ICommand SpeakHungerConditionScreenCommand { get; }
    public ICommand SpeakHungerConditionOverworldCommand { get; }
    public ICommand OpenGuideFoodChapterCommand { get; }
    public ICommand OpenGuideHungryChapterCommand { get; }
    public ICommand OpenDataSheetCommand { get; }
}