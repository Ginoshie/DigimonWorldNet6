namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Topics.FinisherFormulaTopic;

public partial class FinisherFormulaTopicUserControl
{
    public FinisherFormulaTopicUserControl(
        System.Action<string, System.Action<string>> speakShellmonTextShortDelayAction,
        System.Action<string, System.Action<string>> speakShellmonTextNoDelayAction,
        System.Action instantDisplay)
    {
        InitializeComponent();
        DataContext = new FinisherFormulaTopicViewModel(speakShellmonTextShortDelayAction, speakShellmonTextNoDelayAction, instantDisplay);
    }
}
