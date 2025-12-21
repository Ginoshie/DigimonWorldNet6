using System;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.PoopTopic;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.AreaPreferenceTopic;

public partial class AreaPreferenceTopicUserControl
{
    public AreaPreferenceTopicUserControl(Action<string, Action<string>> speakShellmonTextShortDelayAction, Action<string, Action<string>> speakShellmonTextNoDelayAction, Action instantDisplay)
    {
        InitializeComponent();

        DataContext = new AreaPreferenceTopicViewModel(speakShellmonTextShortDelayAction, speakShellmonTextNoDelayAction, instantDisplay);
    }
}