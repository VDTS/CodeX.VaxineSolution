using DataAccessLib;
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
        public ICommand SaveDataCommand { private set; get; }
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
            //SaveDataCommand = new Command(SaveData);
            EditProfileCommand = new Command(EditProfile);
            GetProfile();
        }

        #region Methods

        public async void GetProfile()
        {
            var data = await DataService.Get($"Profile");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, ProfileModel>>(data);
            foreach (KeyValuePair<string, ProfileModel> item in clinic)
            {
                if(item.Value.Email == SharedData.Email)
                {
                    Profile = new ProfileModel
                    {
                        FullName = item.Value.FullName,
                        Age = item.Value.Age,
                        Email = item.Value.Email,
                        FatherOrHusbandName = item.Value.FatherOrHusbandName,
                        Gender = item.Value.Gender,
                        Role = item.Value.Role,
                        TeamId = item.Value.TeamId,
                        ClusterId = item.Value.ClusterId
                    };
                }
            }
        }
        //async void SaveData(object obj)
        //{
        //    try
        //    {
        //        await Data.PutProfile(
        //            new ProfileModel
        //            {
        //                FullName = FullName,
        //                Gender = Gender,
        //                Age = Age,
        //                FatherOrHusbandName = FatherOrHusbandName,
        //                Email = Email,
        //                Role = Profile.Role,
        //                Area = Profile.Area,
        //                Cluster = Profile.Cluster,
        //                Team = Profile.Team
        //            }
        //            );
        //        var route = $"//{nameof(ProfilePage)}";
        //        await Shell.Current.GoToAsync(route);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion

        #region RouteMethods
        public async void EditProfile(object obj)
        {
            var route = $"{nameof(EditProfile)}";
            await Shell.Current.GoToAsync(route);
        }

        #endregion

    }
}
