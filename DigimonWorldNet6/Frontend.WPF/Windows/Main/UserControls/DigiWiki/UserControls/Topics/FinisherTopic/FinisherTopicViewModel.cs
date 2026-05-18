using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FinisherTopic;

public class FinisherTopicViewModel : TopicViewModelBase
{
    public FinisherTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.FinisherWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.FinisherWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.FinisherWiki.WikiText, SpeakShellmonTextAction);

        SpeakFinisherProgressImageCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FinisherWiki.FinisherProgressImage, SpeakShellmonTextAction));
        SpeakFinisherBarImageCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FinisherWiki.FinisherBarImage, SpeakShellmonTextAction));
        OpenGuideFinisherChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideFinisherChapter, UseShellExecute = true }));
    }

    public ICommand SpeakFinisherProgressImageCommand { get; }
    public ICommand SpeakFinisherBarImageCommand { get; }
    public ICommand OpenGuideFinisherChapterCommand { get; }
}
