using System.Windows;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow (NavigationViewModel viewModel)
    {
        DataContext = viewModel;
        viewModel.Display();
        InitializeComponent();
    }

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