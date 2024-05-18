namespace TimeTracker.UI.Core.Navigation;

public interface IView<out T> where T : ViewModel
{
    T ViewModel { get; }

    void Display ();

    void Exit ();
}