using Xamarin.Forms;


namespace LocationWeatherMVVMPoC
{
    public partial class LocationAutomaticallyPage : ContentPage
    {
        private LocationAutomaticallyPageViewModel viewModel;

        public LocationAutomaticallyPage()
        {
            InitializeComponent();
            SetupPage();
        }

        private void SetupPage()
        {
            viewModel = new LocationAutomaticallyPageViewModel(Navigation);
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}