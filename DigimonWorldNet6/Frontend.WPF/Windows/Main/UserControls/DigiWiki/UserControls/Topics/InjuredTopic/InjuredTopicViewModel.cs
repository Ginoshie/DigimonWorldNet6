using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.InjuredTopic;

public class InjuredTopicViewModel : TopicViewModelBase
{
    public InjuredTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.InjuredWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.InjuredWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.InjuredWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakInjuredConditionOverworldCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.InjuredWiki.INJURED_OVERWORLD, SpeakShellmonTextAction));
        SpeakInjuredConditionScreenCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.InjuredWiki.INJURED_SCREEN, SpeakShellmonTextAction));
        OpenGuideInjuredChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_INJURED_CHAPTER, UseShellExecute = true }));
    }

    public ICommand SpeakInjuredConditionScreenCommand { get; }
    public ICommand SpeakInjuredConditionOverworldCommand { get; }
    public ICommand OpenGuideInjuredChapterCommand { get; }
}