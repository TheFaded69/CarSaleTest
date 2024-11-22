using CarSaleSystem.Core.DbService;
using CarSaleSystem.Core.Mapper;
using CarSaleSystem.Database.DbContext;
using CarSaleSystem.Database.Models;
using CarSaleSystem.Database.Repository;
using CarSaleSystem.WPF.Mapper;
using CarSaleSystem.WPF.ViewModels;
using CarSaleSystem.WPF.Views;
using Microsoft.EntityFrameworkCore;
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

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MapperWPFConfiguration>();
            cfg.AddProfile<MapperCoreConfiguration>();
        });

        services.AddSingleton<MainWindowView>();
        services.AddSingleton<MainWindowViewModel>();
        
        services.AddTransient<IDbContextFactory<DataContext>, DbContextFactory>();
        services.AddTransient<IRepositoryCreator<DbOrder, Guid>, RepositoryCreator<DbOrder, Guid>>();
        services.AddTransient<IRepositoryCreator<DbCar, Guid>, RepositoryCreator<DbCar, Guid>>();

        services.AddTransient<ISaleDbService, SaleDbService>();
        services.AddTransient<ICarDbService, CarDbService>();
        
        return services.BuildServiceProvider();
    }
}