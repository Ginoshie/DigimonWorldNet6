using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.FlowerTopic;

public class FlowerTopicViewModel : TopicViewModelBase
{
    public FlowerTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.FlowerWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.FlowerWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.FlowerWiki.WikiText, SpeakShellmonTextAction);

        SpeakButterflyConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FlowerWiki.ButterflyOverworld, SpeakShellmonTextAction));
        SpeakFlowerConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FlowerWiki.FlowerScreen, SpeakShellmonTextAction));
        OpenGuideButterflyFlowerChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideButterflyFlowerChapter, UseShellExecute = true }));
    }

    public ICommand SpeakFlowerConditionScreenCommand { get; }
    public ICommand SpeakButterflyConditionOverworldCommand { get; }
    public ICommand OpenGuideButterflyFlowerChapterCommand { get; }
}