using System;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.DigiWiki;

public partial class DigiWikiUserControl : IDisposable
{
    private readonly DigiWikiViewModel _viewModel;

    public DigiWikiUserControl()
    {
        InitializeComponent();

        _viewModel = new DigiWikiViewModel();

        DataContext = _viewModel;
    }

    public void Dispose() => _viewModel.Dispose();
}