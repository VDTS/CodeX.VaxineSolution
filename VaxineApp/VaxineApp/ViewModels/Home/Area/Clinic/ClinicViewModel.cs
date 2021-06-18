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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class ClinicViewModel : BaseViewModel
    {
        // Properties
        private bool _isToolbarIconsVisible;

        public bool IsToolbarIconsVisible
        {
            get { return _isToolbarIconsVisible; }
            set
            {
                _isToolbarIconsVisible = value;
                RaisedPropertyChanged(nameof(IsToolbarIconsVisible));
            }
        }
        private string _isSearchVisible;

        public string IsSearchVisible
        {
            get { return _isSearchVisible; }
            set
            {
                _isSearchVisible = value;
                RaisedPropertyChanged(nameof(IsSearchVisible));
            }
        }

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
        public ICommand DeleteCommand { private set; get; }
        public ICommand GoToMapCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand EditClinicCommand { private set; get; }
        public ICommand ClinicSelectionChangedCommand { private set; get; }

        // Constructor
        public ClinicViewModel()
        {
            DeleteCommand = new Command(Delete);
            GoToMapCommand = new Command(GoToMap);
            SaveAsPDFCommand = new Command(SaveAsPDF);
            SelectedClinic = new ClinicModel();
            EditClinicCommand = new Command(EditClinic);
            GetClinic();
            GetClinicCommand = new AsyncCommand(Refresh);
            AddClinicCommand = new Command(AddClinic);
            ClinicSelectionChangedCommand = new Command(ClinicSelectionChanged);
            Clinics = new List<ClinicModel>();
        }

        private void ClinicSelectionChanged(object obj)
        {
            if(SelectedClinic.ClinicName != null)
            {
                IsSearchVisible = "Hidden";
                IsToolbarIconsVisible = true;
            }
            else
            {
                IsSearchVisible = "Expanded";
                IsToolbarIconsVisible = false;
            }
        }

        private async void EditClinic()
        {
            if(SelectedClinic.ClinicName != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedClinic);
                var route = $"{nameof(EditClinicPage)}?Clinic={jsonClinic}";
                await Shell.Current.GoToAsync(route);
                SelectedClinic = null;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No Clinic", "Select a clinic", "OK");
            }
        }

        private async void SaveAsPDF(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        private async void GoToMap(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }
        private async void Delete(object obj)
        {
            if(SelectedClinic.ClinicName != null)
            {
                var data = await DataService.Delete($"Clinic/{Preferences.Get("TeamId", "")}/{SelectedClinic.FId}");
                if(data == "Deleted")
                {
                    await App.Current.MainPage.DisplayAlert("Deleted", $"item has been deleted", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Not Deleted", "Try again", "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No item selected", "Select an item to delete", "OK");
            }
        }


        // Methods
        public async void GetClinic()
        {
            var data = await DataService.Get($"Clinic/{Preferences.Get("TeamId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, ClinicModel>>(data);
                foreach (KeyValuePair<string, ClinicModel> item in clinic)
                {
                    Clinics.Add(
                            new ClinicModel
                            {
                                FId = item.Key,
                                ClinicName = item.Value.ClinicName,
                                Fixed = item.Value.Fixed,
                                Outreach = item.Value.Outreach
                            }
                        );
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No data found!", "Add some data to show here", "OK");
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
