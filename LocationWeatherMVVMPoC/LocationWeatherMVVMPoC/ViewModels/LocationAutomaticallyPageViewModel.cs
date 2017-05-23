using Xamarin.Forms;
using Plugin.Geolocator;
using static LocationWeatherMVVMPoC.WeatherService;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;

namespace LocationWeatherMVVMPoC
{
    public class LocationAutomaticallyPageViewModel : BaseViewModel
    {
        #region Fields & Properties
        WeatherService WeatherService { get; } = new WeatherService();
        #endregion

        #region Commands
        private ICommand onObtainButtonClickedCommand;
        public ICommand OnObtainButtonClickedCommand
        {
            get
            {
                if (onObtainButtonClickedCommand == null)
                {
                    onObtainButtonClickedCommand = new ButtonTapCommandAsync(OnObtainButtonClicked);
                }

                return onObtainButtonClickedCommand;
            }

            protected set
            {
                onObtainButtonClickedCommand = value;
            }
        }
        #endregion

        #region Constructor
        public LocationAutomaticallyPageViewModel(INavigation navigation) : base(navigation)
        {

        }
        #endregion

        #region Events
        private async Task OnObtainButtonClicked(object args)
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                WeatherRoot weatherRoot = null;
                     
                if (IsGeolocationEnabled())
                {
                    var unit = Units.Metric;
                    var local = await CrossGeolocator.Current.GetPositionAsync(10000);
                    Latitude = local.Latitude.ToString();
                    Longitude = local.Longitude.ToString();
                    weatherRoot = await WeatherService.GetWeather(local.Latitude, local.Longitude, unit);
                    Temperature = $"{weatherRoot?.MainWeather?.Temperature ?? 0}°";
                    IsBusy = false;
                } 
                else
                {
                    IsBusy = false;
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e);
                Temperature = "(400) Bad Request.";
                IsBusy = false;
            }
        }
        #endregion

        #region Methods
        bool IsGeolocationEnabled()
        {
            if (!CrossGeolocator.Current.IsGeolocationEnabled)
            {
                Latitude = "Not available";
                Longitude = "Not available";
                Temperature = "Please enable GPS and try again";
                return false;
            }
            else
            {
                Temperature = "Please wait";
                return true;
            }
        }
        #endregion
    }
}
