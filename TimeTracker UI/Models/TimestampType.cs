namespace TimeTracker.UI.Models;

public partial class TimestampType
{
    public string Title { get; set; } = null!;

    public List<Timestamp> Timestamps { get; set; } = new List<Timestamp>();
}