using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.ViewModels;
using TimeTracker.UI.Models;

namespace TimeTracker.UI.Views.Pages;

public partial class TaskInfoPage (TaskInfoViewModel viewModel) : Page, INavigatablePage<TaskInfoViewModel>
{
    public TaskInfoViewModel ViewModel { get; private set; } = viewModel;

    public void Display ()
    {
        DataContext = ViewModel;

        ViewModel.OnTaskUpdated += UpdateButton;
        ViewModel.CurrentTask = App.CurrentTask;

        InitializeComponent();

        ViewModel.Display();
    }

    private void UpdateButton (TaskActionType.Kind prevActionKind)
    {
        var dangerColor = Application.Current.FindResource("Danger") as SolidColorBrush;
        var primaryColor = Application.Current.FindResource("Primary") as SolidColorBrush;
        var successColor = Application.Current.FindResource("Success") as SolidColorBrush;

        Application.Current.Dispatcher.Invoke(() => {
            switch (prevActionKind) {
                case TaskActionType.Kind.Start:
                    StartStopButton.Content = "Пауза";
                    StartStopButton.Background = dangerColor;
                break;

                case TaskActionType.Kind.Pause:
                    StartStopButton.Content = "Продолжить";
                    StartStopButton.Background = primaryColor;
                break;

                case TaskActionType.Kind.Resume:
                    StartStopButton.Content = "Пауза";
                    StartStopButton.Background = dangerColor;
                break;

                default:
                    StartStopButton.Content = "Начать";
                    StartStopButton.Background = successColor;
                break;
            }
        });
    }
}
