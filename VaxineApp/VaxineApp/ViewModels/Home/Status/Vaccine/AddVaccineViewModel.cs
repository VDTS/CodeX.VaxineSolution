﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using Xamarin.Forms;
using VaxineApp.Views.Home.Status;
using VaxineApp.Views.Home.Family;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace VaxineApp.ViewModels.Home.Status.Vaccine
{
    public class AddVaccineViewModel : BaseViewModel
    {
        private DateTime _vaccinePeriod;
        public DateTime VaccinePeriod
        {
            get { return _vaccinePeriod; }
            set
            {
                _vaccinePeriod = value;
                RaisedPropertyChanged(nameof(VaccinePeriod));
            }
        }

        private string _vaccineStatus;
        public string VaccineStatus
        {
            get { return _vaccineStatus; }
            set
            {
                _vaccineStatus = value;
                RaisedPropertyChanged(nameof(VaccineStatus));
            }
        }

        public ICommand AddVaccineCommand { private set; get; }
        ChildModel Child;
        public AddVaccineViewModel(ChildModel _child)
        {
            Child = _child;
            AddVaccineCommand = new Command(AddVaccine);
        }

        private async void AddVaccine(object obj)
        {
            VaccineModel clinic = new VaccineModel()
            {
                Id = Guid.NewGuid(),
                VaccinePeriod = VaccinePeriod.Date.ToUniversalTime(),
                VaccineStatus = VaccineStatus,
                RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""))
            };

            var data = JsonConvert.SerializeObject(clinic);

            string a = DataService.Post(data, $"Vaccine/{Child.Id}");
            await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            var route = $"//{nameof(StatusPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}