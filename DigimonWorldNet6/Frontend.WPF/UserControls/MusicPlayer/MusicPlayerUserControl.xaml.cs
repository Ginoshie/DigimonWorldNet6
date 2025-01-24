using System;

namespace DigimonWorld.Frontend.WPF.UserControls.MusicPlayer;

public partial class MusicPlayerUserControl : IDisposable
{
    private readonly MusicPlayerViewModel _viewModel = new();    
    
    public MusicPlayerUserControl()
    {
        InitializeComponent();

        DataContext = _viewModel;
    }

    public void Dispose()
    {
        _viewModel.Dispose();
    }
}