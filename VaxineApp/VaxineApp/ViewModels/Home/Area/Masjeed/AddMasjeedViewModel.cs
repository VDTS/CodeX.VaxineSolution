using Newtonsoft.Json;
using System;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
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
        public ICommand AddLocationCommand { private set; get; }

        // ctor
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

            string a = await DataService.Post(data, $"Masjeed/{Preferences.Get("TeamId", "")}");
            if (a == "OK")
            {
                StandardMessagesDisplay.AddDisplayMessage(Masjeed.MasjeedName);
            }
            else
            {
                StandardMessagesDisplay.CanceledDisplayMessage();
            }
            var route = $"//{nameof(MasjeedPage)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
