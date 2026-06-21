using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.ViewModelComponents;
using Shared;

namespace DigimonWorld.Frontend.WPF.Windows.AboutAndCredits;

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

    public string ContactEmailAddress { get; set; } = UiText.CONTACT_EMAIL_ADDRESS;

    public string YoutubeAddress { get; set; } = Url.YOUTUBE_ADDRESS;

    public string VersionText { get; } = AppVersion.Current;

    public ICommand CopyContactEmailCommand { get; }

    public ICommand OpenYoutubeWebsiteCommand { get; }

    public ICommand CloseWindowCommand { get; }

    private void CopyText() => Clipboard.SetText(ContactEmailAddress);

    private void OpenYoutubeWebsite() =>
        Process.Start(new ProcessStartInfo
        {
            FileName = Url.YOUTUBE_ADDRESS,
            UseShellExecute = true
        });

    private void CloseWindow() => _window.Close();
}