namespace TimeTracker.UI.Models;

/// <summary>
/// Хранит записи о разных действиях с задачей. ID задача, когда сделано, тип действия
/// </summary>
public sealed class TaskAction
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    /// <summary>
    /// Время создания действия. Указывается в тиках
    /// </summary>
    public long CreatedAt { get; set; }

    public TaskActionType? Type { get; set; }
}
