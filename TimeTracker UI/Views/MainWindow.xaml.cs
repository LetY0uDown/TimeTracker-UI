using System.Windows;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Views;

public partial class MainWindow (IPageHost viewModel) : Window, IView<NavigationViewModel>
{
    public NavigationViewModel ViewModel { get; private set; } = (viewModel as NavigationViewModel)!;

    public void Display()
    {
        Task.Run(ViewModel.InitializeAsync);

        DataContext = ViewModel;
        InitializeComponent();
        Show();

        ViewModel.NavigateToView<TaskListViewModel> ();
    }

    public void Exit () { }
    private void ExitButtonClick (object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown ();
    }

    private void Grid_MouseLeftButtonDown (object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void MinimizeButtonClick (object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}