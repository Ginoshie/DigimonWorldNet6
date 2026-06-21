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
            ShellmonDigiWikiNarratorText.PoopyWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.PoopyWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.PoopyWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakPoopingConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.PoopyWiki.POOPING_OVERWORLD, SpeakShellmonTextAction));
        SpeakPoopingConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.PoopyWiki.POOPING_SCREEN, SpeakShellmonTextAction));
        OpenGuidePoopingChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_POOPING_CHAPTER, UseShellExecute = true }));
        OpenGuidePoopyChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_POOPY_CHAPTER, UseShellExecute = true }));
        OpenYoutubeDiscAndPoopingClipCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.YOUTUBE_DISC_AND_POOPING_CLIP, UseShellExecute = true }));
    }

    public ICommand SpeakPoopingConditionScreenCommand { get; }
    public ICommand SpeakPoopingConditionOverworldCommand { get; }
    public ICommand OpenGuidePoopingChapterCommand { get; }
    public ICommand OpenGuidePoopyChapterCommand { get; }
    public ICommand OpenYoutubeDiscAndPoopingClipCommand { get; }
}