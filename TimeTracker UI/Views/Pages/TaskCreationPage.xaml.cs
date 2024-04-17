using System.Windows.Controls;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Views.Pages;

public partial class TaskCreationPage : Page, INavigatablePage<TaskCreationViewModel>
{
    public TaskCreationPage ()
    {
        InitializeComponent();
    }

    public TaskCreationViewModel ViewModel => throw new NotImplementedException();

    public void Display ()
    {
        throw new NotImplementedException();
    }
}
