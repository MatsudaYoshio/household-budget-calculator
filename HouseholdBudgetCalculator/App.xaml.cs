using HouseholdBudgetCalculator.Services;
using HouseholdBudgetCalculator.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace HouseholdBudgetCalculator;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        services.AddSingleton<CsvReaderService>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();

        var serviceProvider = services.BuildServiceProvider();

        var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.Show();
    }
}

