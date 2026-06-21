using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SleepyTopic;

public class SleepyTopicViewModel : TopicViewModelBase
{
    public SleepyTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.SleepyWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.SleepyWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.SleepyWiki.WIKI_TEXT, SpeakShellmonTextAction);

        OpenGuideSleepingChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_SLEEPING_CHAPTER, UseShellExecute = true }));
        OpenGuideSleepyChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_SLEEPY_CHAPTER, UseShellExecute = true }));
        
        SpeakSleepyScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SleepyWiki.SLEEPY_SCREEN, SpeakShellmonTextAction));
        SpeakSleepyOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SleepyWiki.SLEEPY_OVERWORLD, SpeakShellmonTextAction));
    }

    public ICommand OpenGuideSleepingChapterCommand { get; }
    public ICommand OpenGuideSleepyChapterCommand { get; }
    public ICommand SpeakSleepyScreenCommand { get; }
    public ICommand SpeakSleepyOverworldCommand { get; }
}