using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
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
                    StandardMessagesDisplay.AddDisplayMessage(Clinic.ClinicName);

                    var route = $"//{nameof(ClinicPage)}";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    StandardMessagesDisplay.CanceledDisplayMessage();
                }
            }
            else
            {
                StandardMessagesDisplay.InvalidDataDisplayMessage();
            }
        }
    }
}
