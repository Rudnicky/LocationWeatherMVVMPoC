using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using LocationWeatherMVVMPoC.Droid.CustomRenderer;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]

namespace LocationWeatherMVVMPoC.Droid.CustomRenderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        #region Properties & Fields
        private Entry entry;
        #endregion

        #region Methods
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            entry = e.NewElement;

            if (this.Control != null)
            {
                this.Control.SetBackgroundColor(Android.Graphics.Color.White);

                var entryEditText = Control as EditText;
                if (entryEditText != null)
                {
                    entryEditText.SetPadding(15, 15, 15, 15);

                    if (e.NewElement.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center)
                    {
                        entryEditText.TextAlignment = Android.Views.TextAlignment.Center;
                        entryEditText.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
                    }
                }
            }
        }
        #endregion
    }
}