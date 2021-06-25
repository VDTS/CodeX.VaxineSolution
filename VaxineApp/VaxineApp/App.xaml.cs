using System;
using VaxineApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using System.Threading.Tasks;
using VaxineApp.AndroidNativeApi;

namespace VaxineApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            // Syncfusion License
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Constants.SyncFusionCommunityLicenseKey);

            DependencyService.Get<INotificationManager>().Initialize();

            var isLoogged = Xamarin.Essentials.SecureStorage.GetAsync("isLogged").Result;
            if (isLoogged == "1")
            {
                var role = Xamarin.Essentials.SecureStorage.GetAsync("Role").Result;
                if (role == "Mobilizer")
                {
                    MainPage = new AppShell();
                }
                else if(role == "Supervisor")
                {
                    MainPage = new SupAppShell();
                }
                else if(role == "Parent")
                {
                    MainPage = new ParentAppShell();
                }
                else
                {
                    MainPage = new AccessShell();
                }
            }
            else
            {
                MainPage = new AccessShell();
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=518d8341-3aa8-467f-ae69-fdae9b224b1b;" + "ios=a35581bf-3a81-47d7-b871-0dd9a46a57c5;", typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
