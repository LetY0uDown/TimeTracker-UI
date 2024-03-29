using System.Windows.Controls;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Views.Pages;

public partial class TaskInfoPage : Page
{
    public TaskInfoPage (TaskInfoViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}
