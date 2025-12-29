using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SleepTopic;

public class SleepTopicViewModel : TopicViewModelBase
{
    public SleepTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.SleepWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.SleepWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.SleepWiki.WikiText, SpeakShellmonTextAction);

        OpenGuideSleepingChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideSleepingChapter, UseShellExecute = true }));
        
        SpeakSleepyScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SleepWiki.SleepScreen, SpeakShellmonTextAction));
        SpeakSleepyOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SleepWiki.SleepOverworld, SpeakShellmonTextAction));
    }

    public ICommand OpenGuideSleepingChapterCommand { get; }
    public ICommand SpeakSleepyScreenCommand { get; }
    public ICommand SpeakSleepyOverworldCommand { get; }
}