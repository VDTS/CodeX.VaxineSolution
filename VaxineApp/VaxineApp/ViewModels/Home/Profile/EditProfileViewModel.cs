using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using VaxineApp.AndroidNativeApi;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Profile;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;

namespace VaxineApp.ViewModels.Home.Profile
{
    public class EditProfileViewModel : BaseViewModel
    {
        public ProfileModel Profile { get; set; }
        public ICommand SaveDataCommand { private set; get; }
        public ICommand BrowsePhotoCommad { private set; get; }
        public EditProfileViewModel(ProfileModel _profile)
        {
            Profile = _profile;
            SaveDataCommand = new Command(SaveData);
            BrowsePhotoCommad = new Command(BrowsePhoto);
        }

        async void SaveData(object obj)
        {
            //var data = JsonConvert.SerializeObject(Profile);

            //string a = DataService.Post(data, "Profile");
            //await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            //var route = $"//{nameof(ProfilePage)}";
            //await Shell.Current.GoToAsync(route);
            await App.Current.MainPage.DisplayAlert("Not submitted!", "The Gallery functionality is under construction", "OK");

        }
        async void BrowsePhoto(object sender)
        {
            var action = await App.Current.MainPage.DisplayActionSheet("Open photo", "Cancel", null, "Gallery", "Camera", "Remove");
            Debug.WriteLine("Action: " + action);
            if(action == "Gallery")
            {
                OpenFileBrowser();
            }else if(action == "Camera")
            {
                await App.Current.MainPage.DisplayAlert("Not submitted!", "The camera functionality is under construction", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Not submitted!", "Remove photo functionality is under construction", "OK");
            }
        }
        public async void OpenFileBrowser()
        {
            //(sender as Button).IsEnabled = false;
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
                //image.Source = ImageSource.FromStream(() => stream);
            }
            //(sender as Button).IsEnabled = true;
        }
    }
}
