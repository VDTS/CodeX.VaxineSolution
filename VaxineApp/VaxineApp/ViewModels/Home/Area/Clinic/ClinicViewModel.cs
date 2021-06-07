using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.ViewModels.Home.Area.Area;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class ClinicViewModel : BaseViewModel
    {
        // Properties
        private List<ClinicModel> _clinics;
        public List<ClinicModel> Clinics
        {
            get { return _clinics; }
            set
            {
                _clinics = value;
                RaisedPropertyChanged(nameof(Clinics));
            }
        }
        private bool _isBusy;
        private ClinicModel _selectedClinic;

        public ClinicModel SelectedClinic
        {
            get { return _selectedClinic; }
            set
            {
                _selectedClinic = value;
                RaisedPropertyChanged(nameof(SelectedClinic));
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisedPropertyChanged(nameof(IsBusy));
            }
        }

        //Commands
        public ICommand AddClinicCommand { private set; get; }
        public ICommand GetClinicCommand { private set; get; }
        public ICommand DeleteClinicCommand { private set; get; }

        // Constructor
        public ClinicViewModel()
        {
            SelectedClinic = new ClinicModel();
            GetClinic();
            GetClinicCommand = new AsyncCommand(Refresh);
            AddClinicCommand = new Command(AddClinic);
            DeleteClinicCommand = new Command(DeleteClinic);
            Clinics = new List<ClinicModel>();
        }

        private async void DeleteClinic(object obj)
        {
            //await Data.DelClinic(SelectedClinic.ClinicName);
        }


        // Methods
        public async void GetClinic()
        {
            var data = await DataService.Get($"Clinic/c0cda6a9-759a-4e87-b8cb-49af170bd24e");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(data);
            foreach (KeyValuePair<string, ClinicModel> item in clinic)
            {
                Clinics.Add(
                        new ClinicModel
                        {
                            ClinicName = item.Value.ClinicName,
                            Fixed = item.Value.Fixed,
                            Outreach = item.Value.Outreach
                        }
                    );
            }
        }

        // GoTo Routes
        async void AddClinic()
        {
            var route = $"{nameof(AddClinicPage)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            GetClinic();

            IsBusy = false;
        }

        void Clear()
        {
            Clinics.Clear();
        }
    }
}
