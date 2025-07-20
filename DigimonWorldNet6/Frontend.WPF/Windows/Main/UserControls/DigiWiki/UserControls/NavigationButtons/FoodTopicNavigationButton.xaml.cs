using System.Windows;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.NavigationButtons;

public partial class FoodTopicNavigationButton
{

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(FoodTopicNavigationButton), new PropertyMetadata(null));

    public FoodTopicNavigationButton() => InitializeComponent();

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}