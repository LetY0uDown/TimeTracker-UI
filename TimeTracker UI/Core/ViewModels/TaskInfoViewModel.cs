using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.Services;
using TimeTracker.UI.Models;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class TaskInfoViewModel : ViewModel
{
    private readonly APIClient _client;
    private readonly NavigationService _navigation;
    private readonly IHubFactory _hubFactory;

    private HubConnection _hub;

    public TaskInfoViewModel (APIClient client, NavigationService navigation, IHubFactory hubFactory)
    {
        _client = client;
        _navigation = navigation;
        _hubFactory = hubFactory;

        FinishTaskCommand = new(async () => {
            await _client.PutAsync($"Tasks/{CurrentTask.Id}/finish");
            _navigation.SetView<TaskListViewModel> ();
        }, () => !CurrentTask.IsDone);

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
            _navigation.SetView<TaskListViewModel>();
        });
    }

    /// <summary>
    /// Вызывается при совершении каких-то действий над задачкой. Нужно для обновления UI
    /// </summary>
    public Action<TaskActionType.Kind> OnTaskUpdated { get; set; }

    public UICommand FinishTaskCommand { get; private init; }

    public UICommand PauseTaskCommand { get; private init; }
    public UICommand StartTaskCommand { get; private init; }
    public UICommand ResumeTaskCommand { get; private init; }

    /// <summary>
    /// Команда для совершения действий (Начать / Пауза / Продолжить), меняется зависимо от статуса задачи
    /// Привязана к UI
    /// </summary>
    public UICommand? StartStopCommand { get; private set; } = null!;

    public UICommand ReturnCommand { get; private set; }

    public TrackedTask CurrentTask { get; set; } = new();
    public ObservableCollection<TaskAction> Actions { get; private set; } = null!;

    public override void Display ()
    {
        ConfigureHub();

        Task.Run(async () => {
            Actions = new((await _client.GetAsync<List<TaskAction>>($"Tasks/{CurrentTask.Id}/actions"))!
                                         .OrderBy(action => action.Id))!;

            StartStopCommand = GetStartStopCommand();

            var actionTypeId = Actions.Any() ? Actions[^1]!.Type!.Id : TaskActionType.Kind.Start;

            OnTaskUpdated(actionTypeId);
        });

    }

    public override async void Exit ()
    {
        await _hub.StopAsync();
    }

    private UICommand? GetStartStopCommand ()
    {
        // Если нет никаких действий, значит задачку можно только начать
        if (Actions.Count == 0) {
            return StartTaskCommand;
        }

        // Смотрим какое действие было последним
        // и возвращаем комманду для действия
        // которое может идти после
        return Actions[^1]!.Type!.Id switch {
            TaskActionType.Kind.Pause => ResumeTaskCommand,
            TaskActionType.Kind.Resume or TaskActionType.Kind.Start => PauseTaskCommand,
            _ => null
        };
    }

    private void ConfigureHub ()
    {
        _hub = _hubFactory.CreateHub();
        Task.Run(async () => await _hub.StartAsync());

        _hub.On<TaskAction>("TaskStateUpdated", action => {
            // Методы, работающие с UI приходится вызывать через Dispatcher,
            // т.к. WPF очень не любит, когда с ним работают из разных потоков
            Application.Current.Dispatcher.Invoke(() => {
                Actions?.Add(action);
            });

            StartStopCommand = GetStartStopCommand();

            OnTaskUpdated(Actions![^1].Type!.Id);
        });
    }
}