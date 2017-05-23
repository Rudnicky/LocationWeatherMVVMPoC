using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static LocationWeatherMVVMPoC.WeatherService;

namespace LocationWeatherMVVMPoC
{
    public class LocationManuallyPageViewModel : BaseViewModel
    {
        #region Fields & Properties
        WeatherService WeatherService { get; } = new WeatherService();

        private bool isBorderedEntryEnabled;
        public bool IsBorderedEntryEnabled
        {
            get { return isBorderedEntryEnabled; }
            set
            {
                isBorderedEntryEnabled = value;
                NotifyPropertyChanged();
            }
        }

        private bool isWarningReadVisible;
        public bool IsWarningReadVisible
        {
            get { return isWarningReadVisible; }
            set
            {
                isWarningReadVisible = value;
                NotifyPropertyChanged();
            }
        }

        private bool isWarningMessageVisible;
        public bool IsWarningMessageVisible
        {
            get { return isWarningMessageVisible; }
            set
            {
                isWarningMessageVisible = value;
                NotifyPropertyChanged();
            }
        }

        private string warning;
        public string Warning
        {
            get
            {
                return warning;
            }

            set
            {
                warning = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private ICommand onSearchButtonClickedCommand;
        public ICommand OnSearchButtonClickedCommand
        {
            get
            {
                if (onSearchButtonClickedCommand == null)
                {
                    onSearchButtonClickedCommand = new ButtonTapCommandAsync(OnSearchButtonClicked);
                }

                return onSearchButtonClickedCommand;
            }

            protected set
            {
                onSearchButtonClickedCommand = value;
            }
        }

        public bool CanExecutePayRefNumberChangedCommand { get; set; } = true;

        private ICommand longitudeChangedCommand;
        public ICommand LongitudeChangedCommand
        {
            get
            {
                if (longitudeChangedCommand == null)
                {
                    longitudeChangedCommand = new Command<TextChangedEventArgs>(OnLongitudeTextChanged, (args) =>
                    {
                       var canExecute = CanExecutePayRefNumberChangedCommand;
                       CanExecutePayRefNumberChangedCommand = true;
                       return canExecute;
                    });
                }

                return longitudeChangedCommand;
            }

            protected set
            {
                longitudeChangedCommand = value;
            }
        }

        private ICommand longitudeEditingEndedCommand;
        public ICommand LongitudeEditingEndedCommand
        {
            get
            {
                if (longitudeEditingEndedCommand == null)
                {
                    longitudeEditingEndedCommand = new Command<object>(OnLongitudeEditingEnded);
                }

                return longitudeEditingEndedCommand;
            }

            protected set
            {
                longitudeEditingEndedCommand = value;
            }
        }

        private ICommand latitudeChangedCommand;
        public ICommand LatitudeChangedCommand
        {
            get
            {
                if (latitudeChangedCommand == null)
                {
                    latitudeChangedCommand = new Command<TextChangedEventArgs>(OnLatitudeTextChanged, (args) =>
                    {
                        var canExecute = CanExecutePayRefNumberChangedCommand;
                        CanExecutePayRefNumberChangedCommand = true;
                        return canExecute;
                    });
                }

                return latitudeChangedCommand;
            }

            protected set
            {
                latitudeChangedCommand = value;
            }
        }

        private ICommand latitudeEditingEndedCommand;
        public ICommand LatitudeEditingEndedCommand
        {
            get
            {
                if (latitudeEditingEndedCommand == null)
                {
                    latitudeEditingEndedCommand = new Command<object>(OnLatitudeEditingEnded);
                }

                return latitudeEditingEndedCommand;
            }

            protected set
            {
                latitudeEditingEndedCommand = value;
            }
        }
        #endregion
        
        #region Constructor
        public LocationManuallyPageViewModel(INavigation navigation) : base(navigation)
        {
            IsBorderedEntryEnabled = true;
        }
        #endregion

        #region Events
        private async Task OnSearchButtonClicked(object args)
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                IsBorderedEntryEnabled = false;
                WeatherRoot weatherRoot = null;
                var unit = Units.Metric;
                
                if (Longitude == null || Latitude == null)
                {
                    IsWarningReadVisible = true;
                    IsWarningMessageVisible = true;
                    Warning = "Fields cannot be empty";
                    IsBorderedEntryEnabled = true;
                    IsBusy = false;
                    return;
                }

                double lat = ConvertToDouble(Latitude);
                double lon = ConvertToDouble(Longitude);
                weatherRoot = await WeatherService.GetWeather(lon, lat, unit);
                Temperature = $"{weatherRoot?.MainWeather?.Temperature ?? 0}°";
                IsBusy = false;
                IsBorderedEntryEnabled = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e);
                Temperature = "(400) Bad Request.";
                IsBorderedEntryEnabled = true;
                IsBusy = false;
            }
        }

        private void OnLongitudeTextChanged(TextChangedEventArgs textChangedArgs)
        {
            IsWarningReadVisible = false;
            IsWarningMessageVisible = false;
            this.Longitude = textChangedArgs.NewTextValue;
        }

        private void OnLongitudeEditingEnded(object longitude)
        {
            var tmp = longitude as string;
            Debug.WriteLine(tmp);
        }

        private void OnLatitudeTextChanged(TextChangedEventArgs textChangedArgs)
        {
            IsWarningReadVisible = false;
            IsWarningMessageVisible = false;
            this.Latitude = textChangedArgs.NewTextValue;
        }

        private void OnLatitudeEditingEnded(object latitude)
        {
            var tmp = latitude as string;
            Debug.WriteLine(tmp);
        }
        #endregion

        #region Methods
        private double ConvertToDouble(string str)
        {
            try
            {
                if (str.Contains(",") || str.Contains(","))
                {
                    return double.Parse(str);
                }
                else
                {
                    string tmp = str + ",00";
                    return double.Parse(tmp);
                }
            }
            catch (FormatException e)
            {
                Debug.WriteLine("FormatException - " + e);
            }
            catch (OverflowException e)
            {
                Debug.WriteLine("Overflow (outside of range) - " + e);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception - " + e);
            }
            return 0;
        }
        #endregion
    }
}
