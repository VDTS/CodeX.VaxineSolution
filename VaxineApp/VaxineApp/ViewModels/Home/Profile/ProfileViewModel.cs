﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.Services;
using VaxineApp.Views.Home.Profile;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        DbContext firebaseHelper = new DbContext();
        public ICommand SaveDataCommand { private set; get; }
        public ICommand EditProfileCommand { private set; get; }
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
        public ProfileModel Profile {
            get { return _profile; }
            set
            {
                _profile = value;
                RaisedPropertyChanged(nameof(Profile));
            }
        }
        public ProfileViewModel()
        {
            GetProfile();
            SaveDataCommand = new Command(SaveData);
            EditProfileCommand = new Command(EditProfile);
        }

        public async void GetProfile()
        {
            Profile = await firebaseHelper.GetProfile("Yassin@gmail.com");
            FullName = Profile.FullName;
            Gender = Profile.Gender;
            FatherOrHusbandName = Profile.FatherOrHusbandName;
            Age = Profile.Age;
            Email = Profile.Email;
            Role = Profile.Role;
        }
        async void SaveData(object obj)
        {
            try
            {
                await firebaseHelper.UpdatePerson(Profile.Email,
                    new ProfileModel
                    {
                        FullName = FullName,
                        Gender = Gender,
                        Age = Age,
                        FatherOrHusbandName = FatherOrHusbandName,
                        Email = Email,
                        Password = Password,
                        Role = Profile.Role
                    }
                    );
                var route = $"{nameof(ProfilePage)}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async void EditProfile(object obj)
        {
            var route = $"{nameof(EditProfile)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}