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
            ShellmonDigiWikiNarratorText.SleepyWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.SleepyWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.SleepyWiki.WikiText, SpeakShellmonTextAction);

        OpenGuideSleepingChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideSleepingChapter, UseShellExecute = true }));
        OpenGuideSleepyChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideSleepyChapter, UseShellExecute = true }));
        
        SpeakSleepyScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SleepyWiki.SleepyScreen, SpeakShellmonTextAction));
        SpeakSleepyOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SleepyWiki.SleepyOverworld, SpeakShellmonTextAction));
    }

    public ICommand OpenGuideSleepingChapterCommand { get; }
    public ICommand OpenGuideSleepyChapterCommand { get; }
    public ICommand SpeakSleepyScreenCommand { get; }
    public ICommand SpeakSleepyOverworldCommand { get; }
}