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
            ShellmonDigiWikiNarratorText.HungryWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.HungryWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.HungryWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakHungerConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.HungryWiki.HUNGER_SCREEN, SpeakShellmonTextAction));
        SpeakHungerConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.HungryWiki.HUNGER_OVERWORLD, SpeakShellmonTextAction));
        OpenGuideFoodChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_FOOD_CHAPTER, UseShellExecute = true }));
        OpenGuideHungryChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_HUNGRY_CHAPTER, UseShellExecute = true }));
    }

    public ICommand SpeakHungerConditionScreenCommand { get; }
    public ICommand SpeakHungerConditionOverworldCommand { get; }
    public ICommand OpenGuideFoodChapterCommand { get; }
    public ICommand OpenGuideHungryChapterCommand { get; }
}