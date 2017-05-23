using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using LocationWeatherMVVMPoC;
using LocationWeatherMVVMPoC.Droid;

[assembly: ExportRenderer(typeof(CustomBtn), typeof(ExtButtonRenderer))]

namespace LocationWeatherMVVMPoC.Droid
{
    public class ExtButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var formsButton = e.NewElement;

            if (Control != null
               && e.NewElement != null)
            {

                if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    Control.StateListAnimator = null;
                }

                Control.SetBackgroundResource(Resource.Drawable.corners_primary);
            }
        }
    }
}