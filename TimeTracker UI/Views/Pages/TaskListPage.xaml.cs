using System.Windows.Controls;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Views.Pages;

public partial class TaskListPage : Page
{
    public TaskListPage (TaskListViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}