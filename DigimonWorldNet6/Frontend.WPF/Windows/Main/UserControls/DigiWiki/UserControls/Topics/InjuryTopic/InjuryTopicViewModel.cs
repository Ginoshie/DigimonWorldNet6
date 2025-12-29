using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.InjuryTopic;

public class InjuryTopicViewModel : TopicViewModelBase
{
    public InjuryTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.InjuredWiki.ShellFacts,
            ShellmonDigiWikiNarratorText.InjuredWiki.WikiText)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.InjuredWiki.WikiText, SpeakShellmonTextAction);

        SpeakInjuredConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.InjuredWiki.InjuredOverworld, SpeakShellmonTextAction));
        SpeakInjuredConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.InjuredWiki.InjuredScreen, SpeakShellmonTextAction));
        OpenGuideInjuredChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GuideInjuredChapter, UseShellExecute = true }));
    }

    public ICommand SpeakInjuredConditionScreenCommand { get; }
    public ICommand SpeakInjuredConditionOverworldCommand { get; }
    public ICommand OpenGuideInjuredChapterCommand { get; }
}