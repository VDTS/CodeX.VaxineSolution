using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Doctor;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Doctor
{
    public class DoctorViewModel : ViewModelBase, IDataCrud, IVMUtils
    {
        // Property
        private DoctorModel selectedDoctor;
        public DoctorModel SelectedDoctor
        {
            get
            {
                return selectedDoctor;
            }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DoctorModel> doctors;
        public ObservableCollection<DoctorModel> Doctors
        {
            get
            {
                return doctors;
            }
            set
            {
                doctors = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }


        // Command
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand GoToPutPageCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }


        // ctor
        public DoctorViewModel()
        {
            // Property
            Doctors = new ObservableCollection<DoctorModel>();
            SelectedDoctor = new DoctorModel();


            // Get
            Get();

            // Command
            PullRefreshCommand = new Command(Refresh);
            DeleteCommand = new Command(Delete);
            SaveAsPDFCommand = new Command(SaveAsPDF);
            GoToPutPageCommand = new Command(GoToPutPage);
            GoToPostPageCommand = new Command(GoToPostPage);
        }

        public async void Get()
        {
            var data = await DataService.Get($"Doctor/{Preferences.Get("TeamId", "")}");
            if (data != "null" & data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, DoctorModel>>(data);
                foreach (KeyValuePair<string, DoctorModel> item in clinic)
                {
                    Doctors.Add(
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

        public void Put()
        {
            throw new NotImplementedException();
        }

        public void Post()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Doctors.Clear();
        }

        public void CancelSelection()
        {
            throw new NotImplementedException();
        }

        public async void SaveAsPDF()
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        public async void GoToPostPage()
        {
            var route = $"{nameof(AddDoctorPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public async void GoToPutPage()
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

        public void GoToDetailsPage()
        {
            throw new NotImplementedException();
        }

        public void GoToMapPage()
        {
            throw new NotImplementedException();
        } 
    }
}
