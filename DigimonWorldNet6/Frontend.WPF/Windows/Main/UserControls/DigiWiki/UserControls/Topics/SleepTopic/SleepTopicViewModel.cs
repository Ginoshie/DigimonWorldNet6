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
            ShellmonDigiWikiNarratorText.SleepWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.SleepWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.SleepWiki.WIKI_TEXT, SpeakShellmonTextAction);

        OpenGuideSleepingChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_SLEEPING_CHAPTER, UseShellExecute = true }));
        
        SpeakProfileSectionSleepingScheduleCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SleepWiki.SLEEPING_SCHEDULE, SpeakShellmonTextAction));
    }

    public ICommand OpenGuideSleepingChapterCommand { get; }
    public ICommand SpeakProfileSectionSleepingScheduleCommand { get; }
}