using System;

namespace DigimonWorld.Frontend.WPF.Windows.Main;

public partial class MainWindow
{
    private readonly MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        _viewModel = new MainWindowViewModel(this);

        DataContext = _viewModel;

        Closed += MainWindow_Closed;
    }

    private void MainWindow_Closed(object? sender, EventArgs e)
    {
        if (CurrentSelectedMainWindowContent is IDisposable disposableContent)
        {
            disposableContent.Dispose();
        }

        _viewModel.Dispose();
    }
}