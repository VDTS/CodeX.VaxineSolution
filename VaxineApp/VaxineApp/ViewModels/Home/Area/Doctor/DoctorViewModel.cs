using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Area.Doctor;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Doctor
{
    public class DoctorViewModel : BaseViewModel
    {
        public ICommand AddDoctorCommand { private set; get; }
        public DoctorViewModel()
        {
            AddDoctorCommand = new Command(AddDoctor);
        }
        async void AddDoctor()
        {
            var route = $"{nameof(AditDoctorPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
