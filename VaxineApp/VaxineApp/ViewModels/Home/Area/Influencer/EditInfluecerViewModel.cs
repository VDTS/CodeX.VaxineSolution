using Newtonsoft.Json;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Home.Area.Influencer;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Influencer
{
    public class EditInfluecerViewModel : ViewModelBase
    {
        // Property
        private InfluencerModel influencer;
        public InfluencerModel Influencer
        {
            get
            {
                return influencer;
            }
            set
            {
                influencer = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PutCommand { private set; get; }

        public EditInfluecerViewModel(InfluencerModel influencer)
        {
            // Property
            Influencer = influencer;

            // Command
            PutCommand = new Command(Put);
        }

        public async void Put()
        {
            var jsonData = JsonConvert.SerializeObject(Influencer);
            var data = await DataService.Put(jsonData, $"Influencer/{Preferences.Get("TeamId", "")}/{Influencer.FId}");
            if (data == "Submit")
            {
                await App.Current.MainPage.DisplayAlert("Updated", $"item has been updated", "OK");
                var route = $"//{nameof(InfluencerPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Not Updated", "Try again", "OK");
            }
        }
    } 
}
