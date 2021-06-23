using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.MVVMHelper;
using VaxineApp.Views.Home.Area.Influencer;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Influencer
{
    public class AddInfluecerViewModel : ViewModelBase
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
        public ICommand PostCommand { private set; get; }

        // ctor
        public AddInfluecerViewModel()
        {
            // Property
            Influencer = new InfluencerModel();

            // Command
            PostCommand = new Command(Post);
        }

        public async void Post()
        {
            Influencer.Id = Guid.NewGuid();

            var data = JsonConvert.SerializeObject(Influencer);

            string a = DataService.Post(data, $"Influencer/{Preferences.Get("TeamId", "")}");
            await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            var route = $"//{nameof(InfluencerPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
