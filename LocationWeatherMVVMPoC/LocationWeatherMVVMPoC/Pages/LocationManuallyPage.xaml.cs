using Xamarin.Forms;


namespace LocationWeatherMVVMPoC
{
    public partial class LocationManuallyPage : ContentPage
    {
        private LocationManuallyPageViewModel viewModel;

        public LocationManuallyPage()
        {
            InitializeComponent();
            SetupPage();
        }

        private void SetupPage()
        {
            viewModel = new LocationManuallyPageViewModel(Navigation);
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}