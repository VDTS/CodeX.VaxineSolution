using DataAccess.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using VaxineApp.AccessShellDir.Views.AccessAppshell;
using VaxineApp.AdminShell.Views.AdminAppShell;
using VaxineApp.AndroidNativeApi;
using VaxineApp.MobilizerShell.Views.Appshell;
using VaxineApp.ParentShellDir.Views.ParentAppshell;
using VaxineApp.Resx;
using VaxineApp.SupervisorShellDir.Views.SupervisorAppshell;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace VaxineApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            AppConfigurations();
            AppShellSelector();
        }

        protected void AppConfigurations()
        {
            DbService ds = new DbService(Constants.AppCenterAndroidXamarinKey);
            // Localization

            LocalizationResourceManager.Current.PropertyChanged += (rm, c) => AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);

            // Syncfusion License
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Constants.SyncFusionCommunityLicenseKey);

            DependencyService.Get<INotificationManager>().Initialize();

        }
        protected void AppShellSelector()
        {
            var isLoogged = Xamarin.Essentials.SecureStorage.GetAsync("isLogged").Result;
            if (isLoogged == "1")
            {
                var role = Xamarin.Essentials.SecureStorage.GetAsync("Role").Result;
                if (role == "Mobilizer")
                {
                    MainPage = new Mobilizerappshell();
                }
                else if (role == "Supervisor")
                {
                    MainPage = new SupervisorShell();
                }
                else if (role == "Parent")
                {
                    MainPage = new ParentShell();
                }
                else if (role == "Admin")
                {
                    MainPage = new AdminAppShell();
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
            AppCenter.Start($"android={Constants.AppCenterAndroidXamarinKey};" + $"ios={Constants.AppCenteriOSXamarinKey};", typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
