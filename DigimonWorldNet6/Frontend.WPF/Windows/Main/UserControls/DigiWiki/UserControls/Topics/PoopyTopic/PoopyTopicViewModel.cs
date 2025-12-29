using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.PoopyTopic;

public class PoopyTopicViewModel : TopicViewModelBase
{
    public PoopyTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.PoopyWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.PoopyWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.PoopyWiki.WikiText, SpeakShellmonTextAction);

        SpeakPoopingConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.PoopyWiki.PoopingOverworld, SpeakShellmonTextAction));
        SpeakPoopingConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.PoopyWiki.PoopingScreen, SpeakShellmonTextAction));
        OpenGuidePoopingChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuidePoopingChapter, UseShellExecute = true }));
        OpenGuidePoopyChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuidePoopyChapter, UseShellExecute = true }));
        OpenYoutubeDiscAndPoopingClipCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.YoutubeDiscAndPoopingClip, UseShellExecute = true }));
    }

    public ICommand SpeakPoopingConditionScreenCommand { get; }
    public ICommand SpeakPoopingConditionOverworldCommand { get; }
    public ICommand OpenGuidePoopingChapterCommand { get; }
    public ICommand OpenGuidePoopyChapterCommand { get; }
    public ICommand OpenYoutubeDiscAndPoopingClipCommand { get; }
}