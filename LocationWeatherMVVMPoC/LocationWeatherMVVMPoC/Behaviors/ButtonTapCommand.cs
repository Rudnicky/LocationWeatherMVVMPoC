using System;
using System.Windows.Input;

namespace LocationWeatherMVVMPoC
{
    public class ButtonTapCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> actionToExecute;
        private bool isExecuting;

        public ButtonTapCommand(Action<object> actionToExecute)
        {
            this.actionToExecute = actionToExecute;
        }

        public bool CanExecute(object parameter)
        {
            return !isExecuting;
        }

        public void Execute(object parameter)
        {
            isExecuting = true;
            CanExecuteChanged?.Invoke(this, new EventArgs());

            actionToExecute(parameter);

            isExecuting = false;
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
