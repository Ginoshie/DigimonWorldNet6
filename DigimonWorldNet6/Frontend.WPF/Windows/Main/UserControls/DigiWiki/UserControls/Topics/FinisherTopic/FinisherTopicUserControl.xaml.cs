namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FinisherTopic;

public partial class FinisherTopicUserControl
{
    public FinisherTopicUserControl(
        System.Action<string, System.Action<string>> speakShellmonTextShortDelayAction,
        System.Action<string, System.Action<string>> speakShellmonTextNoDelayAction,
        System.Action instantDisplay)
    {
        InitializeComponent();
        DataContext = new FinisherTopicViewModel(speakShellmonTextShortDelayAction, speakShellmonTextNoDelayAction, instantDisplay);
    }
}
