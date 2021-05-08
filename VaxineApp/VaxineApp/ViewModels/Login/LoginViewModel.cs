﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Status;
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
<<<<<<< HEAD
            System.Diagnostics.Process.GetCurrentProcess().Kill();
=======
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
>>>>>>> c36ad67de379a8f3309b5785322f2d27216cd294
        }
        private async void SignIn(object sender)
        {
            string Token = await auth.LoginWithEmailPassword(Email, Password);
            if (Token != "")
            {
                await Shell.Current.GoToAsync($"//{nameof(StatusPage)}");
            }
            else
            {
                ShowError();
            }

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