using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home;
using VaxineApp.Views.Home.Profile;
using Xamarin.Forms;

namespace VaxineApp.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        public ICommand GoToProfileCommand { private set; get; }

        public AppViewModel()
        {
            GoToProfileCommand = new Command(GoToProfile);
        }

        private async void GoToProfile(object obj)
        {
            var route = $"{nameof(ProfilePage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
