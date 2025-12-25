using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.SicknessTopic;

public class SicknessTopicViewModel : TopicViewModelBase
{
    public SicknessTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.SicknessWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.SicknessWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.SicknessWiki.WikiText, SpeakShellmonTextAction);

        SpeakSicknessConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SicknessWiki.SickOverworld, SpeakShellmonTextAction));
        SpeakSicknessConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SicknessWiki.SickScreen, SpeakShellmonTextAction));
        OpenGuideSickChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideSickChapter, UseShellExecute = true }));
    }

    public ICommand SpeakSicknessConditionScreenCommand { get; }
    public ICommand SpeakSicknessConditionOverworldCommand { get; }
    public ICommand OpenGuideSickChapterCommand { get; }
}