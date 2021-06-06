﻿using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class AddClinicViewModel : BaseViewModel
    {
        private string _clinicName;
        public string ClinicName
        {
            get { return _clinicName; }
            set
            {
                _clinicName = value;
                RaisedPropertyChanged(nameof(ClinicName));
            }
        }
        public string _fixed;
        public string Fixed
        {
            get { return _fixed; }
            set
            {
                _fixed = value;
                RaisedPropertyChanged(nameof(Fixed));
            }
        }
        public string _outreach;
        public string Outreach
        {
            get { return _outreach; }
            set
            {
                _outreach = value;
                RaisedPropertyChanged(nameof(Outreach));
            }
        }
        public double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                RaisedPropertyChanged(nameof(Latitude));
            }
        }
        public double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                RaisedPropertyChanged(nameof(Longitude));
            }
        }
        public ICommand SaveClinicCommand { private set; get; }
        public AddClinicViewModel()
        {
            SaveClinicCommand = new Command(SaveClinic);

        }
        public async void SaveClinic()
        {
            ClinicModel clinic = new ClinicModel()
            {
                Id = Guid.NewGuid(),
                ClinicName = ClinicName,
                Fixed = Fixed,
                Outreach = Outreach,
                Latitude = Latitude,
                Longitude = Longitude
            };

            var data = JsonConvert.SerializeObject(clinic);

            string a = DataService.Post(data, "Clinic/c0cda6a9-759a-4e87-b8cb-49af170bd24e");
            await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            var route = $"//{nameof(ClinicPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
