using Microsoft.Extensions.DependencyInjection;
using TimeTracker.UI.Core.Navigation;

namespace TimeTracker.UI.Core.ViewModels;

public sealed class NavigationViewModel : ViewModel, IPageHost
{
    public IView<ViewModel> CurrentView { get; private set; } = null!;

    public void NavigateToView<T> () where T : ViewModel
    {
        if (CurrentView is not null) {
            CurrentView.Exit();
        }

        CurrentView = App.Container.Services.GetRequiredService<IView<T>>();

        CurrentView.Display();
    }

    public void NavigateToView<T> (params (object value, string fieldName)[] parameters) where T : ViewModel
    {
        if (CurrentView is not null) {
            CurrentView.Exit();
        }
        
        CurrentView = App.Container.Services.GetRequiredService<IView<T>>();

        PropertiesSetter.SetParameters(CurrentView.ViewModel, parameters);

        CurrentView.Display();
    }

    public override Task InitializeAsync ()
    {
        return Task.CompletedTask;
    }
}