using System.Windows;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Navigation.NavigationButtons;

public partial class PoopingTopicNavigationButton
{

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(PoopingTopicNavigationButton), new PropertyMetadata(null));

    public PoopingTopicNavigationButton() => InitializeComponent();

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}