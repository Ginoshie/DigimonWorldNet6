using System.Windows;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Navigation.NavigationButtons;

public partial class CombatChapterNavigationButton
{
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(CombatChapterNavigationButton), new PropertyMetadata(null));

    public CombatChapterNavigationButton() => InitializeComponent();

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}
