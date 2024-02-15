using System.Windows;
using DigimonWorld.Evolution.Calculator.Core.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DigimonWorld.Frontend.WPF;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                
                var evolutionCalculatorModule = new EvolutionCalculatorModule();
                evolutionCalculatorModule.RegisterServices(services);
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
        startupForm.Show();
        
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        
        base.OnExit(e);
    }
}