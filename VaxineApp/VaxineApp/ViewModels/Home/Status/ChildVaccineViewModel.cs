using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Status;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Status
{
    public class ChildVaccineViewModel : BaseViewModel
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

        public ICommand AddVaccineCommand { private set; get; }
        public ChildVaccineViewModel()
        {
            AddVaccineCommand = new Command(Add);
        }

        public async void Add()
        {
            var route = $"{nameof(AddVaccinePage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
