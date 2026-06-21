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
            ShellmonDigiWikiNarratorText.AgeWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.AgeWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.AgeWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakAgeScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.AgeWiki.AGE_SCREEN, SpeakShellmonTextAction));
        SpeakHappinessThresholdsCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.AgeWiki.HAPPINESS_THRESHOLDS, SpeakShellmonTextAction));
        OpenGuideLifespanChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_LIFESPAN_CHAPTER, UseShellExecute = true }));
        OpenYoutubeLifespanPartOneClipCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.YOUTUBE_LIFESPAN_PART_ONE_CLIP, UseShellExecute = true }));
        OpenYoutubeLifespanPartTwoClipCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.YOUTUBE_LIFESPAN_PART_TWO_CLIP, UseShellExecute = true }));
    }

    public ICommand SpeakAgeScreenCommand { get; }
    public ICommand SpeakHappinessThresholdsCommand { get; }
    public ICommand OpenGuideLifespanChapterCommand { get; }
    public ICommand OpenYoutubeLifespanPartOneClipCommand { get; }
    public ICommand OpenYoutubeLifespanPartTwoClipCommand { get; }
}