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
            ShellmonDigiWikiNarratorText.PoopWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.PoopWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.PoopWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakToiletFileCityCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.PoopWiki.TOILET_FILE_CITY, SpeakShellmonTextAction));
        SpeakPortpottyShopCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.PoopWiki.PORTPOTTY_SHOP, SpeakShellmonTextAction));
        OpenGuidePoopingChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_POOPING_CHAPTER, UseShellExecute = true }));
        OpenYoutubeDiscAndPoopingClipCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.YOUTUBE_DISC_AND_POOPING_CLIP, UseShellExecute = true }));
    }

    public ICommand SpeakToiletFileCityCommand { get; }
    public ICommand SpeakPortpottyShopCommand { get; }
    public ICommand OpenGuidePoopingChapterCommand { get; }
    public ICommand OpenYoutubeDiscAndPoopingClipCommand { get; }
}