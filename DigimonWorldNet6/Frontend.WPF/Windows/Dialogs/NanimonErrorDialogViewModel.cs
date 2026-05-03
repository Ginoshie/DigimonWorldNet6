using System.Diagnostics;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Services;
using DigimonWorld.Frontend.WPF.ViewModelComponents;

namespace DigimonWorld.Frontend.WPF.Windows.Dialogs;

public class NanimonErrorDialogViewModel
{
    public NanimonErrorDialogViewModel(string narratorName, string message, string? linkUrl = null)
    {
        NarratorName = narratorName;
        Message = message;
        LinkUrl = linkUrl;
        HasLink = !string.IsNullOrEmpty(linkUrl);
        OpenLinkCommand = new CommandHandler(OpenLink);
        SoundService.PlaySfx("nani.mp3");
    }

    public string NarratorName { get; }
    public string Message { get; }
    public string? LinkUrl { get; }
    public bool HasLink { get; }
    public ICommand OpenLinkCommand { get; }

    private void OpenLink()
    {
        if (!string.IsNullOrEmpty(LinkUrl))
        {
            Process.Start(new ProcessStartInfo(LinkUrl) { UseShellExecute = true });
        }
    }
}

