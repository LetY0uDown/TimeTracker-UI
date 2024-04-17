using System.Windows.Controls;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Views.Pages;

public partial class TaskListPage : Page, INavigatablePage<TaskListViewModel>
{
    public TaskListPage (TaskListViewModel viewModel)
    {
        DataContext = viewModel;
        ViewModel = viewModel;
    }

    public TaskListViewModel ViewModel { get; private set; }

    public void Display ()
    {
        ViewModel.Display();
        InitializeComponent();
    }
}