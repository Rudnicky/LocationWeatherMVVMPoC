using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LocationWeatherMVVMPoC
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Properties
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private ICommand onEnterLocationManuallyButtonClickedCommand;
        public ICommand OnEnterLocationManuallyButtonClickedCommand
        {
            get
            {
                if (onEnterLocationManuallyButtonClickedCommand == null)
                {
                    onEnterLocationManuallyButtonClickedCommand = new ButtonTapCommandAsync(OnEnterLocationManuallyButtonClicked);
                }

                return onEnterLocationManuallyButtonClickedCommand;
            }

            protected set
            {
                onEnterLocationManuallyButtonClickedCommand = value;
            }
        }

        private ICommand onEnterLocationAutomaticallyButtonClickedCommand;
        public ICommand OnEnterLocationAutomaticallyButtonClickedCommand
        {
            get
            {
                if (onEnterLocationAutomaticallyButtonClickedCommand == null)
                {
                    onEnterLocationAutomaticallyButtonClickedCommand = new ButtonTapCommandAsync(OnEnterLocationAutomaticallyButtonClicked);
                }

                return onEnterLocationAutomaticallyButtonClickedCommand;
            }

            protected set
            {
                onEnterLocationAutomaticallyButtonClickedCommand = value;
            }
        }

        private ICommand onExitApplicationButtonClickedCommand;
        public ICommand OnExitApplicationButtonClickedCommand
        {
            get
            {
                if (onExitApplicationButtonClickedCommand == null)
                {
                    onExitApplicationButtonClickedCommand = new ButtonTapCommandAsync(OnExitApplicationButtonClicked);
                }

                return onExitApplicationButtonClickedCommand;
            }

            protected set
            {
                onExitApplicationButtonClickedCommand = value;
            }
        }
        #endregion

        #region Constructor
        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            UserName = "Hello -> User <-";
        }
        #endregion

        #region Events
        private async Task OnEnterLocationManuallyButtonClicked(object args)
        {
            try
            {
                await navigation.PushAsync(new LocationManuallyPage());
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e);
            }
        }

        private async Task OnEnterLocationAutomaticallyButtonClicked(object args)
        {
            try
            {
                await navigation.PushAsync(new LocationAutomaticallyPage());
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e);
            }
        }

        private async Task OnExitApplicationButtonClicked(object args)
        {
            try
            {
                DependencyService.Get<IQuitApplication>().Quit();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e);
            }
        }
        #endregion
    }
}
