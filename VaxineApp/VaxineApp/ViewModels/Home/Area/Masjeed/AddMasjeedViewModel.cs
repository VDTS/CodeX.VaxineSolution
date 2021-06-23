using DataAccessLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using VaxineApp.MVVMHelper;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Masjeed;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class AddMasjeedViewModel : ViewModelBase
    {
        // Property
        private MasjeedModel masjeed;
        public MasjeedModel Masjeed
        {
            get
            {
                return masjeed;
            }
            set
            {
                masjeed = value;
                OnPropertyChanged();
            }
        }

        // Command
        public ICommand PostCommand { private set; get; }
        public AddMasjeedViewModel()
        {
            // Property
            Masjeed = new MasjeedModel();

            // Command
            PostCommand = new Command(Post);
        }

        private async void Post()
        {
            Masjeed.Id = Guid.NewGuid();

            var data = JsonConvert.SerializeObject(Masjeed);

            string a = DataService.Post(data, $"Masjeed/{Preferences.Get("TeamId", "")}");
            await App.Current.MainPage.DisplayAlert(a, "Successfully posted", "OK");

            var route = $"//{nameof(MasjeedPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
