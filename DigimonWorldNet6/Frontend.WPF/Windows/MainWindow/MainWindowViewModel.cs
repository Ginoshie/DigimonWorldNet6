using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using DigimonWorld.Frontend.WPF.Models;
using Generics.Enums;

namespace DigimonWorld.Frontend.WPF.Windows.MainWindow;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private bool _leftPaneIsOpen;

    public MainWindowViewModel(Window window)
    {
        MinimizeCommand = new CommandHandler(() => window.WindowState = WindowState.Minimized);

        CloseCommand = new CommandHandler(window.Close);

        DragCommand = new CommandHandler(() => DragWindow(window));
        
        ToggleLeftPaneCommand = new CommandHandler(ToggleLeftPane);
        
        ToggleBottomPaneCommand = new CommandHandler(ToggleBottomPane);

        HistoricEvolutionClickedCommand = new RelayCommand(UpdateHistoricEvolution);
    }

    public bool LeftPaneIsOpen
    {
        get => _leftPaneIsOpen;
        private set
        {
            if (_leftPaneIsOpen == value) return;

            _leftPaneIsOpen = value;
            OnPropertyChanged();
        }
    }

    public bool BottomPaneIsOpen
    {
        get => _leftPaneIsOpen;
        private set
        {
            if (_leftPaneIsOpen == value) return;

            _leftPaneIsOpen = value;
            OnPropertyChanged();
        }
    }
    
    public IList<DigimonType> HistoricEvolutions { get; set; } = [];

    public ICommand MinimizeCommand { get; }

    public ICommand CloseCommand { get; }

    public ICommand DragCommand { get; }
    
    public ICommand ToggleLeftPaneCommand { get; }
    
    public ICommand ToggleBottomPaneCommand { get; }
    
    public ICommand HistoricEvolutionClickedCommand { get; }

    private void DragWindow(Window window)
    {
        if (Mouse.PrimaryDevice.LeftButton != MouseButtonState.Pressed) return;

        try
        {
            window.DragMove();
        }
        catch (InvalidOperationException)
        {
            // Ignore exceptions caused by improper DragMove calls
        }
    }
    
    private void ToggleLeftPane()
    {
        LeftPaneIsOpen = !LeftPaneIsOpen;
    }
    
    private void ToggleBottomPane()
    {
        BottomPaneIsOpen = !BottomPaneIsOpen;
    }

    
    private void UpdateHistoricEvolution(object parameter)
    {
        if (parameter is not DigimonIcon digimonIcon) return;
        
        if (!HistoricEvolutions.Remove(digimonIcon.DigimonType))
        {
            HistoricEvolutions.Add(digimonIcon.DigimonType);
        }
        
        OnPropertyChanged(nameof(HistoricEvolutions));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}