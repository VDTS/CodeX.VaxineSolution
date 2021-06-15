using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Doctor;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Doctor
{
    public class DoctorViewModel : BaseViewModel
    {
        private DoctorModel _selectedDoctor;
        public DoctorModel SelectedDoctor
        {
            get { return _selectedDoctor; }
            set
            {
                _selectedDoctor = value;
                RaisedPropertyChanged(nameof(SelectedDoctor));
            }
        }

        private List<DoctorModel> _doctor;
        public List<DoctorModel> Doctor
        {
            get { return _doctor; }
            set
            {
                _doctor = value;
                RaisedPropertyChanged(nameof(Doctor));
            }
        }
        // Properties
        public AsyncCommand GetDoctorCommand { private set; get; }
        public ICommand AddDoctorCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand EditCommand { private set; get; }

        // Constructor
        public DoctorViewModel()
        {
            GetDoctor();
            SaveAsPDFCommand = new Command(SaveAsPDF);
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(EditDoctor);
            Doctor = new List<DoctorModel>();
            GetDoctorCommand = new AsyncCommand(Refresh);
            AddDoctorCommand = new Command(AddDoctor);
            SelectedDoctor = new DoctorModel();
        }

        private async void EditDoctor()
        {
            if (SelectedDoctor.Name != null)
            {
                var jsonClinic = JsonConvert.SerializeObject(SelectedDoctor);
                var route = $"{nameof(EditDoctorPage)}?Doctor={jsonClinic}";
                await Shell.Current.GoToAsync(route);
                SelectedDoctor = null;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No Doctor", "Select a doctor", "OK");
            }
        }

        private async void Delete(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        private async void SaveAsPDF(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }


        // Route Methods
        async void AddDoctor()
        {
            var route = $"{nameof(AddDoctorPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public async void GetDoctor()
        {
            var data = await DataService.Get($"Doctor/{Preferences.Get("TeamId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, DoctorModel>>(data);
                foreach (KeyValuePair<string, DoctorModel> item in clinic)
                {
                    Doctor.Add(
                            new DoctorModel
                            {
                                Name = item.Value.Name,
                                IsHeProvindingSupportForSIAAndVaccination = item.Value.IsHeProvindingSupportForSIAAndVaccination
                            }
                        );
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("No data found!", "Add some data to show here", "OK");
            }
        }
        // Methods
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
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            GetDoctor();

            IsBusy = false;
        }

        void Clear()
        {
            Doctor.Clear();
        }
    }
}
