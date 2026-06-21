using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.PraisingTopic;

public class PraisingTopicViewModel : TopicViewModelBase
{
    public PraisingTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.PraisingWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.PraisingWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.PraisingWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakPraiseMenuActionCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.PraisingWiki.PRAISING_MENU_ACTION, SpeakShellmonTextAction));
        OpenGuidePraisingChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_PRAISING_CHAPTER, UseShellExecute = true }));
    }

    public ICommand SpeakPraiseMenuActionCommand { get; }
    public ICommand OpenGuidePraisingChapterCommand { get; }
}