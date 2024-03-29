using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Core.Services;

public sealed class NavigationService
{
    private readonly NavigationViewModel _navigation;

    public NavigationService (NavigationViewModel navigation)
    {
        _navigation = navigation;
    }

    public Window MainWindow { get; private set; } = null!;

    public void SetMainWindow<T>() where T : Window
    {
        MainWindow?.Close();
        MainWindow = App.Host.Services.GetService<T>()!;
        MainWindow.Show();
    }

    public void SetPage<T> () where T : Page
    {
        _navigation.CurrentPage = App.Host.Services.GetService<T>()!;
    }
}