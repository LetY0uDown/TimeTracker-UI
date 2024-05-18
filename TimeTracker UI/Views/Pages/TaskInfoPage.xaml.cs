using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.ViewModels;
using TimeTracker.UI.Models;

namespace TimeTracker.UI.Views.Pages;

public partial class TaskInfoPage (TaskInfoViewModel viewModel) : Page, IView<TaskInfoViewModel>
{
    public TaskInfoViewModel ViewModel { get; private set; } = viewModel;

    public void Display ()
    {
        ViewModel.OnTaskUpdated += UpdateButton;
        ViewModel.OnTaskUpdated += UpdateTimeForeground;

        Task.Run(ViewModel.InitializeAsync);

        DataContext = ViewModel;
        InitializeComponent();
    }

    private void UpdateTimeForeground (TaskActionType.Kind kind)
    {
        var foreground = Application.Current.FindResource("Foreground") as SolidColorBrush;
        var danger = Application.Current.FindResource("Danger") as SolidColorBrush;

        var res = foreground;

        if (ViewModel.CurrentTask!.PlannedTime is null) {
            App.ExecuteSync(() => planTimeTB.Foreground = res);
            return;
        }

        if (ViewModel.TimeSpentOnTask.Ticks > 0 && ViewModel.CurrentTask.PlannedTime < ViewModel.TimeSpentOnTask.Ticks) {
            res = danger;
        }

        App.ExecuteSync(() => planTimeTB.Foreground = res);
    }

    private void UpdateButton (TaskActionType.Kind prevActionKind)
    {
        var dangerColor = Application.Current.FindResource("Danger") as SolidColorBrush;
        var primaryColor = Application.Current.FindResource("Primary") as SolidColorBrush;
        var successColor = Application.Current.FindResource("Success") as SolidColorBrush;

        // Выбираем, какой будет кнопка (надпись и цвет) зависимо от состояния задачи
        App.ExecuteSync(() => {
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
    
    public void Exit ()
    {
        Task.Run(ViewModel.StopAsync);
    }
}
