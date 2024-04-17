using TimeTracker.UI.Core.Extensions;
using TimeTracker.UI.Core.Navigation;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class NavigationViewModel (NavigationService navigation) : ViewModel
{
    private readonly NavigationService _navigation = navigation;

    public INavigatablePage<ViewModel> CurrentPage { get; set; } = null!;

    public override void Display ()
    {
        CurrentPage = App.Host.Services.GetPage<TaskListViewModel>()!;
        _navigation.RegisterHost(this);
    }
}