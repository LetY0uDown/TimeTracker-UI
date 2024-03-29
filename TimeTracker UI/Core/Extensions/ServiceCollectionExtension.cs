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
    }

    internal static void AddViewModels (this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<NavigationViewModel>();
        serviceCollection.AddSingleton<TaskListViewModel>();
    }

    internal static void AddViews(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MainWindow>();
        serviceCollection.AddSingleton<TaskCreationPage>();
        serviceCollection.AddSingleton<TaskInfoPage>();
        serviceCollection.AddSingleton<TaskListPage>();
    }
}