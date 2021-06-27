using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Home.Area.Doctor;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Doctor
{
    public class EditDoctorViewModel : ViewModelBase
    {
        // Propery
        private DoctorModel doctor;
        public DoctorModel Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }
        public EditDoctorViewModel(DoctorModel doctor)
        {
            // Property
            Doctor = doctor;

            // Command
            PutCommand = new Command(Put);
        }

        private async void Put()
        {
            var jsonData = JsonConvert.SerializeObject(Doctor);
            var data = await DataService.Put(jsonData, $"Doctor/{Preferences.Get("TeamId", "")}/{Doctor.FId}");
            if (data == "Submit")
            {
                await App.Current.MainPage.DisplayAlert("Updated", $"item has been updated", "OK");
                var route = $"//{nameof(DoctorPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Not Updated", "Try again", "OK");
            }
        }
    }
}
