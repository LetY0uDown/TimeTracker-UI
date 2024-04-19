using System.Windows.Controls;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Views.Pages;

public partial class TaskCreationPage (TaskCreationViewModel vm) : Page, INavigatablePage<TaskCreationViewModel>
{
    public TaskCreationViewModel ViewModel { get; private set; } = vm;

    public void Display ()
    {
        DataContext = ViewModel;
        InitializeComponent();
    }
}
