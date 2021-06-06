using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Doctor;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Doctor
{
    public class AddDoctorViewModel : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisedPropertyChanged(nameof(Name));
            }
        }
        private bool _isHeProvindingSupportForSIAAndVaccination;
        public bool IsHeProvindingSupportForSIAAndVaccination
        {
            get { return _isHeProvindingSupportForSIAAndVaccination; }
            set
            {
                _isHeProvindingSupportForSIAAndVaccination = value;
                RaisedPropertyChanged(nameof(IsHeProvindingSupportForSIAAndVaccination));
            }
        }

        public ICommand SaveDoctorCommand { private set; get; }
        public AddDoctorViewModel()
        {
            SaveDoctorCommand = new Command(SaveDoctor);
        }
        public async void SaveDoctor()
        {
            DoctorModel clinic = new DoctorModel()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                IsHeProvindingSupportForSIAAndVaccination = IsHeProvindingSupportForSIAAndVaccination
            };

            var data = JsonConvert.SerializeObject(clinic);

            string a = DataService.Post(data, "Doctor/c0cda6a9-759a-4e87-b8cb-49af170bd24e");
            await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            var route = $"//{nameof(DoctorPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
