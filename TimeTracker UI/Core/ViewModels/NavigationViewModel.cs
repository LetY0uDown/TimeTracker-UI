using System.Windows.Controls;
using TimeTracker.UI.Views.Pages;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class NavigationViewModel : ViewModel
{
    public NavigationViewModel (TaskListPage taskListPage)
    {
        CurrentPage = taskListPage;
    }

    public Page CurrentPage { get; set; } = null!;
}