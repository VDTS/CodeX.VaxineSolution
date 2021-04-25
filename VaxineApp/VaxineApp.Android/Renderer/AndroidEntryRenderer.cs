using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using VaxineApp.Droid.Renderer;
using VaxineApp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Resource;

[assembly: ExportRenderer(typeof(CustomEntryRenderer), typeof(AndroidEntryRenderer))]
namespace VaxineApp.Droid.Renderer
{
    public class AndroidEntryRenderer : EntryRenderer
    {
        public AndroidEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(10);
                gd.SetStroke(2, Android.Graphics.Color.LightGray);
                
                this.Control.SetBackgroundDrawable(gd);
            }
        }
    }
}



