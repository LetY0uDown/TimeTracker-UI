namespace TimeTracker.UI.Core.Navigation;

public interface IPageHost
{
    IView<ViewModel> CurrentView { get; }

    void NavigateToView<T> () where T : ViewModel;

    void NavigateToView<T> (params (object value, string fieldName)[] parameters) where T : ViewModel;
}