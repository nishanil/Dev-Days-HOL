using MyEvents;
using MyEvents.UWP.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(SpeakButton), typeof(SpeakButtonRenderer))]
namespace MyEvents.UWP.Renderers
{
    public class SpeakButtonRenderer : ButtonRenderer
    {
        //Add UWP Sepcific custoization here
    }
}
