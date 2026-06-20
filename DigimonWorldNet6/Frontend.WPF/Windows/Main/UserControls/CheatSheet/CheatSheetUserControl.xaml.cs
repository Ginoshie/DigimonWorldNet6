using System;
using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet;

public partial class CheatSheetUserControl : IDisposable
{
    private readonly CheatSheetViewModel _viewModel;

    public CheatSheetUserControl()
    {
        InitializeComponent();

        _viewModel = new CheatSheetViewModel();
        DataContext = _viewModel;
    }

    public void Dispose() => _viewModel.Dispose();
}