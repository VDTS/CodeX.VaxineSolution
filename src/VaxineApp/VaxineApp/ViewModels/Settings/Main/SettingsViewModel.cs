using System.Windows.Input;
using VaxineApp.Views.Home.Profile;
using VaxineApp.Views.Settings.AboutUs;
using VaxineApp.Views.Settings.AppUpdates;
using VaxineApp.Views.Settings.Feedback;
using VaxineApp.Views.Settings.Font;
using VaxineApp.Views.Settings.Language;
using VaxineApp.Views.Settings.Notifications;
using VaxineApp.Views.Settings.PrivacyPolicy;
using VaxineApp.Views.Settings.Themes;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Settings.Main
{
    public class SettingsViewModel
    {
        // Command
        public ICommand PrivacyPolicyCommand { private set; get; }
        public ICommand FeedbackPageCommand { private set; get; }
        public ICommand GoToAboutUsPageCommand { private set; get; }
        public ICommand FontPageCommand { private set; get; }
        public ICommand ThemesPageCommand { private set; get; }
        public ICommand LanguagePageCommand { private set; get; }
        public ICommand NotificationPageCommand { private set; get; }
        public ICommand AppUpdatesPageCommand { private set; get; }
        public ICommand GoToProfilePageCommand { private set; get; }

        // ctor
        public SettingsViewModel()
        {
            FeedbackPageCommand = new Command(GoToFeedbackPage);
            PrivacyPolicyCommand = new Command(PrivacyPolicy);
            GoToAboutUsPageCommand = new Command(GoToAboutUsPage);
            FontPageCommand = new Command(FontPage);
            ThemesPageCommand = new Command(ThemesPage);
            LanguagePageCommand = new Command(LanguagePage);
            NotificationPageCommand = new Command(NotificationPage);
            AppUpdatesPageCommand = new Command(AppUpdatesPage);
            GoToProfilePageCommand = new Command(GoToProfilePage);
        }

        private async void GoToProfilePage(object obj)
        {
            var navigationPage = new NavigationPage(new ProfilePage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }

        private async void AppUpdatesPage(object obj)
        {
            var navigationPage = new NavigationPage(new AppUpdatesPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }

        private async void NotificationPage(object obj)
        {
            var navigationPage = new NavigationPage(new NotificationsPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }

        private async void ThemesPage(object obj)
        {
            var navigationPage = new NavigationPage(new ThemesPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }

        private async void FontPage(object obj)
        {
            var navigationPage = new NavigationPage(new FontPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }

        private async void LanguagePage(object obj)
        {
            var navigationPage = new NavigationPage(new LanguagePage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }

        private async void GoToFeedbackPage(object obj)
        {
            var route = $"{nameof(FeedbackPage)}";
            await Shell.Current.GoToAsync(route);
        }
        private async void PrivacyPolicy(object obj)
        {
            var navigationPage = new NavigationPage(new PrivacyPolicyPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }

        private async void GoToAboutUsPage(object obj)
        {
            var navigationPage = new NavigationPage(new AboutUsPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }
    }
}
