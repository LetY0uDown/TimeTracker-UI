using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.Services;
using TimeTracker.UI.Models;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class TaskListViewModel : ViewModel
{
    private readonly APIClient _client;
    private readonly NavigationService _navigation;
    private readonly IHubFactory _hubFactory;

    private HubConnection _hub;

    private bool _showingFinished = false;

    public TaskListViewModel (APIClient client, NavigationService navigation, IHubFactory hubFactory)
    {
        _client = client;
        _navigation = navigation;
        _hubFactory = hubFactory;

        ShowTrackedTasksCommand = new(async () => {
            Tasks = await _client.GetAsync<ObservableCollection<TrackedTask>>("Tasks");
            _showingFinished = false;
        });

        ShowFinishedTasksCommand = new(async () => {
            Tasks = await _client.GetAsync<ObservableCollection<TrackedTask>>("Tasks/Done");
            _showingFinished = true;
        });

        CreateTaskCommand = new(() => {
            _navigation.SetView<TaskCreationViewModel>();
        });

        ShowSelectedTask = new(() => {
            App.CurrentTask = SelectedTask;
            _navigation.SetView<TaskInfoViewModel>();
        });
    }

    public UICommand ShowTrackedTasksCommand { get; set; }
    public UICommand ShowFinishedTasksCommand { get; set; }
    public UICommand CreateTaskCommand { get; set; }

    public UICommand ShowSelectedTask { get; set; }

    public TrackedTask? SelectedTask { get; set; }

    public ObservableCollection<TrackedTask>? Tasks { get; set; }

    public override void Display ()
    {
        Task.Run(async () => {
            Tasks = await _client.GetAsync<ObservableCollection<TrackedTask>>("Tasks");
        });

        ConfigureHub();
    }

    public override async void Exit ()
    {
        await _hub.StopAsync();
    }

    private void ConfigureHub ()
    {
        _hub = _hubFactory.CreateHub();
        Task.Run(async () => await _hub.StartAsync());

        _hub.On<TrackedTask>("TaskPosted", task => {
            // Добавляем новую задачку в список только если смотрим незавершённые задачи
            if (!_showingFinished) {
                Tasks?.Add(task);
            }
        });
    }
}