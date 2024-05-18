using System.Windows.Controls;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Views.Pages;

public partial class TaskListPage (TaskListViewModel viewModel) : Page, IView<TaskListViewModel>
{
    public TaskListViewModel ViewModel { get; private set; } = viewModel;

    public void Display ()
    {
        Task.Run(ViewModel.InitializeAsync);

        DataContext = ViewModel;
        InitializeComponent();
    }

    public void Exit ()
    {
        Task.Run(ViewModel.StopAsync);
    }
}