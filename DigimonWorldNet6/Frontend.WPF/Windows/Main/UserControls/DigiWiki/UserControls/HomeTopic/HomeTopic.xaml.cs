using System;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.HomeTopic;

public partial class HomeTopic
{
    public HomeTopic(Action<string, Action<string>> speakShellmonTextAction, Action instantDisplay)
    {
        InitializeComponent();

        DataContext = new HomeTopicViewModel(speakShellmonTextAction, instantDisplay);
    }
}