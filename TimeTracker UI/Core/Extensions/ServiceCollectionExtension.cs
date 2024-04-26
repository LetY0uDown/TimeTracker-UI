using Microsoft.Extensions.DependencyInjection;
using TimeTracker.UI.Core.Navigation;
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

        serviceCollection.AddTransient<IHubFactory, HubFactory>();
    }

    internal static void AddViewModels (this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<NavigationViewModel>();
        serviceCollection.AddTransient<TaskListViewModel>();
        serviceCollection.AddTransient<TaskInfoViewModel>();
        serviceCollection.AddTransient<TaskCreationViewModel>();
    }

    internal static void AddViews(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MainWindow>();

        serviceCollection.AddTransient<INavigatablePage<TaskCreationViewModel>, TaskCreationPage>();
        serviceCollection.AddTransient<INavigatablePage<TaskInfoViewModel>,     TaskInfoPage>();
        serviceCollection.AddTransient<INavigatablePage<TaskListViewModel>,     TaskListPage>();
    }
}