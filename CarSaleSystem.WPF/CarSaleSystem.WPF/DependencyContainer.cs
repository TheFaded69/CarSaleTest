using CarSaleSystem.WPF.ViewModels;
using CarSaleSystem.WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CarSaleSystem.WPF;

internal static class DependencyContainer
{
    internal static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddLogging(options =>
        {
            options.AddSerilog(dispose: true);
            options.AddDebug();
        });

        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        return services.BuildServiceProvider();
    }
}