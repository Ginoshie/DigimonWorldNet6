using System;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.AgeTopic;

public partial class AgeTopicUserControl
{
    public AgeTopicUserControl(Action<string, Action<string>> speakShellmonTextShortDelayAction, Action<string, Action<string>> speakShellmonTextNoDelayAction, Action instantDisplay)
    {
        InitializeComponent();

        DataContext = new AgeTopicViewModel(speakShellmonTextShortDelayAction, speakShellmonTextNoDelayAction, instantDisplay);
    }
}