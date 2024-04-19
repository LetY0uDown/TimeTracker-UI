namespace TimeTracker.UI.Models;

public sealed class TaskActionType
{
    public Kind Id { get; set; }

    public string Title { get; set; } = null!;

    public enum Kind
    {
        Start, Pause, Resume, Finish
    }
}
