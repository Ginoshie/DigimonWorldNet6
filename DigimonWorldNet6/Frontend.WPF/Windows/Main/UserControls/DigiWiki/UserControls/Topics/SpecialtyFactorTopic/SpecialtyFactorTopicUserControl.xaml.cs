namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.SpecialtyFactorTopic;

public partial class SpecialtyFactorTopicUserControl
{
    public SpecialtyFactorTopicUserControl(
        System.Action<string, System.Action<string>> speakShellmonTextShortDelayAction,
        System.Action<string, System.Action<string>> speakShellmonTextNoDelayAction,
        System.Action instantDisplay)
    {
        InitializeComponent();
        DataContext = new SpecialtyFactorTopicViewModel(speakShellmonTextShortDelayAction, speakShellmonTextNoDelayAction, instantDisplay);
    }
}
