using System;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvolutionCalculator;

public partial class EvolutionCalculatorUserControl : IDisposable
{
    private readonly EvolutionCalculatorViewModel _viewModel;

    public EvolutionCalculatorUserControl()
    {
        InitializeComponent();

        _viewModel = new EvolutionCalculatorViewModel();

        DataContext = new EvolutionCalculatorViewModel();
    }

    public void Dispose() => _viewModel.Dispose();
}