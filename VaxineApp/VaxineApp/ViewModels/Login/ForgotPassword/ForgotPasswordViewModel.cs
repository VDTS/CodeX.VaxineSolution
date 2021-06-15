using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Login.ForgotPassword;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Login.ForgotPassword
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
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

        private bool _isNextPageVisible;
        public bool IsNextPageVisible
        {
            get { return _isNextPageVisible; }
            set
            {
                _isNextPageVisible = value;
                RaisedPropertyChanged(nameof(IsNextPageVisible));
            }
        }

        public ICommand ResetPasswordByEmailCommand { private set; get; }
        public ICommand ResetPasswordByPhoneNumberCommand { private set; get; }
        public ICommand ValidateEmailOrPhoneCommand { private set; get; }
        public ForgotPasswordViewModel()
        {
            IsNextPageVisible = false;
            ValidateEmailOrPhoneCommand = new Command(ValidateEmailOrPhone);
            ResetPasswordByEmailCommand = new Command(ResetPasswordByEmail);
            ResetPasswordByPhoneNumberCommand = new Command(ResetPasswordByPhoneNumber);
        }

        private async void ResetPasswordByPhoneNumber(object obj)
        {
            var navigationPage = new NavigationPage(new ResetByPhoneNumber());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
            
        }

        private async void ResetPasswordByEmail(object obj)
        {
            var navigationPage = new NavigationPage(new ResetByEmail());
            await App.Current.MainPage.Navigation.PushModalAsync(navigationPage, true);
        }

        private void ValidateEmailOrPhone(object obj)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                string emailRegex = @"^([\w\. \-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                bool isMatched = Regex.IsMatch(Email, emailRegex);
                if (isMatched)
                {
                    IsNextPageVisible = true;
                }
                else
                {
                    IsNextPageVisible = false;
                }
            }
        }
    }
}
