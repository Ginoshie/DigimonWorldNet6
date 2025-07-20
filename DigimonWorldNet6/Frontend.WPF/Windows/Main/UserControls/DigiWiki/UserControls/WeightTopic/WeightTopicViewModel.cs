using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.WeightTopic;

public class WeightTopicViewModel : TopicViewModelBase
{
    public WeightTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.WeightWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.WeightWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.WeightWiki.WikiText, SpeakShellmonTextAction);

        OpenGuideFoodChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideWeightChapter, UseShellExecute = true }));
        OpenDataSheetCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.DataSheet, UseShellExecute = true }));
    }

    public ICommand OpenGuideFoodChapterCommand { get; }
    public ICommand OpenDataSheetCommand { get; }
}