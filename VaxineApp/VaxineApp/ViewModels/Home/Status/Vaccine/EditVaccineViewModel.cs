using System;
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
    public class EditVaccineViewModel : BaseViewModel
    {
       
        private VaccineModel _vaccine;
        public VaccineModel Vaccine
        {
            get { return _vaccine; }
            set
            {
                _vaccine = value;
                RaisedPropertyChanged(nameof(Vaccine));
            }
        }

        public ICommand UpdateVaccineCommand { private set; get; }
        public EditVaccineViewModel(VaccineModel vaccine)
        {
            Vaccine = vaccine;
            UpdateVaccineCommand = new Command(UpdateVaccine);
        }

        private async void UpdateVaccine(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }
    }
}
