using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using Xamarin.Forms;
using VaxineApp.Views.Home.Status;
using VaxineApp.Views.Home.Family;

namespace VaxineApp.ViewModels.Home.Status
{
    public class AddVaccineViewModel : BaseViewModel
    {
        private DateTime _vaccinePeriod;
        public DateTime VaccinePeriod
        {
            get { return _vaccinePeriod; }
            set
            {
                _vaccinePeriod = value;
                RaisedPropertyChanged(nameof(VaccinePeriod));
            }
        }

        private string _vaccineStatus;
        public string VaccineStatus
        {
            get { return _vaccineStatus; }
            set
            {
                _vaccineStatus = value;
                RaisedPropertyChanged(nameof(VaccineStatus));
            }
        }

        public ICommand AddVaccineCommand { private set; get; }
        int HouseNo;
        public AddVaccineViewModel(int houseNo)
        {
            HouseNo = houseNo;
            AddVaccineCommand = new Command(AddVaccine);
        }

        private async void AddVaccine(object obj)
        {
            await Data.PostVaccine(HouseNo ,new VaccineModel
            {
                VaccinePeriod = VaccinePeriod,
                VaccineStatus = VaccineStatus
            });
            await App.Current.MainPage.Navigation.PushAsync(new ChildVaccinePage(new ChildModel { HouseNo = HouseNo }));
        }
    }
}
