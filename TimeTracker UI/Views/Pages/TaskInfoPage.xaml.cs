using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TimeTracker.UI.Core;
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

        ViewModel.CurrentTask = App.CurrentTask;

        ViewModel.OnTaskUpdated += UpdateButton;
        ViewModel.OnTaskUpdated += UpdateTimeForeground;

        InitializeComponent();

        ViewModel.Display();
    }

    private void UpdateTimeForeground (TaskActionType.Kind kind)
    {
        var foreground = Application.Current.FindResource("Foreground") as SolidColorBrush;
        var danger = Application.Current.FindResource("Danger") as SolidColorBrush;

        var res = foreground;

        if (ViewModel.CurrentTask.PlannedTime is null) {
            Application.Current.Dispatcher.Invoke(() => planTimeTB.Foreground = res);
            return;
        }

        if (ViewModel.TimeSpentOnTask.Ticks > 0 && ViewModel.CurrentTask.PlannedTime < ViewModel.TimeSpentOnTask.Ticks) {
            res = danger;
        }

        Application.Current.Dispatcher.Invoke(() => planTimeTB.Foreground = res);
    }

    private void UpdateButton (TaskActionType.Kind prevActionKind)
    {
        var dangerColor  = Application.Current.FindResource("Danger")  as SolidColorBrush;
        var primaryColor = Application.Current.FindResource("Primary") as SolidColorBrush;
        var successColor = Application.Current.FindResource("Success") as SolidColorBrush;

        // Выбираем, какой будет кнопка (надпись и цвет) зависимо от прошлого состояния задачи
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
