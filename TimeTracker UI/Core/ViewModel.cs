using System.ComponentModel;

namespace TimeTracker.UI.Core;

public abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
}