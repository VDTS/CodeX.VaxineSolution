﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using Xamarin.Forms;
using VaxineApp.Views.Home.Status;
using VaxineApp.Views.Home.Family;
using Newtonsoft.Json;
using Xamarin.Essentials;
using VaxineApp.MVVMHelper;

namespace VaxineApp.ViewModels.Home.Status.Vaccine
{
    public class AddVaccineViewModel : ViewModelBase
    {
        // Property
        private VaccineModel vaccine;
        public VaccineModel Vaccine
        {
            get
            {
                return vaccine;
            }
            set
            {
                vaccine = value;
                OnPropertyChanged();
            }
        }

        ChildModel Child;

        // Command
        public ICommand PostCommand { private set; get; }

        public AddVaccineViewModel(ChildModel _child)
        {
            // Property
            Child = _child;
            Vaccine = new VaccineModel();


            // Command
            PostCommand = new Command(Post);
        }

        private async void Post(object obj)
        {
            Vaccine.Id = Guid.NewGuid();
            Vaccine.RegisteredBy = Guid.Parse(Preferences.Get("UserId", ""));

            var data = JsonConvert.SerializeObject(Vaccine);

            string a = await DataService.Post(data, $"Vaccine/{Child.Id}");
            if (a == "OK")
            {
                await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(a, "Try again", "OK");
            }
            var route = $"//{nameof(StatusPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
