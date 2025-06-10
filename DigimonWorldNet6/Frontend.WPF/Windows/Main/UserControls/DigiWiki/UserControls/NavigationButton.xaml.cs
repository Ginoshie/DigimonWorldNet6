using System.Windows;
using System.Windows.Input;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls;

public partial class NavigationButton
{
    public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(nameof(IconPath), typeof(string), typeof(NavigationButton), new PropertyMetadata(null));

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(NavigationButton), new PropertyMetadata(null));

    public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(NavigationButton), new PropertyMetadata(""));

    public NavigationButton() => InitializeComponent();

    public string IconPath
    {
        get => (string)GetValue(IconPathProperty);
        set => SetValue(IconPathProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }
}