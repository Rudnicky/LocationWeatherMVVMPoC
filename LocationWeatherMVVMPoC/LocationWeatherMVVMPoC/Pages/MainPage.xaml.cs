using Xamarin.Forms;

namespace LocationWeatherMVVMPoC
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            SetupPage();
        }
        private void SetupPage()
        {
            viewModel = new MainPageViewModel(Navigation);
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
