namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.NormalAttackTopic;

public partial class NormalAttackTopicUserControl
{
    public NormalAttackTopicUserControl(
        System.Action<string, System.Action<string>> speakShellmonTextShortDelayAction,
        System.Action<string, System.Action<string>> speakShellmonTextNoDelayAction,
        System.Action instantDisplay)
    {
        InitializeComponent();
        DataContext = new NormalAttackTopicViewModel(speakShellmonTextShortDelayAction, speakShellmonTextNoDelayAction, instantDisplay);
    }
}
