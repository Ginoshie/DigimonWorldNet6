using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.HungryTopic;

public class HungryTopicViewModel : TopicViewModelBase
{
    public HungryTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.HungryWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.HungryWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.HungryWiki.WikiText, SpeakShellmonTextAction);

        SpeakHungerConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.HungryWiki.HungerScreen, SpeakShellmonTextAction));
        SpeakHungerConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.HungryWiki.HungerOverworld, SpeakShellmonTextAction));
        OpenGuideFoodChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideFoodChapter, UseShellExecute = true }));
        OpenGuideHungryChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideHungryChapter, UseShellExecute = true }));
    }

    public ICommand SpeakHungerConditionScreenCommand { get; }
    public ICommand SpeakHungerConditionOverworldCommand { get; }
    public ICommand OpenGuideFoodChapterCommand { get; }
    public ICommand OpenGuideHungryChapterCommand { get; }
}