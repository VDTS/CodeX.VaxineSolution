using System;
using System.Collections.Generic;
using System.Text;
using VaxineApp.AndroidNativeApi;
using Xamarin.Forms;
using static VaxineApp.ViewModels.Settings.AppUpdates.AppUpdatesViewModel;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace VaxineApp.AndroidNativeApi
{
    public class BaseUrl_Android : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}