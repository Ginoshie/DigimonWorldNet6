using System;
using DigimonWorld.Frontend.WPF.Constants;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.HomeTopic;

public class HomeTopicViewModel : TopicViewModelBase
{
    public HomeTopicViewModel(Action<string, Action<string>> speakShellmonTextAction, Action instantDisplay) : base(instantDisplay)
    {
        speakShellmonTextAction(ShellmonDigiWikiNarratorText.IntroText, SpeakShellmonTextAction);
    }
}