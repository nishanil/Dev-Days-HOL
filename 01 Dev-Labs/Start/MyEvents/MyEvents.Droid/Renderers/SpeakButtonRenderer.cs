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
using MyEvents;
using MyEvents.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SpeakButton), typeof(SpeakButtonRenderer))]
namespace MyEvents.Droid.Renderers
{
    public class SpeakButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetCompoundDrawablesWithIntrinsicBounds(0, Resource.Drawable.speakerphone, 0, 0);
            }
        }
    }
}