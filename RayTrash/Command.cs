using System;
using System.Windows.Input;

namespace RayTrash
{
    public class Command<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;
        public Command(Action<T> execute) : this(execute, o => true)
        {
        }
        public Command(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
            => _canExecute((T)parameter);

        public void Execute(object parameter)
            => _execute((T)parameter);
    }

    public class Command : Command<object>
    {
        public Command(Action execute)
            : this(execute, () => true)
        {
        }
        public Command(Action execute, Func<bool> canExecute) : base(o => execute(), o => canExecute())
        {
        }
    }
}
