using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VaxineApp.AndroidNativeApi;
using VaxineApp.Droid.NativeApi;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly:Dependency(typeof(Toaster))]
namespace VaxineApp.Droid.NativeApi
{
    public class Toaster : IToast
    {
        public void MakeToast(string message)
        {
            Toast.MakeText(Platform.AppContext, message, ToastLength.Long).Show();
        }
    }
}