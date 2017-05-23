using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LocationWeatherMVVMPoC
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields & Properties
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigation navigation;

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                NotifyPropertyChanged();
            }
        }

        private string longitude;
        public string Longitude
        {
            get
            {
                return longitude;
            }

            set
            {
                longitude = value;
                NotifyPropertyChanged();
            }
        }

        private string latitude;
        public string Latitude
        {
            get
            {
                return latitude;
            }

            set
            {
                latitude = value;
                NotifyPropertyChanged();
            }
        }

        private string temperature = "...";
        public string Temperature
        {
            get
            {
                return temperature;
            }

            set
            {
                temperature = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private ICommand goBackButtonClickedCommand;
        public ICommand GoBackButtonClickedCommand
        {
            get
            {
                if (goBackButtonClickedCommand == null)
                {
                    goBackButtonClickedCommand = new ButtonTapCommandAsync(OnGoBackButtonClicked);
                }

                return goBackButtonClickedCommand;
            }

            protected set
            {
                goBackButtonClickedCommand = value;
            }
        }
        #endregion

        #region Constructor
        protected BaseViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
        #endregion

        #region Events
        private async Task OnGoBackButtonClicked(object args)
        {
            try
            {
                await navigation.PopAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e);
            }
        }
        #endregion

        #region Methods
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
