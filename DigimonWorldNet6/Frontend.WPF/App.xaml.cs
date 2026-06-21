/*
Copyright © 2026 Ginoshie

Licensed for non-commercial use only.
Modification, redistribution, or reuse of this code
in other projects is strictly prohibited.
*/

using System.Text;
using System.Windows;
using DigimonWorld.Frontend.WPF.Windows.Main;
using Domain;
using MemoryAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI.Builder;
using Velopack;

namespace DigimonWorld.Frontend.WPF;

public partial class App
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        VelopackApp.Build().Run();

        RxAppBuilder.CreateReactiveUIBuilder()
            .WithPlatformServices()
            .WithCoreServices()
            .BuildApp();

        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<MainWindow>();
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

        UserDigimon _ = UserDigimon.Instance;
        
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();

        base.OnExit(e);
    }
}