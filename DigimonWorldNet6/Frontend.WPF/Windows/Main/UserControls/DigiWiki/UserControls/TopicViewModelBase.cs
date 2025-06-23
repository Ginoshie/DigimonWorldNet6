using System;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki.UserControls;

public abstract class TopicViewModelBase : BaseViewModel
{
    private readonly Action _instantDisplay;

    private string _shellmonText = string.Empty;

    protected TopicViewModelBase(
        Action instantDisplay,
        Action<string, Action<string>> speakShellmonTextNoDelayAction,
        string shellFactText,
        string wikiText)
    {
        _instantDisplay = instantDisplay;

        SpeakShellFactCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(shellFactText, SpeakShellmonTextAction));
        SpeakWikiTextCommand = new CommandHandler(() => speakShellmonTextNoDelayAction(wikiText, SpeakShellmonTextAction));

        InstantDisplayCommand = new CommandHandler(InstantDisplay);
    }

    public ICommand InstantDisplayCommand { get; }

    public ICommand SpeakShellFactCommand { get; }
    public ICommand SpeakWikiTextCommand { get; }

    protected void SpeakShellmonTextAction(string speakShellmonText) => ShellmonText = speakShellmonText;

    protected void InstantDisplay() => _instantDisplay.Invoke();

    public string ShellmonText
    {
        get => _shellmonText;
        private set => SetField(ref _shellmonText, value);
    }
}