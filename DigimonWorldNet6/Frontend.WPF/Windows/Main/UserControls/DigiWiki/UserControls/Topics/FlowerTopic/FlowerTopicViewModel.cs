using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FlowerTopic;

public class FlowerTopicViewModel : TopicViewModelBase
{
    public FlowerTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.FlowerWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.FlowerWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.FlowerWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakButterflyConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FlowerWiki.BUTTERFLY_OVERWORLD, SpeakShellmonTextAction));
        SpeakFlowerConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FlowerWiki.FLOWER_SCREEN, SpeakShellmonTextAction));
        OpenGuideButterflyFlowerChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_BUTTERFLY_FLOWER_CHAPTER, UseShellExecute = true }));
    }

    public ICommand SpeakFlowerConditionScreenCommand { get; }
    public ICommand SpeakButterflyConditionOverworldCommand { get; }
    public ICommand OpenGuideButterflyFlowerChapterCommand { get; }
}