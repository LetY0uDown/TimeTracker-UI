using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.Services;
using TimeTracker.UI.Models;
using TimeTracker.UI.Views.Pages;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class TaskInfoViewModel : ViewModel
{
    private readonly APIClient _client;
    private readonly NavigationService _navigation;

    public TaskInfoViewModel (APIClient client, NavigationService navigation)
    {
        _client = client;
        _navigation = navigation;

        FinishTaskCommand = new(async () => {
            await _client.PutAsync($"Tasks/{CurrentTask.Id}/finish");
        });

        PauseTaskCommand = new(async () => {
            await _client.PutAsync($"Tasks/{CurrentTask.Id}/pause");
            StartStopCommand = ResumeTaskCommand!;
        });

        StartTaskCommand = new(async () => {
            await _client.PutAsync($"Tasks/{CurrentTask.Id}/start");
            StartStopCommand = PauseTaskCommand;
        });

        ResumeTaskCommand = new(async () => {
            await _client.PutAsync($"Tasks/{CurrentTask.Id}/resume");
            StartStopCommand = PauseTaskCommand;
        });

        // TODO: Do something with it
        //StartStopCommand = CurrentTask.Timestamps.Count == 0 ? StartTaskCommand
        //                                                     : CurrentTask.IsPaused ? ResumeTaskCommand
        //                                                                            : PauseTaskCommand;

        ReturnCommand = new(() => {
            _navigation.SetPage<TaskListViewModel>();
        });
    }

    public UICommand FinishTaskCommand { get; private init; }
    public UICommand PauseTaskCommand { get; private init; }
    public UICommand StartTaskCommand { get; private init; }
    public UICommand ResumeTaskCommand { get; private init; }

    public UICommand StartStopCommand { get; private set; }

    public UICommand ReturnCommand { get; private set; }

    public TrackedTask CurrentTask { get; set; } = new();
    public List<TaskAction> Actions { get; private set; }

    public override void Display ()
    {
        Task.Run(async () => {
            Actions = (await _client.GetAsync<List<TaskAction>>($"Tasks/{CurrentTask.Id}/actions"))!;
        });
    }
}