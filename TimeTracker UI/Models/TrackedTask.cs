using System.Collections.ObjectModel;

namespace TimeTracker.UI.Models;

public sealed class TrackedTask
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public long? PlannedTime { get; set; }

    public bool IsDone { get; set; }

    public ObservableCollection<TaskAction> Actions { get; set; } = new ();
}