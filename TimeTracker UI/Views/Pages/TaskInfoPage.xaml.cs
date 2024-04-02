using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using TimeTracker.UI.Core.ViewModels;

namespace TimeTracker.UI.Views.Pages;

public partial class TaskInfoPage : Page
{
    private bool _isStartButtonChecked = false;

    public TaskInfoPage (TaskInfoViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    protected override void OnRender (DrawingContext drawingContext)
    {
        var vm = DataContext as TaskInfoViewModel;
        vm.CurrentTask = App.CurrentTask;
    }

    private void StartButtonClick (object sender, RoutedEventArgs e)
    {
        var button = (sender as ToggleButton)!;

        var dangerColor = Application.Current.FindResource("Danger") as SolidColorBrush;
        var primaryColor = Application.Current.FindResource("Primary") as SolidColorBrush;

        if (_isStartButtonChecked) {
            button.Content = "Начать";
            button.Background = primaryColor;
        } else {
            button.Content = "Пауза";
            button.Background = dangerColor;
        }
        _isStartButtonChecked = !_isStartButtonChecked;
    }
}
