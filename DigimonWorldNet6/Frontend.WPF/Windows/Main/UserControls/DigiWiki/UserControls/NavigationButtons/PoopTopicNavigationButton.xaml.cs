using System.Windows;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.NavigationButtons;

public partial class PoopTopicNavigationButton
{

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(PoopTopicNavigationButton), new PropertyMetadata(null));

    public PoopTopicNavigationButton() => InitializeComponent();

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}