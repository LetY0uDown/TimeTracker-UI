using Microsoft.Extensions.DependencyInjection;
using TimeTracker.UI.Core.Navigation;

namespace TimeTracker.UI.Core.Extensions;

internal static class ServiceProviderExtensions
{
    /// <summary>
    /// Get page from DI by it's view model
    /// </summary>
    /// <typeparam name="T">Type of view model</typeparam>
    /// <returns>INavigatablePage</returns>
    internal static INavigatablePage<T>? GetPage<T> (this IServiceProvider serviceProvider) where T : ViewModel
    {
        return serviceProvider.GetService<INavigatablePage<T>>();
    }
}
