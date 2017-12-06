using System;
using System.Diagnostics;
using System.Windows.Input;

namespace PlanFactAnalysis.ViewModel
{
    internal sealed class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        public Predicate<object> CanExecutePredicate { get; set; }

        public RelayCommand (Action<object> execute)
            : this (execute, null)
        {

        }

        public RelayCommand (Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException ("execute");
            else
                _execute = execute;
            CanExecutePredicate = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute (object parameter)
        {
            return CanExecutePredicate == null ? true : CanExecutePredicate (parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute (object parameter)
        {
            _execute (parameter);
        }
    }
}
