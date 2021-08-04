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