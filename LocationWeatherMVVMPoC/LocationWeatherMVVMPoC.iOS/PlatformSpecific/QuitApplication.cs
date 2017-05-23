using System.Threading;
using LocationWeatherMVVMPoC.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(QuitApplication))]

namespace LocationWeatherMVVMPoC.iOS
{
    public class QuitApplication : IQuitApplication
    {
        public void Quit()
        {
            Thread.CurrentThread.Abort();
        }
    }
}