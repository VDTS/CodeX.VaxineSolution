using DataAccessLib.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using UtilityLib.Validations;
using VaxineApp.AndroidNativeApi;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Profile
{
    public class EditProfileViewModel : ViewModelBase
    {
        // Validator
        ProfileValidator ValidationRules { get; set; }
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

        Auth Account { get; set; }

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
            Account = new Auth();
            ValidationRules = new ProfileValidator();

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
                    if (EmailValidators.IsEmailValid(NewEmail))
                    {
                        string result = await App.Current.MainPage.DisplayPromptAsync("Enter your Password", "You are entering sudo mode.");
                        string jSignInResponse = await Account.SignIn(Preferences.Get("ProfileEmail", "").ToString(), result);
                        if (jSignInResponse.Contains("Error"))
                        {
                            StandardMessagesDisplay.CommonToastMessage(jSignInResponse);
                        }
                        else if (jSignInResponse == "ConnectionError")
                        {
                            StandardMessagesDisplay.NoConnectionToast();
                        }
                        else if (jSignInResponse == "ErrorTracked")
                        {
                            StandardMessagesDisplay.ErrorTracked();
                        }
                        else
                        {
                            JObject jo = JObject.Parse(jSignInResponse);
                            var Token = (string)jo.SelectToken("idToken");

                            var message = await Account.ChangeEmail(NewEmail, Token);
                            if (message == "OK")
                            {
                                StandardMessagesDisplay.EmailChanged(NewEmail);
                                await Xamarin.Essentials.SecureStorage.SetAsync("isLogged", "0");

                                string s1 = await Account.VerifyEmail(Token);
                                if (s1 == "OK")
                                {
                                    StandardMessagesDisplay.EmailVerificationSend(NewEmail);
                                }
                            }
                            else
                            {
                                StandardMessagesDisplay.CanceledDisplayMessage();
                            }
                        }
                    }
                    else
                    {
                        StandardMessagesDisplay.EmailValidator();
                    }
                }
                else
                {
                    StandardMessagesDisplay.EmailMatchValidator();
                }
            }
            else
            {
                StandardMessagesDisplay.CanceledDisplayMessage();
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
                        string jSignInResponse = await Account.SignIn(Preferences.Get("ProfileEmail", "").ToString(), CurrentPassword);
                        if (jSignInResponse.Contains("Error"))
                        {
                            StandardMessagesDisplay.CommonToastMessage(jSignInResponse);
                        }
                        else if (jSignInResponse == "ConnectionError")
                        {
                            StandardMessagesDisplay.NoConnectionToast();
                        }
                        else if (jSignInResponse == "ErrorTracked")
                        {
                            StandardMessagesDisplay.ErrorTracked();
                        }
                        else
                        {
                            JObject jo = JObject.Parse(jSignInResponse);
                            var Token = (string)jo.SelectToken("idToken");
                            var message = await Account.ChangeAccountPassword(Token, NewPassword);
                            if (message == "OK")
                            {
                                StandardMessagesDisplay.PasswordChanged();
                            }
                            else
                            {
                                StandardMessagesDisplay.CanceledDisplayMessage();
                            }
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
            var result = ValidationRules.Validate(Profile);
            if (result.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(Profile);
                var data = await DataService.Put(jsonData, $"Profile/{Preferences.Get("UserLocalId", "")}");
                if (data == "Submit")
                {
                    await App.Current.MainPage.DisplayAlert("Updated", $"item has been updated", "OK");
                    var route = "..";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Not Updated", "Try again", "OK");
                }
            }
            else
            {
                StandardMessagesDisplay.ValidationRulesViolation(result.Errors[0].PropertyName, result.Errors[0].ErrorMessage);
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
            catch (FeatureNotSupportedException)
            {
                // Feature is now supported on the device
            }
            catch (PermissionException)
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
