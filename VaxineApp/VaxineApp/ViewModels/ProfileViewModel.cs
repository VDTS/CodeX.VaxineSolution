using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Profile;
using Xamarin.Forms;

namespace VaxineApp.ViewModels
{
    public class ProfileViewModel
    {
        public ICommand EditProfileCommand { private set; get; }
        public ProfileViewModel()
        {
            EditProfileCommand = new Command(EditProfile);
        }

        public async void EditProfile(object obj)
        {
            var route = $"{nameof(EditProfile)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
