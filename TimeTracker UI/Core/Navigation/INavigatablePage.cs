namespace TimeTracker.UI.Core.Navigation;

public interface INavigatablePage<out T> where T : ViewModel
{
    T ViewModel { get; }

    /// <summary>
    /// Show a page on a navigation host
    /// </summary>
    void Display ();
}