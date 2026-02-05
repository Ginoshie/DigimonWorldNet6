/*
Copyright © 2026 Ginoshie

Licensed for non-commercial use only.
Modification, redistribution, or reuse of this code
in other projects is strictly prohibited.
*/

using System.Windows;
using DigimonWorld.Evolution.Calculator.Core.Modules;
using DigimonWorld.Frontend.WPF.Windows.Main;
using MemoryAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DigimonWorld.Frontend.WPF;

public partial class App
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<MainWindow>();

                EvolutionCalculatorModule evolutionCalculatorModule = new();
                evolutionCalculatorModule.RegisterServices(services);
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        MainWindow startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();

        base.OnStartup(e);

        LiveMemoryReader.Instance.Start();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();

        base.OnExit(e);
    }
}