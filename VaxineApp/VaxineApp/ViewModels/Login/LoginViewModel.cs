using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Status;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
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

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisedPropertyChanged(nameof(Email));
            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisedPropertyChanged(nameof(Password));
            }
        }


        public ICommand SignInCommand { private set; get; }
        public LoginViewModel()
        {
            auth = DependencyService.Get<IAuth>();
            SignInCommand = new Command(SignIn);
            ForgotPasswordCommand = new Command(ForgotPassword);
            CloseAppCommand = new Command(CloseApp);
        }
        private void CloseApp()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        private async void SignIn(object sender)
        {
            string Token = await auth.LoginWithEmailPassword(Email, Password);
            if (Token != "")
            {
                Preferences.Set("Email", Email);
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
