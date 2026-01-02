using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.ScoldingTopic;

public class ScoldingTopicViewModel : TopicViewModelBase
{
    public ScoldingTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.ScoldingWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.ScoldingWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.ScoldingWiki.WikiText, SpeakShellmonTextAction);

        SpeakScoldingMenuActionCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.ScoldingWiki.ScoldingMenuAction, SpeakShellmonTextAction));
        OpenGuideScoldingChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideScoldingChapter, UseShellExecute = true }));
    }

    public ICommand SpeakScoldingMenuActionCommand { get; }
    public ICommand OpenGuideScoldingChapterCommand { get; }
}