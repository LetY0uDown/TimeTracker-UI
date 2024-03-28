using System.Windows;

namespace TimeTracker.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow ()
    {
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