using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.PoopTopic;

public class PoopTopicViewModel : TopicViewModelBase
{
    public PoopTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.PoopWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.PoopWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.PoopWiki.WikiText, SpeakShellmonTextAction);

        SpeakPoopingConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.PoopWiki.PoopingOverworld, SpeakShellmonTextAction));
        SpeakPoopingConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.PoopWiki.PoopingScreen, SpeakShellmonTextAction));
        OpenGuidePoopingChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuidePoopingChapter, UseShellExecute = true }));
        OpenYoutubeDiscAndPoopingClipCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.YoutubeDiscAndPoopingClip, UseShellExecute = true }));
    }

    public ICommand SpeakPoopingConditionScreenCommand { get; }
    public ICommand SpeakPoopingConditionOverworldCommand { get; }
    public ICommand OpenGuidePoopingChapterCommand { get; }
    public ICommand OpenYoutubeDiscAndPoopingClipCommand { get; }
}