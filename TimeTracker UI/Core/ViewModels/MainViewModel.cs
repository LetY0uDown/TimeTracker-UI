using System.Windows;
using System.Windows.Controls;
using TimeTracker.UI.Views.Pages;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class MainViewModel : ViewModel
{
    public MainViewModel ()
    {
        CurrentPage = new TaskListPage();

        CreateTaskCommand = new(() => {
            CreateTaskVisibility = Visibility.Hidden;

            CurrentPage = new TaskCreationPage();
        });
    }

    public Page CurrentPage { get; set; } = null!;

    public UICommand CreateTaskCommand { get; set; }
    public Visibility CreateTaskVisibility { get; set; }
}