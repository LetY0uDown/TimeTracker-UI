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

        Task.Run(async () => {
            Timestamps = (await _client.GetAsync<List<Timestamp>>($"Tasks/{CurrentTask.Id}/timestamps"))!;
        });

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

        StartStopCommand = CurrentTask.Timestamps.Count == 0 ? StartTaskCommand
                                                             : CurrentTask.IsPaused ? ResumeTaskCommand
                                                                                    : PauseTaskCommand;

        ReturnCommand = new(() => {
            _navigation.SetPage<TaskListPage>();
        });
    }

    public UICommand FinishTaskCommand { get; private init; }
    public UICommand PauseTaskCommand { get; private init; }
    public UICommand StartTaskCommand { get; private init; }
    public UICommand ResumeTaskCommand { get; private init; }

    public UICommand StartStopCommand { get; private set; }

    public UICommand ReturnCommand { get; private set; }

    public TrackedTask CurrentTask { get; set; } = new();
    public List<Timestamp> Timestamps { get; private set; }
}