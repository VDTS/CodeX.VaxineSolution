using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.AboutUs;
using VaxineApp.Views.Feedback;
using VaxineApp.Views.PrivacyPolicy;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Settings
{
    public class SettingsViewModel : BaseViewModel
    {
        public ICommand PrivacyPolicyCommand { private set; get; }
        public ICommand FeedbackPageCommand { private set; get; }
        public ICommand GoToAboutUsPageCommand { private set; get; }

        public SettingsViewModel()
        {
            FeedbackPageCommand = new Command(GoToFeedbackPage);
            PrivacyPolicyCommand = new Command(PrivacyPolicy);
            GoToAboutUsPageCommand = new Command(GoToAboutUsPage);

        }
        private async void GoToFeedbackPage(object obj)
        {
            var route = $"{nameof(FeedbackPage)}";
            await Shell.Current.GoToAsync(route);
        }
        private async void PrivacyPolicy(object obj)
        {
            var route = $"{nameof(PrivacyPolicyPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void GoToAboutUsPage(object obj)
        {
            var route = $"//{nameof(AboutUsPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
