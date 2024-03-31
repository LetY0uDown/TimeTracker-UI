using TimeTracker.UI.Core.Services;
using TimeTracker.UI.Models;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class TaskInfoViewModel : ViewModel
{
    private readonly APIClient _client;

    public TaskInfoViewModel(APIClient client)
    {
        _client = client;
        CurrentTask = App.CurrentTask!;

        Task.Run(async () => {
            CurrentTask.Intervals = await _client.GetAsync<List<Interval>>($"Tasks/{CurrentTask.Id}/intervals");
        });
    }

    public TrackedTask CurrentTask { get; set; }
}