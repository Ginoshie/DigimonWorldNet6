using System;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.PoopyTopic;

public partial class PoopyTopicUserControl
{
    public PoopyTopicUserControl(Action<string, Action<string>> speakShellmonTextShortDelayAction, Action<string, Action<string>> speakShellmonTextNoDelayAction, Action instantDisplay)
    {
        InitializeComponent();

        DataContext = new PoopyTopicViewModel(speakShellmonTextShortDelayAction, speakShellmonTextNoDelayAction, instantDisplay);
    }
}