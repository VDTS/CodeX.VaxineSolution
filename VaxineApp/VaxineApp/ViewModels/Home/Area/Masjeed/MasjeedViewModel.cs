using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Masjeed;
using VaxineApp.Views.Shared;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class MasjeedViewModel : BaseViewModel
    {
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
        private List<MasjeedModel> _masjeed;
        public List<MasjeedModel> Masjeed
        {
            get { return _masjeed; }
            set
            {
                _masjeed = value;
                RaisedPropertyChanged(nameof(Masjeed));
            }
        }
        // Commands
        public ICommand AddMasjeedCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand EditCommand { private set; get; }
        public ICommand GoToMapCommand { private set; get; }
        public AsyncCommand GetMasjeedCommand { private set; get; }


        // Constructor
        public MasjeedViewModel()
        {
            Masjeed = new List<MasjeedModel>();
            SaveAsPDFCommand = new Command(SaveAsPDF);
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
            GetMasjeed();
            GetMasjeedCommand = new AsyncCommand(Refresh);
            AddMasjeedCommand = new Command(AddMasjeed);
            GoToMapCommand = new Command(GoToMap);
        }

        private async void Edit(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        private async void Delete(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        private async void SaveAsPDF(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Not submitted!", "This functionality is under construction", "OK");
        }

        private async void GoToMap(object obj)
        {
            var route = $"{nameof(GoogleMapPage)}";
            await Shell.Current.GoToAsync(route);
        }

        // Methods

        public async void GetMasjeed()
        {
            var data = await DataService.Get($"Masjeed/{Preferences.Get("TeamId", "")}");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, MasjeedModel>>(data);
            foreach (KeyValuePair<string, MasjeedModel> item in clinic)
            {
                Masjeed.Add(
                    new MasjeedModel
                    {
                        MasjeedName = item.Value.MasjeedName,
                        KeyInfluencer = item.Value.KeyInfluencer,
                        DoYouHavePermissionForAdsInMasjeed = item.Value.DoYouHavePermissionForAdsInMasjeed,
                        DoesImamSupportsVaccine = item.Value.DoesImamSupportsVaccine,
                        Latitude = item.Value.Latitude,
                        Longitude = item.Value.Longitude
                    }
                    );
            }
        }
        // Route Methods
        async void AddMasjeed()
        {
            var route = $"{nameof(AditMasjeedPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            GetMasjeed();

            IsBusy = false;
        }

        void Clear()
        {
            Masjeed.Clear();
        }
    }
}
