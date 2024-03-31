using Microsoft.Extensions.DependencyInjection;
using TimeTracker.UI.Core.Services;
using TimeTracker.UI.Core.ViewModels;
using TimeTracker.UI.Views;
using TimeTracker.UI.Views.Pages;

namespace TimeTracker.UI.Core.Extensions;

internal static class ServiceCollectionExtension
{
    internal static void AddServices (this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<APIClient>();
        serviceCollection.AddSingleton<NavigationService>();
    }

    internal static void AddViewModels (this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<NavigationViewModel>();
        serviceCollection.AddSingleton<TaskListViewModel>();
        serviceCollection.AddSingleton<TaskInfoViewModel>();
    }

    internal static void AddViews(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MainWindow>();
        serviceCollection.AddSingleton<TaskCreationPage>();
        serviceCollection.AddSingleton<TaskInfoPage>();
        serviceCollection.AddSingleton<TaskListPage>();
    }
}