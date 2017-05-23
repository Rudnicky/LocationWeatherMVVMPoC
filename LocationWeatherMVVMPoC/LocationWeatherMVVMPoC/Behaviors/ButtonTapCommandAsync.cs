using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LocationWeatherMVVMPoC
{
    public class ButtonTapCommandAsync : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Func<object, Task> actionToExecute;
        private bool isExecuting;

        public ButtonTapCommandAsync(Func<object, Task> actionToExecute)
        {
            this.actionToExecute = actionToExecute;
        }

        public bool CanExecute(object parameter)
        {
            return !isExecuting;
        }

        public async void Execute(object parameter)
        {
            await Task.Factory.StartNew(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    isExecuting = true;
                    CanExecuteChanged?.Invoke(this, new EventArgs());

                    await actionToExecute(parameter);

                    isExecuting = false;
                    CanExecuteChanged?.Invoke(this, new EventArgs());
                });
            });
        }
    }
}
