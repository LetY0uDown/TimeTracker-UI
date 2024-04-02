using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeTracker.UI.Core;

public abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged ([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new(name));
    }
}