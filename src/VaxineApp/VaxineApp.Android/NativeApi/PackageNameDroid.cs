using Android.App;
using VaxineApp.AndroidNativeApi;
using VaxineApp.Droid.NativeApi;

[assembly: Xamarin.Forms.Dependency(typeof(PackageNameDroid))]
namespace VaxineApp.Droid.NativeApi
{
    public class PackageNameDroid : IPackageName
    {
        public PackageNameDroid()
        {
        }

        public string PackageName
        {
            get { return Application.Context.PackageName; }
        }
    }
}