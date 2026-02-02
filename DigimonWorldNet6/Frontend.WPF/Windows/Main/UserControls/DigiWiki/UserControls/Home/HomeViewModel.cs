using System;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls.Home;

public class HomeViewModel : BaseViewModel
{
    private readonly Action _instantDisplay;

    public HomeViewModel(Action<string, Action<string>> speakShellmonTextAction, Action instantDisplay)
    {
        _instantDisplay = instantDisplay;
        
        speakShellmonTextAction(ShellmonDigiWikiNarratorText.HomeScreen.IntroText, SpeakShellmonTextAction);
        
        InstantDisplayCommand = new CommandHandler(InstantDisplay);
    }
    
    public ICommand InstantDisplayCommand { get; }

    protected void SpeakShellmonTextAction(string speakShellmonText) => ShellmonText = speakShellmonText;

    protected void InstantDisplay() => _instantDisplay.Invoke();

    public string ShellmonText
    {
        get;
        private set => SetField(ref field, value);
    } = string.Empty;
}