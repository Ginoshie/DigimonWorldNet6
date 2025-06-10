using System;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls;

public abstract class TopicViewModelBase : BaseViewModel
{
    private readonly Action _instantDisplay;
    
    private string _shellmonText = string.Empty;

    protected TopicViewModelBase(Action instantDisplay)
    {
        _instantDisplay = instantDisplay;

        InstantDisplayCommand = new CommandHandler(InstantDisplay);
    }

    public ICommand InstantDisplayCommand { get; }

    protected void SpeakShellmonTextAction(string speakShellmonText) => ShellmonText = speakShellmonText;
    
    protected void InstantDisplay() => _instantDisplay.Invoke();

    public string ShellmonText
    {
        get => _shellmonText;
        private set => SetField(ref _shellmonText, value);
    }
}