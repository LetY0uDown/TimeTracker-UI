using TimeTracker.UI.Models;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class TaskListViewModel : ViewModel
{
    public TaskListViewModel ()
    {
        Task.Run(async () => {
            Tasks = await APIClient.GetAsync<List<TrackedTask>>("Tasks");
        });

        ShowTrackedTasksCommand = new(async () => {
            Tasks = await APIClient.GetAsync<List<TrackedTask>>("Tasks");
        });

        ShowFinishedTasksCommand = new(async () => {
            Tasks = await APIClient.GetAsync<List<TrackedTask>>("Tasks/Done");
        });
    }

    public UICommand ShowTrackedTasksCommand { get; set; }
    public UICommand ShowFinishedTasksCommand { get; set; }

    public List<TrackedTask>? Tasks { get; set; }
}