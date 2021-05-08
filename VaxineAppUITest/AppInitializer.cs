using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace VaxineAppUITest
{
    public class AppInitializer
    {
        // The Commented StartApp method was default method, and it doesn't work. 
        // This stackoverflow solution suggested the below StartApp method.
        // https://stackoverflow.com/questions/51296837/xamarin-uitesting-nu1201-error-android-8-1-is-incompatible-with-netframework-4
        //public static IApp StartApp(Platform platform)
        //{
        //    if (platform == Platform.Android)
        //    {
        //        return ConfigureApp.Android.StartApp();
        //    }

        //    return ConfigureApp.iOS.StartApp();
        //}
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("com.codex.vaxineapp").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}