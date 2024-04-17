namespace TimeTracker.UI.Models;

public class TrackedTask
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public long? PlannedTime { get; set; }

    public bool IsDone { get; set; }

    public bool HasPlannedTime => PlannedTime is not null;

    public List<Timestamp> Timestamps { get; set; } = new List<Timestamp>();
}