using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.AgeTopic;

public class AgeTopicViewModel : TopicViewModelBase
{
    public AgeTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.AgeWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.AgeWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.AgeWiki.WikiText, SpeakShellmonTextAction);

        SpeakAgeScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.AgeWiki.AgeScreen, SpeakShellmonTextAction));
        SpeakHappinessThresholdsCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.AgeWiki.HappinessThresholds, SpeakShellmonTextAction));
        OpenGuideLifespanChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideLifespanChapter, UseShellExecute = true }));
        OpenYoutubeLifespanPartOneClipCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.YoutubeLifespanPartOneClip, UseShellExecute = true }));
        OpenYoutubeLifespanPartTwoClipCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.YoutubeLifespanPartTwoClip, UseShellExecute = true }));
    }

    public ICommand SpeakAgeScreenCommand { get; }
    public ICommand SpeakHappinessThresholdsCommand { get; }
    public ICommand OpenGuideLifespanChapterCommand { get; }
    public ICommand OpenYoutubeLifespanPartOneClipCommand { get; }
    public ICommand OpenYoutubeLifespanPartTwoClipCommand { get; }
}