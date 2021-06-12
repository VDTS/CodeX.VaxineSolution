using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using VaxineApp.Views.Home.Area.Influencer;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.ViewModels.Home.Area.Influencer
{
    public class InfluencerViewModel : BaseViewModel
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
        private List<InfluencerModel> _influencer;
        public List<InfluencerModel> Influencer
        {
            get { return _influencer; }
            set
            {
                _influencer = value;
                RaisedPropertyChanged(nameof(Influencer));
            }
        }

        // Commands
        public AsyncCommand GetInfluencerCommand { private set; get; }
        public ICommand AddInfluencerCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand DeleteCommand { private set; get; }
        public ICommand EditCommand { private set; get; }

        // Constructor
        public InfluencerViewModel()
        {
            SaveAsPDFCommand = new Command(SaveAsPDF);
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
            Influencer = new List<InfluencerModel>();
            GetInfluencer();
            GetInfluencerCommand = new AsyncCommand(Refresh);
            AddInfluencerCommand = new Command(AddInfluencer);
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

        public async void GetInfluencer()
        {
            var data = await DataService.Get($"Influencer/{Preferences.Get("TeamId", "")}");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, InfluencerModel>>(data);
            foreach (KeyValuePair<string, InfluencerModel> item in clinic)
            {
                Influencer.Add(
                    new InfluencerModel
                    {
                        Name = item.Value.Name,
                        Contact = item.Value.Contact,
                        Position = item.Value.Position,
                        DoesHeProvidingSupport = item.Value.DoesHeProvidingSupport
                    }
                    );
            }
        }

        // Route Methods
        async void AddInfluencer()
        {
            var route = $"{nameof(AddInfluencerPage)}";
            await Shell.Current.GoToAsync(route);
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            GetInfluencer();

            IsBusy = false;
        }

        void Clear()
        {
            Influencer.Clear();
        }
    }
}
