using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LocationWeatherMVVMPoC
{
    public partial class App : Application
    {
        public static INavigation Navigation { get; private set; }

        public App()
        {
            InitializeComponent();

            Page startPage = new MainPage();
            MainPage = new NavigationPage(startPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
