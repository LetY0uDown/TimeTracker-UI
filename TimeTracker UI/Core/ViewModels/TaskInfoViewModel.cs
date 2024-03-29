using TimeTracker.UI.Models;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class TaskInfoViewModel : ViewModel
{
    public TaskInfoViewModel()
    {
        CurrentTask = App.CurrentTask!;
    }

    public TrackedTask CurrentTask { get; set; }
}