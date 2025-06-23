using System;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Home;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.HomeTopic;

public partial class Home
{
    public Home(Action<string, Action<string>> speakShellmonTextAction, Action instantDisplay)
    {
        InitializeComponent();

        DataContext = new HomeViewModel(speakShellmonTextAction, instantDisplay);
    }
}