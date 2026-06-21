using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FinisherFormulaTopic;

public class FinisherFormulaTopicViewModel : TopicViewModelBase
{
    public FinisherFormulaTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.FinisherFormulaWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.FinisherFormulaWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.FinisherFormulaWiki.WIKI_TEXT, SpeakShellmonTextAction);

        SpeakFormulaImageCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.FinisherFormulaWiki.FORMULA_IMAGE, SpeakShellmonTextAction));
        OpenGuideFinisherFormulaChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_FINISHER_FORMULA_CHAPTER, UseShellExecute = true }));
    }

    public ICommand SpeakFormulaImageCommand { get; }
    public ICommand OpenGuideFinisherFormulaChapterCommand { get; }
}
