using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.AdminShell.Views.Home.User.UserClaims;
using VaxineApp.Core.Models.AccountModels;
using VaxineApp.Core.Models.Enums;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Forms;

namespace VaxineApp.AdminShell.ViewModels.Home.User.UserClaims
{
    public class AddUserRoleViewModel : ViewModelBase
    {
        private string role;
        public string Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
                OnPropertyChanged();
            }
        }

        private string fullName;

        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
                OnPropertyChanged();
            }
        }

        private string email;

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddRoleAndNextCommand { private set; get; }
        public AddUserRoleViewModel(string fullName, string email, string phoneNumber)
        {
            this.FullName = fullName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;


            AddRoleAndNextCommand = new Command(AddRoleAndNext);
        }

        private async void AddRoleAndNext(object obj)
        {
            string route = $"{nameof(AddUserClaimsPage)}?FullName={FullName}&Email={Email}&PhoneNumber={PhoneNumber}&Role={Role}";

            await Shell.Current.GoToAsync(route);
        }
    }
}
