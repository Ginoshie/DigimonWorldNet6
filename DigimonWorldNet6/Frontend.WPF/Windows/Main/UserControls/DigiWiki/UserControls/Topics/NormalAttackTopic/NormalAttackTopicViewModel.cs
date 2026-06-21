using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.NormalAttackTopic;

public class NormalAttackTopicViewModel : TopicViewModelBase
{
    public NormalAttackTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.NormalAttackWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.NormalAttackWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.NormalAttackWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakFormulaImageCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.NormalAttackWiki.FORMULA_IMAGE, SpeakShellmonTextAction));
        OpenGuideNormalAttackChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_NORMAL_ATTACK_CHAPTER, UseShellExecute = true }));
    }

    public ICommand SpeakFormulaImageCommand { get; }
    public ICommand OpenGuideNormalAttackChapterCommand { get; }
}
