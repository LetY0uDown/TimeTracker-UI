namespace TimeTracker.UI.Models;

public class Timestamp
{
    public int Id { get; set; }

    public long CreatedAt { get; set; }

    public TimestampType? Type { get; set; }
}
