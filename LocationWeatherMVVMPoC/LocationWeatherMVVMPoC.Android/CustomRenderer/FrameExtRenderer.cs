using LocationWeatherMVVMPoC;
using LocationWeatherMVVMPoC.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FrameExt), typeof(FrameExtRenderer))]

namespace LocationWeatherMVVMPoC.Droid
{
    public class FrameExtRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            SetBackgroundResource(Resource.Drawable.frame_border);
        }
    }
}