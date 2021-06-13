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
            Distribute.ReleaseAvailable = OnReleaseAvailable;
            Distribute.NoReleaseAvailable = OnNoReleaseAvailable;
            AppCenter.Start("android=518d8341-3aa8-467f-ae69-fdae9b224b1b;", typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }
        bool OnReleaseAvailable(ReleaseDetails releaseDetails)
        {
            // Look at releaseDetails public properties to get version information, release notes text or release notes URL
            string versionName = releaseDetails.ShortVersion;
            string versionCodeOrBuildNumber = releaseDetails.Version;
            string releaseNotes = releaseDetails.ReleaseNotes;
            Uri releaseNotesUrl = releaseDetails.ReleaseNotesUrl;

            // custom dialog
            var title = "Version " + versionName + " available!";
            Task answer;

            // On mandatory update, user can't postpone
            if (releaseDetails.MandatoryUpdate)
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install");
            }
            else
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install", "Maybe tomorrow...");
            }
            answer.ContinueWith((task) =>
            {
                // If mandatory or if answer was positive
                if (releaseDetails.MandatoryUpdate || (task as Task<bool>).Result)
                {
                    // Notify SDK that user selected update
                    Distribute.NotifyUpdateAction(UpdateAction.Update);
                }
                else
                {
                    // Notify SDK that user selected postpone (for 1 day)
                    // This method call is ignored by the SDK if the update is mandatory
                    Distribute.NotifyUpdateAction(UpdateAction.Postpone);
                }
            });

            // Return true if you're using your own dialog, false otherwise
            return true;
        }
        async void OnNoReleaseAvailable()
        {
            await App.Current.MainPage.DisplayAlert("No updates found", "Try later", "OK");
        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
