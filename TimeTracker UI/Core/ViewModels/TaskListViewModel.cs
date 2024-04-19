using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.Services;
using TimeTracker.UI.Models;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class TaskListViewModel : ViewModel
{
    private readonly APIClient _client;
    private readonly NavigationService _navigation;

    public TaskListViewModel (APIClient client, NavigationService navigation)
    {
        _client = client;
        _navigation = navigation;

        ShowTrackedTasksCommand = new(async () => {
            Tasks = await _client.GetAsync<List<TrackedTask>>("Tasks");
        });

        ShowFinishedTasksCommand = new(async () => {
            Tasks = await _client.GetAsync<List<TrackedTask>>("Tasks/Done");
        });

        CreateTaskCommand = new(() => {
            _navigation.SetPage<TaskCreationViewModel>();
        });

        ShowSelectedTask = new(() => {
            App.CurrentTask = SelectedTask;
            _navigation.SetPage<TaskInfoViewModel>();
        });
    }

    public UICommand ShowTrackedTasksCommand { get; set; }
    public UICommand ShowFinishedTasksCommand { get; set; }
    public UICommand CreateTaskCommand { get; set; }

    public UICommand ShowSelectedTask { get; set; }

    public TrackedTask? SelectedTask { get; set; }

    public List<TrackedTask>? Tasks { get; set; }

    public override void Display ()
    {
        Task.Run(async () => {
            Tasks = await _client.GetAsync<List<TrackedTask>>("Tasks");
        });
    }
}