using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Core.Navigation;

public sealed class NavigationService
{
    private NavigationViewModel _navigation = null!;

    public Window MainWindow { get; private set; } = null!;

    public void RegisterHost (NavigationViewModel viewModel)
    {
        _navigation = viewModel;
    }

    public void SetMainWindow<T> () where T : Window
    {
        MainWindow?.Close();
        MainWindow = App.Host.Services.GetService<T>()!;
        MainWindow.Show();
    }

    public void SetPage<T> () where T : ViewModel
    {
        _navigation.CurrentPage = App.Host.Services.GetService<INavigatablePage<T>>()!;
    }
}