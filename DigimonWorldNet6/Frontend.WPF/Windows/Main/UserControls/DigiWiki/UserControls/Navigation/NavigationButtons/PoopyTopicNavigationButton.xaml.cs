using System.Windows;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Navigation.NavigationButtons;

public partial class PoopyTopicNavigationButton
{

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(PoopyTopicNavigationButton), new PropertyMetadata(null));

    public PoopyTopicNavigationButton() => InitializeComponent();

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}