using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Home.Area.Clinic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Clinic
{
    public class AddClinicViewModel : ViewModelBase
    {
        // Property
        private ClinicModel clinic;
        public ClinicModel Clinic
        {
            get
            {
                return clinic;
            }
            set
            {
                clinic = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand PostCommand { private set; get; }

        // ctor
        public AddClinicViewModel()
        {
            // Property
            Clinic = new ClinicModel();

            // Command
            PostCommand = new Command(Post);
        }

        public async void Post()
        {
            // Validate clinic
            if (Clinic.ClinicName != null)
            {
                Clinic.Id = new Guid();
                var data = JsonConvert.SerializeObject(Clinic);

                string a = await DataService.Post(data, $"Clinic/{Preferences.Get("TeamId", "")}");
                if (a == "OK")
                {
                    await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

                    var route = $"//{nameof(ClinicPage)}";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(a, "Try again", "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Empty fields", "Add data to required fields", "OK");
            }
        }
    }
}
