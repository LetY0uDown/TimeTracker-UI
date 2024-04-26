using TimeTracker.UI.Core.Extensions;
using TimeTracker.UI.Core.Navigation;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class NavigationViewModel (NavigationService navigation) : ViewModel
{
    private readonly NavigationService _navigation = navigation;

    private INavigatablePage<ViewModel> _currentPage = null!;
    public INavigatablePage<ViewModel> CurrentPage
    {
        get => _currentPage;
        set {
            _currentPage?.ViewModel.Exit();
            _currentPage = value;
            _currentPage.Display();
        }
    }

    public override void Display ()
    {
        CurrentPage = App.Host.Services.GetPage<TaskListViewModel>()!;
        _navigation.RegisterHost(this);
    }
}