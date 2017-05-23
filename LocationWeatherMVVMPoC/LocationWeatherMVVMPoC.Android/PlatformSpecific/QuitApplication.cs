using Android.App;
using Android.OS;
using LocationWeatherMVVMPoC.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(QuitApplication))]

namespace LocationWeatherMVVMPoC.Droid
{
    public class QuitApplication : IQuitApplication
    {
        public void Quit()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();

            Process.KillProcess(Process.MyPid());
        }
    }
}