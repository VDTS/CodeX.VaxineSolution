using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Profile;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Profile
{
    public class EditProfileViewModel : BaseViewModel
    {
        public ProfileModel Profile { get; set; }
        public ICommand SaveDataCommand { private set; get; }
        public EditProfileViewModel(ProfileModel _profile)
        {
            Profile = _profile;
            SaveDataCommand = new Command(SaveData);
        }

        async void SaveData(object obj)
        {
            //var data = JsonConvert.SerializeObject(Profile);

            //string a = DataService.Post(data, "Profile");
            //await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            //var route = $"//{nameof(ProfilePage)}";
            //await Shell.Current.GoToAsync(route);
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");

        }
    }
}
