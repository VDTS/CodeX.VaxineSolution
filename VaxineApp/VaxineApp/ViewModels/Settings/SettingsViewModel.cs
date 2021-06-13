using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.AboutUs;
using VaxineApp.Views.Feedback;
using VaxineApp.Views.PrivacyPolicy;
using VaxineApp.Views.Settings.AppUpdates;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public ICommand PrivacyPolicyCommand { private set; get; }
        public ICommand FeedbackPageCommand { private set; get; }
        public ICommand GoToAboutUsPageCommand { private set; get; }
        public ICommand FontPageCommand { private set; get; }
        public ICommand ThemesPageCommand { private set; get; }
        public ICommand LanguagePageCommand { private set; get; }
        public ICommand NotificationPageCommand { private set; get; }
        public ICommand AppUpdatesPageCommand { private set; get; }
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

        }

        private async void AppUpdatesPage(object obj)
        {
            var navigationPage = new NavigationPage(new AppUpdatesPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }

        private async void NotificationPage(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "Notifications functionality is under construction", "OK");
        }

        private async void ThemesPage(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "Themes functionality is under construction", "OK");
        }

        private async void FontPage(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "Font functionality is under construction", "OK");
        }

        private async void LanguagePage(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "Language functionality is under construction", "OK");
        }

        private async void GoToFeedbackPage(object obj)
        {
            var navigationPage = new NavigationPage(new FeedbackPage());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
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
