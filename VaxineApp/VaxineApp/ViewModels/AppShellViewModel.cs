using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.AboutUs;
using VaxineApp.Views.Help;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Profile;
using VaxineApp.Views.Login;
using VaxineApp.Views.Settings;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public ICommand GoToProfileCommand { private set; get; }
        public ICommand GoToSettingsPageCommand { private set; get; }
        public ICommand LogginOutCommand { private set; get; }
        public ICommand GoToHelpPageCommand { private set; get; }
        public ICommand GoToAboutUsPageCommand { private set; get; }
        private string _userName;
        public string UserName {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisedPropertyChanged(nameof(UserName));
            }
        }
        private string _role;

        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                RaisedPropertyChanged(nameof(Role));
            }
        }

        public AppShellViewModel()
        {
            GoToSettingsPageCommand = new Command(GoToSettingsPage);
            GoToProfileCommand = new Command(GoToProfile);
            LogginOutCommand = new Command(LogginOut);
            GoToHelpPageCommand = new Command(GoToHelpPage);
            GoToAboutUsPageCommand = new Command(GoToAboutUsPage);
            UserName = Preferences.Get("FullName", "");
            Role = Preferences.Get("Role", "");
        }

        private async void GoToProfile(object obj)
        {
            var route = $"{nameof(ProfilePage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void GoToSettingsPage(object obj)
        {
            var route = $"{nameof(Settings)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void LogginOut(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async void GoToHelpPage(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(HelpPage)}");
        }

        private async void GoToAboutUsPage(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(AboutUsPage)}");
        }
    }
}
