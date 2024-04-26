using TimeTracker.UI.Core.Navigation;
using TimeTracker.UI.Core.Services;
using TimeTracker.UI.Models;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class TaskCreationViewModel : ViewModel
{
    private readonly APIClient _client;
    private readonly NavigationService _navigation;

    public TaskCreationViewModel(APIClient client, NavigationService navigation)
    {
        _client = client;
        _navigation = navigation;
        CreateTaskCommand = new(async () => {
            TimeSpan? plan = TimeSpan.Parse($"{Hours}:{Minutes}");
            var task = new TrackedTask {
                Title = Title,
                Description = Description,
                PlannedTime = plan?.Ticks,
            };

            await _client.PostAsync(task, "Tasks");

            Title = string.Empty;
            Description = string.Empty;
            Hours = 0;
            Minutes = 0;
        }, () => !string.IsNullOrWhiteSpace(Title));

        ReturnCommand = new(() => {
            _navigation.SetView<TaskListViewModel>();
        });
    }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public int Hours { get; set; }
    public int Minutes { get; set; }

    public UICommand CreateTaskCommand { get; private init; }
    public UICommand ReturnCommand { get; private init; }

    public override void Display ()
    {
        Title = string.Empty;
        Description = string.Empty;
        Hours = 0;
        Minutes = 0;
    }
}