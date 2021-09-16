using Android.Content.PM;
using System;
using VaxineApp.AndroidNativeApi;

[assembly: Xamarin.Forms.Dependency(typeof(VaxineApp.Droid.NativeApi.AndroidAppVersion))]
namespace VaxineApp.Droid.NativeApi
{
    public class AndroidAppVersion : IAppVersion
    {
        public string GetVersion()
        {
            var context = global::Android.App.Application.Context;

            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        [Obsolete]
        public int GetBuild()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionCode;
        }
    }
}