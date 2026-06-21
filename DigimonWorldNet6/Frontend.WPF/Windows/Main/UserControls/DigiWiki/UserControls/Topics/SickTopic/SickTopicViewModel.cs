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
            ShellmonDigiWikiNarratorText.SickWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.SickWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.SickWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakSicknessConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SickWiki.SICK_OVERWORLD, SpeakShellmonTextAction));
        SpeakSicknessConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SickWiki.SICK_SCREEN, SpeakShellmonTextAction));
        OpenGuideSickChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_SICK_CHAPTER, UseShellExecute = true }));
    }

    public ICommand SpeakSicknessConditionScreenCommand { get; }
    public ICommand SpeakSicknessConditionOverworldCommand { get; }
    public ICommand OpenGuideSickChapterCommand { get; }
}