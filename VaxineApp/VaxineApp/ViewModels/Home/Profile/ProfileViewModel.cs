using DataAccessLib;
using DataAccessLib.Databases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Profile;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        #region Preferences
        public string PrefUserEmail = Preferences.Get("PrefEmail", "");
        #endregion

        #region Commands

        public ICommand EditProfileCommand { private set; get; }

        #endregion

        #region Properties

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                RaisedPropertyChanged(nameof(FullName));
            }
        }
        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                RaisedPropertyChanged(nameof(Gender));
            }
        }
        private string _fatherOrHusbandName;
        public string FatherOrHusbandName
        {
            get { return _fatherOrHusbandName; }
            set
            {
                _fatherOrHusbandName = value;
                RaisedPropertyChanged(nameof(FatherOrHusbandName));
            }
        }
        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisedPropertyChanged(nameof(Age));
            }
        }

        private string _role;
        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                RaisedPropertyChanged(nameof(Role));
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

        private string _confirmEmail;
        public string ConfirmEmail
        {
            get { return _confirmEmail; }
            set
            {
                _confirmEmail = value;
                RaisedPropertyChanged(nameof(ConfirmEmail));
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
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                RaisedPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private ProfileModel _profile;
        public ProfileModel Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                RaisedPropertyChanged(nameof(Profile));
            }
        }
        #endregion

        public ProfileViewModel()
        {
            EditProfileCommand = new Command(EditProfile);
            GetProfile();
        }

        #region Methods

        public async void GetProfile()
        {
            SqliteDataService sqliteDataService = new SqliteDataService();
            sqliteDataService.Initialize(Preferences.Get("ProfileEmail", ""));
            var profileValue = sqliteDataService.Get("Profile");
            var profile = JsonConvert.DeserializeObject<ProfileModel>(profileValue);

            Profile = new ProfileModel
            {
                FullName = profile.FullName,
                Age = profile.Age,
                Email = profile.Email,
                FatherOrHusbandName = profile.FatherOrHusbandName,
                Gender = profile.Gender,
                Role = profile.Role,
                TeamId = profile.TeamId,
                ClusterId = profile.ClusterId
            };

        }

        #endregion

        #region RouteMethods
        public async void EditProfile(object obj)
        {
            var ProfileJson = JsonConvert.SerializeObject(Profile);
            var route = $"{nameof(EditProfilePage)}?Profile={ProfileJson}";
            await Shell.Current.GoToAsync(route);
        }

        #endregion

    }
}
