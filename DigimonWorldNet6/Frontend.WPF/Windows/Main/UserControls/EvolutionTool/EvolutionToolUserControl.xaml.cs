using System;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionTool;

public partial class EvolutionToolUserControl : IDisposable
{
    private readonly EvolutionToolViewModel _viewModel;

    public EvolutionToolUserControl()
    {
        InitializeComponent();
        
        _viewModel = new EvolutionToolViewModel();
        
        DataContext = new EvolutionToolViewModel();
    }

    public void Dispose() => _viewModel.Dispose();
}