using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeTracker.UI.Core;

public abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public abstract Task InitializeAsync ();

    public virtual Task StopAsync() { return Task.CompletedTask; }

    [Obsolete("Should be called automaticly with Fody nuget package")]
    protected void OnPropertyChanged ([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new(name));
    }
}