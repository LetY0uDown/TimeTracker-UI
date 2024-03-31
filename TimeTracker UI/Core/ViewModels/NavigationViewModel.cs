using System.Windows.Controls;
using TimeTracker.UI.Core.Services;
using TimeTracker.UI.Views.Pages;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class NavigationViewModel : ViewModel
{
    public NavigationViewModel (NavigationService navigation, TaskListPage taskListPage)
    {
        navigation.RegisterHost(this);
        CurrentPage = taskListPage;
    }

    public Page CurrentPage { get; set; } = null!;
}