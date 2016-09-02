using CoreGraphics;
using MyEvents.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Editor), typeof(EditorWithBorderRenderer))]

namespace MyEvents.iOS.Renderers
{
    public class EditorWithBorderRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Layer.BorderWidth = 0.5f;
                Control.Layer.BorderColor = UIColor.Gray.CGColor;
                Control.Layer.CornerRadius = 8f;
            }
        }
    }
}