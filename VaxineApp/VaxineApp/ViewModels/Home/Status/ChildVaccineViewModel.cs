using Newtonsoft.Json;
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
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisedPropertyChanged(nameof(IsBusy));
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

        public ICommand GetDataCommand { private set; get; }
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
            var data = await DataService.Get($"Vaccine/{Child.Id}");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, VaccineModel>>(data);
            foreach (KeyValuePair<string, VaccineModel> item in clinic)
            {
                VaccineList.Add(
                    new VaccineModel
                    {
                        VaccinePeriod = item.Value.VaccinePeriod,
                        VaccineStatus = item.Value.VaccineStatus
                    }
                    );
            }
            CurrentVaccine = VaccineList.OrderBy(x => x.VaccinePeriod).LastOrDefault();
        }

        public async void Add()
        {
            var jsonChild = JsonConvert.SerializeObject(Child);
            var route = $"{nameof(AddVaccinePage)}?Child={jsonChild}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
