using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SickTopic;

public class SickTopicViewModel : TopicViewModelBase
{
    public SickTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.SickWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.SickWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.SickWiki.WikiText, SpeakShellmonTextAction);

        SpeakSicknessConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SickWiki.SickOverworld, SpeakShellmonTextAction));
        SpeakSicknessConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SickWiki.SickScreen, SpeakShellmonTextAction));
        OpenGuideSickChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideSickChapter, UseShellExecute = true }));
    }

    public ICommand SpeakSicknessConditionScreenCommand { get; }
    public ICommand SpeakSicknessConditionOverworldCommand { get; }
    public ICommand OpenGuideSickChapterCommand { get; }
}