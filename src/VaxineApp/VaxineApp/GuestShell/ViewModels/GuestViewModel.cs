using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.AccessShellDir.Views.AccessAppshell;
using VaxineApp.AccessShellDir.Views.Login;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Help;
using VaxineApp.Views.Settings.AboutUs;
using VaxineApp.Views.Settings.AppUpdates;
using VaxineApp.Views.Settings.Feedback;
using VaxineApp.Views.Settings.PrivacyPolicy;
using VaxineApp.Views.Settings.Themes;
using Xamarin.Forms;

namespace VaxineApp.GuestShell.ViewModels
{
    public class GuestViewModel : ViewModelBase
    {
        public ICommand SignInPageCommand { private set; get; }
        public ICommand AppUpdatePageCommand { private set; get; }
        public ICommand VaccineGuidePageCommand { private set; get; }
        public ICommand AboutUsPageCommand { private set; get; }
        public ICommand PrivacyPolicyPageCommand { private set; get; }
        public ICommand FeedbackPageCommand { private set; get; }
        public ICommand ThemesPageCommand { private set; get; }
        public ICommand HelpPageCommand { private set; get; }

        public GuestViewModel()
        {
            SignInPageCommand = new Command(SignInPage);
            AppUpdatePageCommand = new Command(AppUpdate);
            VaccineGuidePageCommand = new Command(VaccineGuide);
            AboutUsPageCommand = new Command(AboutUs);
            PrivacyPolicyPageCommand = new Command(PrivacyPolicy);
            FeedbackPageCommand = new Command(Feedback);
            ThemesPageCommand = new Command(Themes);
            HelpPageCommand = new Command(Help);
        }

        private async void Help(object obj)
        {
            var route = $"{nameof(HelpPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void Themes(object obj)
        {
            var route = $"{nameof(ThemesPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void Feedback(object obj)
        {
            var route = $"{nameof(FeedbackPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void PrivacyPolicy(object obj)
        {
            var route = $"{nameof(PrivacyPolicyPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void AboutUs(object obj)
        {
            var route = $"{nameof(AboutUsPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private void VaccineGuide(object obj)
        {
           
        }

        private async void AppUpdate(object obj)
        {
            var route = $"{nameof(AppUpdatesPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void SignInPage(object obj)
        {
            Application.Current.MainPage = new AccessShell();
        }
    }
}
