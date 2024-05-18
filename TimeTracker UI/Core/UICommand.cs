using System.Windows.Input;

namespace TimeTracker.UI.Core;

public sealed class UICommand : ICommand
{
    private readonly Action _execute;

    private readonly Func<bool>? _canExecute = null!;

    public UICommand (Action execute, Func<bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public UICommand (Action execute)
    {
        _execute = execute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Проверяет, может ли команда быть выполнена
    /// </summary>
    public bool CanExecute (object? parameter)
    {
        return _canExecute is null || _canExecute();
    }

    /// <summary>
    /// Выполняет команду
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute (object? parameter)
    {
        _execute();
    }
}