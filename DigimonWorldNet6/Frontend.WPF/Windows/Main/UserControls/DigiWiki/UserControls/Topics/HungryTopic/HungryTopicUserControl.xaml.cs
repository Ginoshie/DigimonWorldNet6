using System;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.HungryTopic;

public partial class HungryTopicUserControl
{
    public HungryTopicUserControl(Action<string, Action<string>> speakShellmonTextShortDelayAction, Action<string, Action<string>> speakShellmonTextNoDelayAction, Action instantDisplay)
    {
        InitializeComponent();

        DataContext = new HungryTopicViewModel(speakShellmonTextShortDelayAction, speakShellmonTextNoDelayAction, instantDisplay);
    }
}