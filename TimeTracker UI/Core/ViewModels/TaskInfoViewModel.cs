using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.Windows;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.Services;
using TimeTracker.UI.Models;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class TaskInfoViewModel : ViewModel
{
    private readonly APIClient _client;
    private readonly IPageHost _navigation;
    private readonly IHubFactory _hubFactory;

    private HubConnection _hub = null!;

    [Parameter("Task ID")]
    private int _taskID = 0;

    public TaskInfoViewModel (APIClient client, IPageHost navigation, IHubFactory hubFactory)
    {
        _client = client;
        _navigation = navigation;
        _hubFactory = hubFactory;

        FinishTaskCommand = new(async () => {
            await _client.PutAsync($"Tasks/{CurrentTask.Id}/finish");
            _navigation.NavigateToView<TaskListViewModel>();

            // Условие, при котором кнопка активна
        }, () => !CurrentTask.IsDone && CurrentTask.Actions?.Any() == true);

        PauseTaskCommand = new(async () => {
            await _client.PutAsync($"Tasks/{CurrentTask.Id}/pause");
            StartStopCommand = ResumeTaskCommand!;
        }, () => !CurrentTask.IsDone);

        StartTaskCommand = new(async () => {
            await _client.PutAsync($"Tasks/{CurrentTask.Id}/start");
            StartStopCommand = PauseTaskCommand;
        }, () => !CurrentTask.IsDone);

        ResumeTaskCommand = new(async () => {
            await _client.PutAsync($"Tasks/{CurrentTask.Id}/resume");
            StartStopCommand = PauseTaskCommand;
        }, () => !CurrentTask.IsDone);

        ReturnCommand = new(() => {
            _navigation.NavigateToView<TaskListViewModel>();
        });

        BlockCommand = new(() => { /* Пусто */ }, () => false);
    }

    public TimeSpan TimeSpentOnTask { get; private set; }

    /// <summary>
    /// Вызывается при совершении каких-то действий над задачкой. Нужно для обновления UI
    /// </summary>
    public Action<TaskActionType.Kind> OnTaskUpdated { get; set; } = null!;

    /// <summary>
    /// Нужна чтобы блокировать кнопку
    /// </summary>
    public UICommand BlockCommand { get; private init; }

    public UICommand FinishTaskCommand { get; private init; }

    public UICommand PauseTaskCommand { get; private init; }
    public UICommand StartTaskCommand { get; private init; }
    public UICommand ResumeTaskCommand { get; private init; }

    /// <summary>
    /// Команда для совершения действий (Начать / Пауза / Продолжить), меняется зависимо от статуса задачи
    /// </summary>
    public UICommand? StartStopCommand { get; private set; } = null!;

    public UICommand ReturnCommand { get; private set; }

    public TrackedTask? CurrentTask { get; set; } = new();

    public override async Task InitializeAsync ()
    {
        await ConfigureHubAsync();

        CurrentTask = await _client.GetAsync<TrackedTask>($"Tasks/{_taskID}");

        if (CurrentTask is null) {
            MessageBox.Show("Такая задачка не найдена. Обратитесь к программисту");
            _navigation.NavigateToView<TaskListViewModel>();
            return;
        }

        StartStopCommand = GetStartStopCommand();

        var actionTypeId = CurrentTask.Actions.Any() ? CurrentTask.Actions[^1]!.Type!.Id
                                                     : TaskActionType.Kind.None;

        TimeSpentOnTask = WorkTimeCounter.CalculateWorkTime(CurrentTask.Actions.ToList());

        OnTaskUpdated(actionTypeId);
    }

    public override async Task StopAsync ()
    {
        await _hub.StopAsync();
    }

    private UICommand GetStartStopCommand ()
    {
        // Если нет никаких действий, значит задачку можно только начать
        if (CurrentTask!.Actions.Count == 0) {
            return StartTaskCommand;
        }

        // Смотрим какое действие было последним
        // и возвращаем комманду для действия
        // которое может идти после
        return CurrentTask.Actions[^1]!.Type!.Id switch {
            TaskActionType.Kind.Pause => ResumeTaskCommand,
            TaskActionType.Kind.Resume or TaskActionType.Kind.Start => PauseTaskCommand,
            _ => BlockCommand
        };
    }

    private async Task ConfigureHubAsync ()
    {
        _hub = _hubFactory.CreateHub();
        await _hub.StartAsync();

        _hub.On<TaskAction>("TaskStateUpdated", action => {
            App.ExecuteSync(() => CurrentTask!.Actions?.Add(action));

            TimeSpentOnTask = WorkTimeCounter.CalculateWorkTime(CurrentTask!.Actions.ToList());

            StartStopCommand = GetStartStopCommand();

            OnTaskUpdated(CurrentTask.Actions![^1].Type!.Id);
        });
    }
}