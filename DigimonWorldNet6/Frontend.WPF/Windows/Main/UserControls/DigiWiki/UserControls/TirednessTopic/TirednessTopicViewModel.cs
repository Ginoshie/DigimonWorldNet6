using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.TirednessTopic;

public class TirednessTopicViewModel : TopicViewModelBase
{
    public TirednessTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.TirednessWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.TirednessWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.TirednessWiki.WikiText, SpeakShellmonTextAction);

        OpenGuideTirednessChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideTirednessChapter, UseShellExecute = true }));
        
        SpeakTirednessScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.TirednessWiki.TirednessScreen, SpeakShellmonTextAction));
        SpeakTirednessOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.TirednessWiki.TirednessOverworld, SpeakShellmonTextAction));
    }

    public ICommand OpenGuideTirednessChapterCommand { get; }
    public ICommand SpeakTirednessScreenCommand { get; }
    public ICommand SpeakTirednessOverworldCommand { get; }
}