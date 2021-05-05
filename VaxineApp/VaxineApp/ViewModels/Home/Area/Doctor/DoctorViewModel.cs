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

        private List<DoctorModel> _doctor;
        public List<DoctorModel> Doctor
        {
            get { return _doctor; }
            set
            {
                _doctor = value;
                RaisedPropertyChanged(nameof(Doctor));
            }
        }
        // Properties
        public ICommand GetDoctorCommand { private set; get; }
        public ICommand AddDoctorCommand { private set; get; }

        // Constructor
        public DoctorViewModel()
        {
            GetDoctor();
            Doctor = new List<DoctorModel>();
            GetDoctorCommand = new Command(GetDoctor);
            AddDoctorCommand = new Command(AddDoctor);
        }


        // Route Methods
        async void AddDoctor()
        {
            var route = $"{nameof(AditDoctorPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public async void GetDoctor()
        {
            var data = await Data.GetDoctor("T");
            foreach (var item in data)
            {
                Doctor.Add(
                    new DoctorModel
                    {
                        Name = item.Name,
                        IsHeProvindingSupportForSIAAndVaccination = item.IsHeProvindingSupportForSIAAndVaccination
                    }
                    );
            }
        }
        // Methods


    }
}
