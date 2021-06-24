using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using VaxineApp.AndroidNativeApi;
using VaxineApp.Views.Home.Profile;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;
using System.Threading.Tasks;
using VaxineApp.MVVMHelper;
using DataAccessLib.Account;
using VaxineApp.AccessShellDir.Views.Login;

namespace VaxineApp.ViewModels.Home.Profile
{
    public class EditProfileViewModel : ViewModelBase
    {
        // Profile
        public ProfileModel Profile { get; set; }

        private string currentPassword;
        public string CurrentPassword
        {
            get
            {
                return currentPassword;
            }
            set
            {
                currentPassword = value;
                OnPropertyChanged();
            }
        }

        private string newPassword;
        public string NewPassword
        {
            get
            {
                return newPassword;
            }
            set
            {
                newPassword = value;
                OnPropertyChanged();
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        AccountManagement Account { get; set; }

        // Command
        public ICommand SaveDataCommand { private set; get; }
        public ICommand BrowsePhotoCommad { private set; get; }
        public ICommand ChangePasswordCommand { private set; get; }

        // ctor
        public EditProfileViewModel(ProfileModel _profile)
        {
            // Property
            Profile = _profile;
            Account = new AccountManagement();

            // Command
            SaveDataCommand = new Command(SaveData);
            ChangePasswordCommand = new Command(ChangePassword);
            BrowsePhotoCommad = new Command(BrowsePhoto);
        }

        private async void ChangePassword()
        {
            if (NewPassword != null && ConfirmPassword != null && CurrentPassword != null)
            {
                if (NewPassword == ConfirmPassword)
                {
                    if (NewPassword.Length >= 8)
                    {
                        string Token = await Account.SignIn(Preferences.Get("ProfileEmail", "").ToString(), CurrentPassword);
                        if (Token != "Error" && Token != "null")
                        {
                            var message = await Account.ChangeAccountPassword(Token, NewPassword);
                            if (message == "OK")
                            {
                                await App.Current.MainPage.DisplayAlert("", "Password Changed!", "OK");
                                await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "0");
                                Application.Current.MainPage = new AccessShell();
                                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Error!", "Try again!", "OK");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Old Password Error", "Make sure you typed the old password correctly!", "OK");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("New Password Error", "Password must not be less then 8 letters, and should contain letters, numbers and symbols", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Passwords are not the same", "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fill in required fields", "Current, new and Confirm Passwords are required", "OK");
            }
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
            if (action == "Gallery")
            {
                OpenFileBrowser();
            }
            else if (action == "Camera")
            {
                await TakePhotoAsync();
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

        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                await App.Current.MainPage.DisplayAlert("Not submitted!", "Taking photo functionality is under construction", "OK");

                //Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is now supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                //PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            //PhotoPath = newFile;
        }
    }
}
