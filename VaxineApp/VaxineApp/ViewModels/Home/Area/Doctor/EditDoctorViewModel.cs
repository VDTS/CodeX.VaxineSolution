using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Doctor;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Doctor
{
    public class EditDoctorViewModel : BaseViewModel
    {

        private DoctorModel _doctor;

        public DoctorModel Doctor
        {
            get { return _doctor; }
            set
            {
                _doctor = value;
                RaisedPropertyChanged(nameof(Doctor));
            }
        }

        public ICommand UpdateDoctorCommand { private set; get; }
        public EditDoctorViewModel(DoctorModel doctor)
        {
            Doctor = doctor;
            UpdateDoctorCommand = new Command(UpdateDoctor);
        }

        private async void UpdateDoctor(object obj)
        {
            await App.Current.MainPage.DisplayAlert("No doctor", "Select a doctor", "OK");
        }
    }
}
