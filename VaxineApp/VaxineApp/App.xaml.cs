using System;
using VaxineApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using VaxineApp.Views.Login;
using Microsoft.AppCenter.Distribute;
using System.Threading.Tasks;

namespace VaxineApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            var isLoogged = Xamarin.Essentials.SecureStorage.GetAsync("isLogged").Result;
            if (isLoogged == "1")
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=518d8341-3aa8-467f-ae69-fdae9b224b1b;", typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
