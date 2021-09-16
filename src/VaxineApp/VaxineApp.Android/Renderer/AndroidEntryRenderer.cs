using Android.Content;
using Android.Graphics.Drawables;
using System;
using VaxineApp.Droid.Renderer;
using VaxineApp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntryRenderer), typeof(AndroidEntryRenderer))]
namespace VaxineApp.Droid.Renderer
{
    public class AndroidEntryRenderer : EntryRenderer
    {
        public AndroidEntryRenderer(Context context) : base(context)
        {
        }

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
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



