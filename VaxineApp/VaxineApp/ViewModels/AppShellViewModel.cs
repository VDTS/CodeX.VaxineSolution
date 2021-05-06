using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Profile;
using VaxineApp.Views.Login;
using VaxineApp.Views.Settings;
using Xamarin.Forms;

namespace VaxineApp.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        public ICommand GoToProfileCommand { private set; get; }
        public ICommand GoToSettingsPageCommand { private set; get; }
        public ICommand LogginOutCommand { private set; get; }
        public AppShellViewModel()
        {
            GoToSettingsPageCommand = new Command(GoToSettingsPage);
            GoToProfileCommand = new Command(GoToProfile);
            LogginOutCommand = new Command(LogginOut);
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
    }
}
