using Android.Widget;
using VaxineApp.AndroidNativeApi;
using VaxineApp.Droid.NativeApi;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(Toaster))]
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