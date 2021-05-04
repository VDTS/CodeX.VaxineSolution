using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models.Home.Area;
using VaxineApp.Views.Home.Area.Doctor;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Doctor
{
    public class DoctorViewModel : BaseViewModel
    {

        // Properties
        private string _name;
        public string Name {
            get { return _name; }
            set
            {
                _name = value;
                RaisedPropertyChanged(nameof(Name));
            }
        }
        private bool _isHeProvindingSupportForSIAAndVaccination;
        public bool IsHeProvindingSupportForSIAAndVaccination {
            get { return _isHeProvindingSupportForSIAAndVaccination; }
            set
            {
                _isHeProvindingSupportForSIAAndVaccination = value;
                RaisedPropertyChanged(nameof(IsHeProvindingSupportForSIAAndVaccination));
            }
        }
        public ICommand AddDoctorCommand { private set; get; }
        public ICommand SaveDoctorCommand { private set; get; }

        // Constructor
        public DoctorViewModel()
        {
            AddDoctorCommand = new Command(AddDoctor);
            SaveDoctorCommand = new Command(SaveDoctor);
        }


        // Route Methods
        async void AddDoctor()
        {
            var route = $"{nameof(AditDoctorPage)}";
            await Shell.Current.GoToAsync(route);
        }


        // Methods
        public async void SaveDoctor()
        {
            await Data.PostDoctor(new DoctorModel
            {
                Name = Name,
                IsHeProvindingSupportForSIAAndVaccination = IsHeProvindingSupportForSIAAndVaccination
            }, "T");

            var route = $"{nameof(DoctorPage)}";
            await Shell.Current.GoToAsync(route);
        }

    }
}
