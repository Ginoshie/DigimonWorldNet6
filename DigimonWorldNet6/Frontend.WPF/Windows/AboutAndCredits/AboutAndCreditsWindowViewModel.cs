using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.About;

public class AboutAndCreditsWindowViewModel : BaseViewModel
{
    private readonly Window _window;

    public AboutAndCreditsWindowViewModel(Window window)
    {
        _window = window;
        CopyContactEmailCommand = new CommandHandler(CopyText);
        
        OpenYoutubeWebsiteCommand = new CommandHandler(OpenYoutubeWebsite);
        
        CloseWindowCommand = new CommandHandler(CloseWindow);
    }

    public string ContactEmailAddress { get; set; } = UiText.ContactEmailAddress;
    
    public string YoutubeAddress { get; set; } = UiText.YoutubeAddress;

    public ICommand CopyContactEmailCommand { get; }
    
    public ICommand OpenYoutubeWebsiteCommand { get; }
    
    public ICommand CloseWindowCommand { get; }

    private void CopyText()
    {
        Clipboard.SetText(ContactEmailAddress);
    }

    private void OpenYoutubeWebsite() =>
        Process.Start(new ProcessStartInfo
        {
            FileName = UiText.YoutubeAddress,
            UseShellExecute = true
        });

    private void CloseWindow()
    {
        _window.Close();
    }
}