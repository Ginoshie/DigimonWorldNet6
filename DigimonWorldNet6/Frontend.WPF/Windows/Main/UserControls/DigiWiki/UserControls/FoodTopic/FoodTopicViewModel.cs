using System;
using DigimonWorld.Frontend.WPF.Constants;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.FoodTopic;

public class FoodTopicViewModel : TopicViewModelBase
{
    public FoodTopicViewModel(Action<string, Action<string>> speakShellmonTextAction, Action instantDisplay) : base(instantDisplay)
    {
        speakShellmonTextAction(ShellmonDigiWikiNarratorText.IntroText, SpeakShellmonTextAction);
    }
}