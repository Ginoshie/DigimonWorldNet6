using System;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.FoodTopic;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.PoopTopic;

public partial class PoopTopicUserControl
{
    public PoopTopicUserControl(Action<string, Action<string>> speakShellmonTextShortDelayAction, Action<string, Action<string>> speakShellmonTextNoDelayAction, Action instantDisplay)
    {
        InitializeComponent();

        DataContext = new PoopTopicViewModel(speakShellmonTextShortDelayAction, speakShellmonTextNoDelayAction, instantDisplay);
    }
}