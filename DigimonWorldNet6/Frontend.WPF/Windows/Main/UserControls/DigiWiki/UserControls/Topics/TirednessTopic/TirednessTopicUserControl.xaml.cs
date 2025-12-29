using System;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.TirednessTopic;

public partial class TirednessTopicUserControl
{
    public TirednessTopicUserControl(Action<string, Action<string>> speakShellmonTextShortDelayAction, Action<string, Action<string>> speakShellmonTextNoDelayAction, Action instantDisplay)
    {
        InitializeComponent();

        DataContext = new TirednessTopicViewModel(speakShellmonTextShortDelayAction, speakShellmonTextNoDelayAction, instantDisplay);
    }
}