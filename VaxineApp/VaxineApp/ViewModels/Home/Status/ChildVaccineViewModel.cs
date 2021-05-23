using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private VaccineModel _currentVaccine;

        public VaccineModel CurrentVaccine
        {
            get { return _currentVaccine; }
            set
            {
                _currentVaccine = value;
                RaisedPropertyChanged(nameof(CurrentVaccine));
            }
        }
        private List<VaccineModel> _vaccineList;

        public List<VaccineModel> VaccineList
        {
            get { return _vaccineList; }
            set
            {
                _vaccineList = value;
                RaisedPropertyChanged(nameof(VaccineList));
            }
        }


        public ICommand AddVaccineCommand { private set; get; }
        public ChildVaccineViewModel(ChildModel child)
        {
            VaccineList = new List<VaccineModel>();
            CurrentVaccine = new VaccineModel();
            Child = child;
            LoadVaccine();
            AddVaccineCommand = new Command(Add);
        }

        private async void LoadVaccine()
        {
            var data = await Data.GetVaccine(Child.HouseNo);
            foreach (var item in data)
            {
                VaccineList.Add(
                    new VaccineModel
                    {
                       VaccinePeriod = item.VaccinePeriod,
                       VaccineStatus = item.VaccineStatus
                    }
                    );
            }

            CurrentVaccine = VaccineList.OrderBy(x => x.VaccinePeriod).FirstOrDefault();
        }

        public async void Add()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddVaccinePage(Child.HouseNo));
        }
    }
}
