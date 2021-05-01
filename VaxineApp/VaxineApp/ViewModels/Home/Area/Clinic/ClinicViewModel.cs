using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class ClinicViewModel : BaseViewModel
    {
        public ICommand AddClinicCommand { private set; get; }
        public ClinicViewModel()
        {
            AddClinicCommand = new Command(AddClinic);
        }
        async void AddClinic()
        {
            var route = $"{nameof(AddClinicPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
