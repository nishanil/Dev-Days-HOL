using MyEvents;
using MyEvents.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SpeakButton), typeof(SpeakButtonRenderer))]
namespace MyEvents.iOS.Renderers
{
    public class SpeakButtonRenderer : ButtonRenderer
    {
        // Add iOS specific customization here
    }
}