using System;
using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.TirednessTopic;

public class TirednessTopicViewModel : TopicViewModelBase
{
    public TirednessTopicViewModel(
        Action<string, Action<string>> speakShellmonTextShortDelayAction,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        Action instantDisplay) :
        base(
            instantDisplay,
            speakShellmonTextNoDelayAction,
            ShellmonDigiWikiNarratorText.TirednessWiki.SHELL_FACTS,
            ShellmonDigiWikiNarratorText.TirednessWiki.WIKI_TEXT)
    {
        speakShellmonTextShortDelayAction(ShellmonDigiWikiNarratorText.TirednessWiki.WIKI_TEXT, SpeakShellmonTextAction);

        OpenGuideTirednessChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_TIREDNESS_CHAPTER, UseShellExecute = true }));
        OpenGuideTiredChapterCommand = new CommandHandler(() => Process.Start(new ProcessStartInfo { FileName = Url.GUIDE_TIRED_CHAPTER, UseShellExecute = true }));
        
        SpeakDrimogemonCarryDirtQuestCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.TirednessWiki.DRIMOGEMON_CARRY_DIRT_QUEST, SpeakShellmonTextAction));
        SpeakTrainingGymCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(ShellmonDigiWikiNarratorText.TirednessWiki.TRAINING_GYM, SpeakShellmonTextAction));
    }

    public ICommand OpenGuideTirednessChapterCommand { get; }
    public ICommand OpenGuideTiredChapterCommand { get; }
    public ICommand SpeakDrimogemonCarryDirtQuestCommand { get; }
    public ICommand SpeakTrainingGymCommand { get; }
}