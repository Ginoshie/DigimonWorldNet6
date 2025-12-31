using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.TiredTopic;

public class TiredTopicViewModel : TopicViewModelBase
{
    public TiredTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.TiredWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.TiredWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.TirednessWiki.WikiText, SpeakShellmonTextAction);

        OpenGuideTirednessChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideTirednessChapter, UseShellExecute = true }));
        OpenGuideTiredChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideTiredChapter, UseShellExecute = true }));
        
        SpeakTiredScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.TiredWiki.TiredScreen, SpeakShellmonTextAction));
        SpeakTiredOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.TiredWiki.TiredOverworld, SpeakShellmonTextAction));
    }

    public ICommand OpenGuideTirednessChapterCommand { get; }
    public ICommand OpenGuideTiredChapterCommand { get; }
    public ICommand SpeakTiredScreenCommand { get; }
    public ICommand SpeakTiredOverworldCommand { get; }
}