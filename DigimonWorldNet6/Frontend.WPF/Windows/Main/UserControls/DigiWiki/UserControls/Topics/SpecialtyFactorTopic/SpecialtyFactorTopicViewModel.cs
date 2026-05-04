using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SpecialtyFactorTopic;

public class SpecialtyFactorTopicViewModel : TopicViewModelBase
{
    public SpecialtyFactorTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.SpecialtyFactorWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.SpecialtyFactorWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.SpecialtyFactorWiki.WikiText, SpeakShellmonTextAction);

        SpeakSpecialtyChartImageCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.SpecialtyFactorWiki.SpecialtyChartImage, SpeakShellmonTextAction));
        OpenGuideSpecialtyFactorChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideSpecialtyFactorChapter, UseShellExecute = true }));
    }

    public ICommand SpeakSpecialtyChartImageCommand { get; }
    public ICommand OpenGuideSpecialtyFactorChapterCommand { get; }
}
