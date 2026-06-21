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
            ShellmonDigiWikiNarratorText.TiredWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.TiredWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.TirednessWiki.WIKI_TEXT, SpeakShellmonTextAction);

        OpenGuideTirednessChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_TIREDNESS_CHAPTER, UseShellExecute = true }));
        OpenGuideTiredChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_TIRED_CHAPTER, UseShellExecute = true }));
        
        SpeakTiredScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.TiredWiki.TIRED_SCREEN, SpeakShellmonTextAction));
        SpeakTiredOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.TiredWiki.TIRED_OVERWORLD, SpeakShellmonTextAction));
    }

    public ICommand OpenGuideTirednessChapterCommand { get; }
    public ICommand OpenGuideTiredChapterCommand { get; }
    public ICommand SpeakTiredScreenCommand { get; }
    public ICommand SpeakTiredOverworldCommand { get; }
}