using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Status;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Login
{
    public partial class LoginViewModel : BaseViewModel
    {
        IAuth auth;
        public ICommand CloseAppCommand { private set; get; }
        public ICommand ForgotPasswordCommand { private set; get; }
        private bool _rememberMe;

        public bool RememberMe
        {
            get { return _rememberMe; }
            set
            {
                _rememberMe = value;
                RaisedPropertyChanged(nameof(RememberMe));
            }
        }

        private string _userEmail;

        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                RaisedPropertyChanged(nameof(UserEmail));
            }
        }
        private string _userPassword;

        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                RaisedPropertyChanged(nameof(UserPassword));
            }
        }


        public ICommand SignInCommand { private set; get; }
        public LoginViewModel()
        {
            auth = DependencyService.Get<IAuth>();
            SignInCommand = new Command(SignIn);
            ForgotPasswordCommand = new Command(ForgotPassword);
            CloseAppCommand = new Command(CloseApp);
            SaveDataCommand = new Command(SaveData);
            EditProfileCommand = new Command(EditProfile);
            
        }
        private void CloseApp()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private async void SignIn(object sender)
        {
            string Token = await auth.LoginWithEmailPassword(UserEmail, UserPassword);
            if (Token != "")
            {
                Preferences.Set("Email", UserEmail);
                GetProfile();
                await Shell.Current.GoToAsync($"//{nameof(StatusPage)}");
            }
            else
            {
                ShowError();
            }
            await Shell.Current.GoToAsync($"//{nameof(StatusPage)}");


        }
        async private void ShowError()
        {
            await Application.Current.MainPage.DisplayAlert("Authentication failed!", "E-mail or password are incorrect. Try again!", "Cancel");
        }
        async private void ForgotPassword()
        {

        }
    }
}
