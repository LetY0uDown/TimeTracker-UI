using System.Windows;
using System.Windows.Controls;
using TimeTracker.UI.Core.ViewModels;
using TimeTracker.UI.Models;

namespace TimeTracker.UI.Views.Pages;

public partial class TaskListPage : Page
{
    public TaskListPage (TaskListViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    private void ListBox_SelectionChanged (object sender, SelectionChangedEventArgs e)
    {
        // DEBUG
        //MessageBox.Show((((sender as ListBox)!.SelectedItem) as TrackedTask)?.Title);
    }
}