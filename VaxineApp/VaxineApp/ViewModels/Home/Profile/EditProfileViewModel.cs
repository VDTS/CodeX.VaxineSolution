﻿using Newtonsoft.Json;
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
using System.Text.RegularExpressions;
using VaxineApp.Models;

namespace VaxineApp.ViewModels.Home.Profile
{
    public class EditProfileViewModel : ViewModelBase
    {
        // Property
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

        private string currentEmail;
        public string CurrentEmail
        {
            get
            {
                return currentEmail;
            }
            set
            {
                currentEmail = value;
                OnPropertyChanged();
            }
        }

        private string newEmail;
        public string NewEmail
        {
            get
            {
                return newEmail;
            }
            set
            {
                newEmail = value;
                OnPropertyChanged();
            }
        }

        private string confirmEmail;
        public string ConfirmEmail
        {
            get
            {
                return confirmEmail;
            }
            set
            {
                confirmEmail = value;
                OnPropertyChanged();
            }
        }


        // Command
        public ICommand PutCommand { private set; get; }
        public ICommand BrowsePhotoCommad { private set; get; }
        public ICommand ChangePasswordCommand { private set; get; }
        public ICommand ChangeEmailCommand { private set; get; }

        // ctor
        public EditProfileViewModel(ProfileModel _profile)
        {
            // Property
            Profile = _profile;
            Account = new AccountManagement();

            // Command
            PutCommand = new Command(Put);
            ChangePasswordCommand = new Command(ChangePassword);
            BrowsePhotoCommad = new Command(BrowsePhoto);
            ChangeEmailCommand = new Command(ChangeEmail);
        }

        private async void ChangeEmail()
        {
            if (!string.IsNullOrEmpty(CurrentEmail) && !string.IsNullOrEmpty(NewEmail) && !string.IsNullOrEmpty(ConfirmEmail))
            {
                if (NewEmail == ConfirmEmail)
                {
                    string emailRegex = @"^([\w\. \-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                    bool isMatched = Regex.IsMatch(NewEmail, emailRegex);
                    if (isMatched)
                    {
                        string result = await App.Current.MainPage.DisplayPromptAsync("Enter your Password", "You are entering sudo mode.");
                        string Token = await Account.SignIn(Preferences.Get("ProfileEmail", "").ToString(), result);
                        if (Token != "Error" && Token != "null")
                        {
                            var message = await Account.ChangeEmail(NewEmail, Token);
                            if (message == "OK")
                            {
                                await App.Current.MainPage.DisplayAlert("", "Email Changed!", "OK");
                                await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "0");

                                string s1 = await Account.VerifyEmail(Token);
                                if(s1 == "OK")
                                {
                                    await App.Current.MainPage.DisplayAlert("Email Verification send", "Go to your email, and confirm verification", "OK");
                                }

                                // Change Email in profile

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
                            await App.Current.MainPage.DisplayAlert("Password Error", "Make sure you typed password correctly!", "OK");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("New Email Error", "Email is baddly formatted", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Emails are not the same", "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fill in required fields", "Current, new and Confirm Emails are required", "OK");
            }
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

        async void Put(object obj)
        {
            var jsonData = JsonConvert.SerializeObject(Profile);
            var data = await DataService.Put(jsonData, $"Profile/{Profile.FId}");
            if (data == "Submit")
            {
                await App.Current.MainPage.DisplayAlert("Updated", $"item has been updated", "OK");
                var route = $"//{nameof(ProfilePage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Not Updated", "Try again", "OK");
            }

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
