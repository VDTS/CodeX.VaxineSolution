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

        private ChildModel _child;

        public ChildModel Child
        {
            get { return _child; }
            set
            {
                _child = value;
                RaisedPropertyChanged(nameof(Child));
            }
        }


        public ICommand AddVaccineCommand { private set; get; }
        public ChildVaccineViewModel(ChildModel child)
        {
            Child = child;
            AddVaccineCommand = new Command(Add);
        }

        public async void Add()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddVaccinePage(Child.HouseNo));
        }
    }
}
