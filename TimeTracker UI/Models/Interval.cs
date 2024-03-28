namespace TimeTracker.UI.Models;

public class Interval
{
    public long WorkingTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public TrackedTask? Task { get; set; } = null;
}